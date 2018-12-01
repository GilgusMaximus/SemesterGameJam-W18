using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreManager : MonoBehaviour {

    public static int currentScore=0;


    public static void resetScore()
    {
        currentScore = 0;
    }

    public static void addScore(int i)
    {
        currentScore += i;
    }

}
