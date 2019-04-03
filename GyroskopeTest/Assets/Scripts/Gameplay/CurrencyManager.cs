using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CurrencyManager : MonoBehaviour {

    public static int stabilität=0; //TODO we need a way to determine on which level we are working on right now

   // public int mindStabi = 0; //required stability for the level

    public static int[] stabilitätsReq = { 10, 20 }; //die stabilitätsGrenzen
    public static int CurrentLevelToUnlock = 0; //der index für das nächste Level 0 = das 2. level
    public static bool[] isUnlocked; // ob das jeweilige level freigeschaltet wurde

    public static int  RoundMoney=0;


    public static int currentMoney = 0;
    

    public Text moneyDisplay;

    private void Awake()
    {
       // resetMoney();
       // resetStabilität();

        LevelData d = SaveSystem.LoadData();
        CurrentLevelToUnlock = d.level;
        stabilität = d.stability;
        //moneyDisplay.text = d.money + " " + d.stability;
        if (d != null)
        {
            Debug.Log("Previous: Money: " + d.money + " Stability: " + d.stability+" CurrentLevel: "+d.level); //Here we can just set our current money and stability
        }

        currentMoney = d.money;
    }

    public static int getStabilitätsRequirement()
    {
        return stabilitätsReq[CurrentLevelToUnlock];
    }

    private void OnDestroy()//save data when we end the scene for debugging TODO: save our progress AFTER the level-up screen
    {
        SaveSystem.SaveData(this);
    }

    // Update is called once per frame
    void Update () {
        if (moneyDisplay != null)
        {
            moneyDisplay.text = "Money: " + RoundMoney;
        }
	}

    public void checkStabilität()//check if we have enough stability
    {
        if (stabilität>=stabilitätsReq[CurrentLevelToUnlock])
        {
          //  isUnlocked[CurrentLevelToUnlock] = true;

            //TODO load scene, whatever
            Debug.Log("We got enough stability");
            if (CurrentLevelToUnlock + 1 <= stabilitätsReq.Length - 1)
            {
                CurrentLevelToUnlock++;

            }
            
            resetStabilität();
            SaveSystem.SaveData(this);
        }
    }

    public void buyStabilität()//erhöhen stabilität durch buttenaufruf
    {
        if (currentMoney>=100)
        {
            currentMoney -= 100;
            incrementStabilität(1);
            SaveSystem.SaveData(this);
        }

    
    }

    public static void incrementMoney(int i)
    {
        currentMoney += i;
    }

    public static void incrementRoundMoney(int i)
    {
        RoundMoney += i;
    }

    public void resetMoney()
    {
        currentMoney = 0;
    }

   

    public static void resetRoundMoney()
    {
       RoundMoney = 0;
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

    public int getLevelToUnlock()
    {
        return CurrentLevelToUnlock;

    }

}
