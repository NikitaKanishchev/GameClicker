using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayGame : MonoBehaviour
{
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
        
        money = PlayerPrefs.GetInt("Money");
        hitPower = 1;
        
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
        moneyText.text = "Money:  " + money;
        exp++;

        plusObject.SetActive(false);
        plusObject.transform.position = new Vector3(Random.Range(465, 645 + 1), Random.Range(745, 945 + 1), 0);
        plusObject.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(Fly());
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
