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
            Debug.Log("Previous: Money: " + d.money+"\n"); //Here we can just set our current money and stability

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
            List<bool> unlockedPos1 = new List<bool> { true };
            List<bool> unlockedPos23 = new List<bool> { true, false, false };

            List<sVector3> spawnPos1 = new List<sVector3> { new sVector3(0,2.61f,0) };
            List<sVector3> spawnPos2 = new List<sVector3> { new sVector3(-24.09f, 5.7509f, -21.1f), new sVector3(-59f, 2f, 20f), new sVector3(-18f, -1f, 26f) };
            List<sVector3> spawnPos3 = new List<sVector3> { new sVector3(-24.94f, 6.56f, 9.32f), new sVector3(-14, 2f, -3f), new sVector3(-17f, 2f, 14f) };

            lData = new List<LevelPositions> { new LevelPositions("Level1",0,0,true, spawnPos1, unlockedPos1),
                                               new LevelPositions("Level2",0,10,false, spawnPos2, unlockedPos23),
                                               new LevelPositions("Level3",0,20,false, spawnPos3, unlockedPos23)};
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
            moneyDisplay.text = "" + RoundMoney;
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

    public void buyPosition(string stats)//enable optional spawnpos. Specify which level to unlock in UI Element
    {
        //we want to parse our targeted level and pos from the input
        int level= int.Parse(stats.Substring(0,2));
        int pos = int.Parse(stats.Substring(2));

        //Debug.Log(level + "/" + pos);
        if (currentMoney>=500 && lData[level].unlocked && !lData[level].unlockedPos[pos])
        {
            currentMoney -= 500;
            lData[level].unlockedPos[pos] = true;
            SaveSystem.SaveData(this);
            Debug.Log("Unlocked Position: "+lData[level].spawnPos[pos].x+"/"+lData[level].spawnPos[pos].y+"/"+ lData[level].spawnPos[pos].z);
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
