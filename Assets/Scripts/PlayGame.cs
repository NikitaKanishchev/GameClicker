using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayGame : MonoBehaviour
{
    public ClickEffect clickEffect;
    
    [SerializeField] private int money;
    private int currentMoney;
    private int hitPower;
    public TMP_Text moneyText;

    [SerializeField] private float exp;
    [SerializeField] private int level;
    private float expNextLevel = 10;

    private float lerpTimer;
    private float delayTimer;

    public TMP_Text levelText;
    public Image levelBarBack;
    public Image levelBarFront;

    public Button buttonBusiness;
    public Button buttonStartup;

    public GameObject plusObject;

    public TMP_Text plusText;


    public void Start()
    {
        UpdateMoneyText();
        LoadGame();
        UpdateButtonStates();

        money = PlayerPrefs.GetInt("Money");

        levelBarFront.fillAmount = exp / expNextLevel;
        levelBarBack.fillAmount = exp / expNextLevel;
    }

    public void Update()
    {
        plusText.text = "+ " + hitPower;

        levelText.text = "LVL: " + level;
        
        if (exp >= expNextLevel)
        {
            level++;
            exp = 0;
            expNextLevel *= 2;
            
            SaveGame();
        }

        if (hitPower > 2 && hitPower < 3)
        {
            money = hitPower;
        }

        UpdateUI();
    }
    
    public void AddMoney()
    {
        money += hitPower;
        moneyText.text = "" + money;
        exp++;
        
        clickEffect.Poke();

        plusObject.SetActive(false);
        plusObject.transform.position = new Vector3(Random.Range(465, 645 + 1), Random.Range(785, 985 + 1), 0);
        plusObject.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(Fly());
        SaveGame();
    }

    public void Initialize(int startingLevel, float startingExperience)
    {
        level = startingLevel;
        exp = startingExperience;
        expNextLevel = CalculateExperienceToNextLevel(level);
    }

    public void BuyBusiness()
    {
        if (money >= 10)
        {
            money -= 10;
            hitPower += 1;
            moneyText.text = money.ToString();
            buttonBusiness.gameObject.SetActive(false);
            SaveGame();
        }
        else
        {
            print( " Not enough money!! ");
        }
    }

    public void BuyStartup()
    {
        if (money >= 20)
        {
            money -= 20;
            hitPower += 2;
            moneyText.text = money.ToString();
            buttonStartup.gameObject.SetActive(false);
            SaveGame();
        }
        else
        {
            print( " Not enough money!! ");
        }
    }

    private void UpdateMoneyText()
    {
        moneyText.text = currentMoney.ToString(); 
    }
    
    private float CalculateExperienceToNextLevel(int level)
    {
        return level*= 2;
    }

    private void UpdateUI()
    {
        levelBarFront.fillAmount = exp / expNextLevel;
    }
    
    private void SaveGame()
    {
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("HitPower", hitPower);
        PlayerPrefs.SetInt("Level", level); 
        PlayerPrefs.SetFloat("Experience", exp); 
        PlayerPrefs.SetFloat("LevelBarFill", levelBarFront.fillAmount);
        
        PlayerPrefs.SetInt("ButtonBusinessActive", buttonBusiness.gameObject.activeSelf ? 1 : 0);
        PlayerPrefs.SetInt("ButtonStartupActive", buttonStartup.gameObject.activeSelf ? 1 : 0);
        
        PlayerPrefs.Save();
    }
    
    private void LoadGame()
    {
        money = PlayerPrefs.GetInt("Money", 0);
        hitPower = PlayerPrefs.GetInt("HitPower", 1);
        level = PlayerPrefs.GetInt("Level", 1);
        exp = PlayerPrefs.GetFloat("Experience", 0f);
        levelBarFront.fillAmount = PlayerPrefs.GetFloat("LevelBarFill", 0f);
        
        buttonBusiness.gameObject.SetActive(PlayerPrefs.GetInt("ButtonBusinessActive", 1) == 1);
        buttonStartup.gameObject.SetActive(PlayerPrefs.GetInt("ButtonStartupActive", 1) == 1);
    }
    
    private void UpdateButtonStates()
    {
        buttonBusiness.interactable = hitPower <= 1; 
        buttonStartup.interactable = hitPower <= 2; 
    }
    
    private IEnumerator Fly()
    {
        for (int i = 0; i <= 19; i++)
        {
            yield return new WaitForSeconds(0.01f);

            plusObject.transform.position =
                new Vector3(plusObject.transform.position.x, plusObject.transform.position.y + 2, 0);
        }
        plusObject.SetActive(false);
    }
}

