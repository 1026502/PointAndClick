using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{

    public float currentPlayerMoney;
    public float baseIncome = 100;

    public TMPro.TextMeshProUGUI currentMoneyText;
    
    void Start()
    {
        currentPlayerMoney = 200;
        StartCoroutine(DayMoneyCycle());
    }

    public void GiveMoney(float x)
    {
        currentPlayerMoney += x;

        if (currentPlayerMoney < 0)
        {
            currentPlayerMoney = 0;
        }
    }

    public IEnumerator DayMoneyCycle()
    {
        yield return new WaitForSeconds(10);
        GiveMoney(baseIncome);

        StartCoroutine(DayMoneyCycle());    

    }

    public void BuyThings(float purchaseAmount)
    {
        if(currentPlayerMoney < purchaseAmount)
        {
            //Add UI showing denied purchase.
        }
    }

    public IEnumerator DisplayUIMessage(int seconds)
    {

    }
}
