using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameScoreManager : MonoBehaviour {

    public static int currentScore=0;
    public float timer = 600;//time in seconds

    public float rtime;//remaining time
    public bool isCounting;

    public Text display;

    private void Start()
    {
        rtime = timer;
        isCounting = true;
    }

    private void Update()
    {

        if (isCounting)
        {
            rtime -= Time.deltaTime;
            float minutes = Mathf.Floor(rtime / 60);
            float seconds = Mathf.RoundToInt(rtime%60);

            display.text = minutes.ToString() + ":" + seconds.ToString();

            if (rtime<=0)//abbruch
            {
                display.text = "0";
                isCounting = false;
                //TODO verloren, naechste scene?
                Highscores.AddNewHighscore("Test", currentScore);//hochladen des highscores

                //laden naechster scene

                Debug.Log("Times's up guys!");
            }

        }

    }

    public static void resetScore()
    {
        currentScore = 0;
    }

    public static void addScore(int i)
    {
        currentScore += i;
    }

}
