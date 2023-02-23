using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{

    public float currentPlayerMoney;
    public float baseIncome;
    public GameObject[] purchasablePackages;
    public float[] purchasablePackagesPrices;

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

    public GameObject[] cameraPoints;
    private int currentCameraPoint = 0;

    public GameObject[] computerObjects;

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

    public void CloseMenu(int i)
    {
        menus[i].SetActive(false);

    }

    public void FluctuateCrypto()
    {
        for( int i = 0; i < cryptoPrice.Length; i++)
        {
            float randomNumber = Random.Range(-20, 21);

            randomNumber = randomNumber / 100;
            
            cryptoPrice[i] = cryptoPrice[i] * 1+randomNumber;

            if(cryptoPrice[i] <= 0)
            {
                cryptoPrice[i] = 0.1f;
            }

            cryptoPriceText[i].text = cryptoPrice[i].ToString("0.00");


            Debug.Log("crypto fluctuated");

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

    public void PurchaseFurniture(int i)
    {
        if (currentPlayerMoney < purchasablePackagesPrices[i])
        {
            // UI message showing player can't purchase it.
        }
        else
        {
            purchasablePackages[i].SetActive(true);

        }

    }


    public void NextCamera()
    {
        if(currentCameraPoint >= 6)
        {
            Debug.Log("can't go above camera point 6");
        }
        else
        {
            cameraPoints[currentCameraPoint].SetActive(false);

            cameraPoints[currentCameraPoint + 1].SetActive(true);

            currentCameraPoint = currentCameraPoint + 1;
        }
        


    }

    public void PreviousCamera()
    {
        if(currentCameraPoint<= 0)
        {
            Debug.Log("can't go below camerapoint 0");

        }
        else
        {
            cameraPoints[currentCameraPoint].SetActive(false);

            cameraPoints[currentCameraPoint - 1].SetActive(true);

            currentCameraPoint = currentCameraPoint - 1;
        }


    }

    public void SelectCamera(int i)
    {
        cameraPoints[currentCameraPoint].SetActive(false);
        cameraPoints[i].SetActive(true);

    }

    private void Update()
    {
        if (currentCameraPoint == 5 && computerObjects[3].activeSelf == false)
        {
            computerObjects[0].SetActive(true);
        }
        else
        {
            computerObjects[0].SetActive(false);
        }
    }

    public void DisableComputer()
    {
        computerObjects[1].SetActive(true);
        computerObjects[2].SetActive(true);
        computerObjects[3].SetActive(false);

    }

    public void EnableComputer()
    {
        computerObjects[1].SetActive(false);
        computerObjects[2].SetActive(false);
        computerObjects[3].SetActive(true);

    }


}

