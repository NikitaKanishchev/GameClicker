using TMPro;
using UnityEngine;

public class PlayGame : MonoBehaviour
{
    [SerializeField] private int money;
    public TMP_Text moneyText;
    
    public void AddMoney()
    {
        money++;
        moneyText.text = "Money:  " + money;
    }

    private void FixedUpdate()
    {
        
    }
}
