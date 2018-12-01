using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDynamite : MonoBehaviour {

    public bool istime;
    public int time;//in seconds
   // public AudioSource audioSource;

    

    void Start()
    {
        //audioSource = GameObject.Find("Audio").GetComponent<AudioSource>();

        

    }

    public void apply()
    {
        GameScoreManager g= GameObject.Find("Highscoremanager").GetComponent<GameScoreManager>();

      //  audioSource.clip = Explosion;
      //  audioSource.Play();

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
