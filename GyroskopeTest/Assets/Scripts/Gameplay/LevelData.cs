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

    
    public List<sVector3> spawnPos; // possible spawn Positions for the levels
    public List<bool> unlockedPos; // mapps, if we already unlcoked the position. All positions have fixed costs to unlock

    public LevelPositions(string name, int cur, int req, bool unlocked, List<sVector3> spawnPos, List<bool> unlockedPos)
    {
        levelName = name;
        curStab = cur;
        reqStab = req;
        this.unlocked = unlocked;
        this.spawnPos = spawnPos;
        this.unlockedPos = unlockedPos;
    }
}

[System.Serializable]
public class sVector3
{
    public float x, y, z;
    public sVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}
