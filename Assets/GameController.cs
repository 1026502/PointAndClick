using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{

    public float currentPlayerMoney;
    public float baseIncome;

    public GameObject[] menus;
    //public GameObject furnitureMenu;
    //public GameObject cryptoMenu;

    public GameObject[] uiPrompts;



    public TMPro.TextMeshProUGUI currentMoneyText;
    public float[] reliablePrices;

    public Text[] reliablePriceText;

    public float[] cryptoPrice;

    public Text[] cryptoPriceText;
    public int[] holdings;
    public Text[] holdingsText;

    void Start()
    {
        StartingPricesCrypto();
        currentPlayerMoney = 100;
        StartCoroutine(DayMoneyCycle());
    }

    public void GiveMoney(float x)
    {
        currentPlayerMoney += x;

        if (currentPlayerMoney < 0)
        {
            currentPlayerMoney = 0;
        }

        currentMoneyText.text = currentPlayerMoney.ToString("0.00");
    }

    public IEnumerator DayMoneyCycle()
    {
        Debug.Log("The player will be given " + baseIncome + " after 10 seconds");

        yield return new WaitForSeconds(10);
        GiveMoney(baseIncome);
        FluctuateCrypto();

        StartCoroutine(DayMoneyCycle());

        Debug.Log("The player has been given " + baseIncome);

    }

    public void BuyThings(int i)
    {
        float purchaseAmount = reliablePrices[i];

        if (currentPlayerMoney < purchaseAmount)
        {
            //Add UI showing denied purchase.

            Debug.Log("Player can't afford this!");
        }
        else
        {
            currentPlayerMoney -= purchaseAmount;

            // Modify the base income after purchase.

            baseIncome = baseIncome * 1.1f;

            //Increase the purchase amount on future purchases of the same investment.

            purchaseAmount = purchaseAmount * 1.13f;

            reliablePrices[i] = purchaseAmount;

            FloatToString2dp(i);



            currentMoneyText.text = currentPlayerMoney.ToString("0.00");


        }
    }


    public void FloatToString2dp(int i)
    {
        reliablePriceText[i].text = reliablePrices[i].ToString("0.00");


    }

    public IEnumerator DisplayTimedUIMessage(int seconds, GameObject uiPrompt)
    {
        uiPrompt.SetActive(true);
        yield return new WaitForSeconds(seconds);
        uiPrompt.SetActive(false);
    }


    public void ExitButton()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(false);
        }

    }

    public void OpenMenu(int i)
    {
        menus[i].SetActive(true);

    }

    public void FluctuateCrypto()
    {
        for( int i = 0; i < cryptoPrice.Length; i++)
        {
            float randomNumber = Random.Range(-10, 11);

            randomNumber = randomNumber / 100;
            
            cryptoPrice[i] = cryptoPrice[i] * 1+randomNumber;

            if(cryptoPrice[i] <= 0)
            {
                cryptoPrice[i] = 0.1f;
            }

            cryptoPriceText[i].text = cryptoPrice[i].ToString("0.00");

        }
    }

    public void BuyCrypto(int i)
    {
        float purchasePrice = cryptoPrice[i];

            if(currentPlayerMoney < purchasePrice)
        {
            Debug.Log("You can't afford that");
        }
        else
        {
            currentPlayerMoney -= purchasePrice;
            holdings[i] = holdings[i] + 1;
            UpdateHoldings(i);
            currentMoneyText.text = currentPlayerMoney.ToString("0.00");
        }
    }

    public void SellCrypto(int i)
    {
        float sellPrice = cryptoPrice[i];

        if(holdings[i] <= 0)
        {

            Debug.Log("I dont own anymore of this!");
        }
        else
        {
            currentPlayerMoney += cryptoPrice[i];
            holdings[i] = holdings[i] - 1;
            UpdateHoldings(i);
            currentMoneyText.text = currentPlayerMoney.ToString("0.00");
        }
    }

    public void UpdateHoldings(int i)
    {
        
        holdingsText[i].text = holdings[i].ToString();
    }

    private void StartingPricesCrypto()
    {
        for (int i = 0; i < cryptoPrice.Length; i++)
        {
            cryptoPrice[i] = Random.Range(1, 10);
            cryptoPriceText[i].text = cryptoPrice[i].ToString("0.00");

        }
    }

}

