using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData {

    public List<string> levels;//all possible levels for highscore run
    public List<LevelPositions> lData;//all possible spawnpositions for possible Levels Maarten TODO: Implement this later
    public int money;
    //public int stability;

    public LevelData(CurrencyManager cm)
    {
        money = cm.getMoney();

        lData = cm.getLData(); //get the relevant leveldata like stability from the currencymanager
        //Debug.Log(lData.Count);

        levels = new List<string> { };//write all the unlocked levels in here

        foreach (LevelPositions ld in lData)
        {
            if (ld.unlocked)
            {
                levels.Add(ld.levelName);
            }
        }

    }

}

[System.Serializable]
public class LevelPositions
{
    public string levelName;
    public int curStab;
    public int reqStab;
    public bool unlocked;

    public LevelPositions(string name, int cur, int req, bool unlocked)
    {
        levelName = name;
        curStab = cur;
        reqStab = req;
        this.unlocked = unlocked;
    }
}
