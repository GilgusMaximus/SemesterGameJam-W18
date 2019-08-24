using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class CurrencyManager : MonoBehaviour {

    //public static int stabilität=0; //TODO we need a way to determine on which level we are working on right now

   // public int mindStabi = 0; //required stability for the level

    //public static int[] stabilitätsReq = { 10, 20 }; //die stabilitätsGrenzen
   // public static int CurrentLevelToUnlock = 0; //der index für das nächste Level 0 = das 2. level

    public static int  RoundMoney=0;


    public static int currentMoney = 0;
    

    public TMP_Text moneyDisplay;

    public List<LevelPositions> lData;

    private void Awake()
    {

        LevelData d = SaveSystem.LoadData();
        currentMoney = d.money;
        
        if (d != null)
        {
            Debug.Log("Previous: Money: " + d.money); //Here we can just set our current money and stability

            /*if (d.lData!=null)
            {
                foreach(LevelPositions lp in d.lData)
                {
                    if (lp.unlocked)
                    {
                        Debug.Log("Unlocked: " + lp.levelName);
                    }
                }
            }*/
        }

        if (d == null || d.lData==null || d.lData.Count == 0)//initial buildup for the stability
        {
            lData = new List<LevelPositions> { new LevelPositions("Level1",0,0,true),
                                               new LevelPositions("Level2",0,10,false),
                                               new LevelPositions("Level3",0,20,false)};
            SaveSystem.SaveData(this);
        }
        else
        {
            lData = d.lData;// load the saved data
        }

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

    public void buyLevelStability(int i)//increment the level stability
    {
        if (currentMoney >= 100 && !lData[i].unlocked)
        {
            currentMoney -= 100;
            lData[i].curStab += 1;
            checkLevelStabilität(lData[i]);

            SaveSystem.SaveData(this);
        }
    }

    public void checkLevelStabilität(LevelPositions ld)//check if we have enough stability and set the unlocked flag
    {
        if (ld.curStab>=ld.reqStab)
        {
            ld.unlocked = true;
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

    public List<LevelPositions> getLData()
    {
        return lData;
    }

}
