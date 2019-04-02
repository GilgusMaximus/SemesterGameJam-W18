using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData {

    public int level; //TODO Method to determine which level we are working on right now with incrementing stability
    public List<string> levels;//all possible levels for highscore run
    public List<LevelPositions> positions;//all possible spawnpositions for possible Levels Maarten TODO: Implement this later
    public int money;
    public int stability;

    public LevelData(CurrencyManager cm)
    {
        money = cm.getMoney();
        stability = cm.getStability();
        level = cm.getLevelToUnlock();

        switch (level)//Maarten: determine all possible levels for highscore TODO find a better solution for this
        {
            case 1:
                levels = new List<string>{ "Level1","Level2" };
                break;

            case 2:
                levels = new List<string> { "Level1", "Level2", "Level3" };
                break;

            default:
                levels = new List<string> { "Level1"};
                break;
        }

    }

}

public class LevelPositions
{
    public string levelName;
    public List<Vector3> possiblePositions;//all possible spawnpositions for this specific level

    public LevelPositions(string name, List<Vector3> positions)
    {
        levelName = name;
        possiblePositions = positions;
    }
}
