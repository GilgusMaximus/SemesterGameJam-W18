using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDynamite : MonoBehaviour {

    public bool istime;
    public int time;//in seconds
   

    

  

    public void apply()  //zeit wird abgezofen oder hinzugefügt
    {
      


        if (istime)
        {
            GameScoreManager.rtime += time;   
        }
        else
        {
            GameScoreManager.rtime -= time;
        }

    }
	
}
