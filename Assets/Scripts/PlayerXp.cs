using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerXp : MonoBehaviour
{
    public int level;
    public float exp;
    public float expNextLevel;

    private float lerpTimer;
    private float delayTimer;

    [Header("UI")] public Image frontXpBar;
    public Image backXpBar;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI XpText;

    private void Start()
    {
        frontXpBar.fillAmount = exp / expNextLevel;
        backXpBar.fillAmount = exp / expNextLevel;
        levelText.text = "LVL" + level;
    }

    private void Update()
    {
        UpdateXpUI();
        
        if (exp > expNextLevel)
        {
            LevelUp();
        }
    }

    public void UpdateXpUI()
    {
        float xpFraction = exp / expNextLevel;
        float FXP = frontXpBar.fillAmount;
        if (FXP < xpFraction)
        {
            delayTimer += Time.deltaTime;
            backXpBar.fillAmount = xpFraction;
            if (delayTimer > 0.3)
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 4;
                frontXpBar.fillAmount = Mathf.Lerp(FXP, backXpBar.fillAmount, percentComplete);
            }
        }

        XpText.text = exp + "/" + expNextLevel;
    }

    public void Click()
    {
        exp++;
    }

    private void LevelUp()
    {
        level++;
        exp = 0;
        expNextLevel *= 2;
        frontXpBar.fillAmount = 0f;
        backXpBar.fillAmount = 0f;
    }
}
