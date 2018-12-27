using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CurrencyManager : MonoBehaviour {

    private int stabilität=0;

    public int mindStabi = 0;

    private static int currentMoney = 0;

    public Text moneyDisplay;

    private void Start()
    {
        resetMoney();
        resetStabilität();
    }

    // Update is called once per frame
    void Update () {
        moneyDisplay.text = "Money: " + currentMoney;
	}

    public void checkStabilität()//check if we have enough stability
    {
        if (stabilität>=mindStabi)
        {
            //TODO load scene, whatever
            Debug.Log("We got enough stability");
        }
    }

    public void buyStabilität()//erhöhen stabilität durch buttenaufruf
    {
        if (currentMoney>=100)
        {
            currentMoney -= 100;
            incrementStabilität(1);
        }
    }

    public static void incrementMoney(int i)
    {
        currentMoney += i;
        
    }

    public static void resetMoney()
    {
        currentMoney = 0;
    }

    public static int getMoney()
    {
        return currentMoney;
    }

    public void incrementStabilität(int i)
    {
        stabilität += i;
        checkStabilität();
    }

    public void resetStabilität()
    {
        stabilität = 0;
    }

    public int getStabilität()
    {
        return stabilität;
    }

}
