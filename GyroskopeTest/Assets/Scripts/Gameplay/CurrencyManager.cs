using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CurrencyManager : MonoBehaviour {

    public static int stabilität=0; //TODO we need a way to determine on which level we are working on right now

    public int mindStabi = 0; //required stability for the level

    public static int currentMoney = 0;

    public Text moneyDisplay;

    private void Start()
    {
        resetMoney();
        resetStabilität();

        LevelData d = SaveSystem.LoadData();
        Debug.Log("Previous: Money: "+d.money+" Stability: "+d.stability); //Here we can just set our current money and stability
    }

    private void OnDestroy()//save data when we end the scene for debugging TODO: save our progress AFTER the level-up screen
    {
        SaveSystem.SaveData(this);
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

    public void resetMoney()
    {
        currentMoney = 0;
    }

    public int getMoney()
    {
        return currentMoney;
    }

    public int getStability()
    {
        return stabilität;
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

}
