using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData {

    public int level; //TODO Method to determine which level we are working on right now
    public int money;
    public int stability;

    public LevelData(CurrencyManager cm)
    {
        money = cm.getMoney();
        stability = cm.getStability();
        level = cm.getLevelToUnlock();
    }

}
