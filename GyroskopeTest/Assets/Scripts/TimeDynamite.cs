using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDynamite : MonoBehaviour {

    public bool istime;
    public int time;//in seconds

    public void apply()
    {
        GameScoreManager g= GameObject.Find("Highscoremanager").GetComponent<GameScoreManager>();

        if (istime)
        {
            g.rtime += time;   
        }
        else
        {
            g.rtime -= time;
        }

    }
	
}
