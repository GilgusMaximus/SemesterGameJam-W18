using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameScoreManager : MonoBehaviour {

    public static int currentScore = 0;
    public float timer = 600;//time in seconds
    

    public static float remainingTime;//remaining time
    public static bool timeIsCounting;
    public static bool menuExit;


    //the display texts while playing
    [SerializeField]
    private TMP_Text timeDisplayText;

    public enum difficulty //we enable/disable stuff based on the set diffiulty
    {
        nothing,normal,hard
    };

    public static difficulty currentDiff;

    
    private void Start()//Aufpassen: wenn man in einem level mehrere scenen läd, darf man den score nicht resetten!
    {
        
        menuExit = false;
        //remainingTime auf Anfangswert setzten
        if (remainingTime <= 0){
            remainingTime = timer;
        }
        //Ablaufen der Zeit starten
        timeIsCounting = true;
        
        //Features je nach Schwierigkeit (de)aktivieren
        if (Camera.main != null){
            switch (currentDiff){
                case difficulty.nothing:
                    break;

                case difficulty.normal:
                    Camera.main.GetComponent<Water>().enabled = false;

                    break;

                case difficulty.hard:
                    Camera.main.GetComponent<Water>().enabled = true;
                    break;

            }
        }
    }

    private void Update()
    {
        //wenn die Zeit läuft
        if (timeIsCounting){
            //Zeit seit letztem Update abziehen und anzeigen 
            remainingTime -= Time.deltaTime;
            string minutes = Mathf.Floor(remainingTime / 60).ToString("00");
            string seconds = (remainingTime % 60).ToString("00");

            timeDisplayText.text = minutes + ":" + seconds;

           //wenn keine Zeit mehr übrig ist
            if (remainingTime <= 0)//abbruch
            {
                //Ablaufen der Zeit deaktivieren und verbleibende Zeit auf 0 setzen
                timeDisplayText.text = "0";
                timeIsCounting = false;
                
                //wenn die Schwierigkeit nicht 'nothing' ist
                if (currentDiff!=GameScoreManager.difficulty.nothing)//only upload in highscore scene Maarten: we are in highscore Modus if diff is not nothing
                {
                    Highscores.AddNewHighscore(Menu.playerName, currentScore);//hochladen des highscores TODO how to get name
                }

                
                if (currentDiff==GameScoreManager.difficulty.nothing && menuExit==false)//Maarten: Do we always enable the upgrade, even after highscorerun?
                {
                    SceneManager.LoadScene("ProgressScene");
                }


                //Anzeigen Highscore und enable laden naechster scene durch button
            }

        }

    }
    
    public static void resetScore()//Caution: Always call this before embarking a new run!
    {
        currentScore = 0;remainingTime = 0;
    }

    public static void addScore(int i)
    {
        currentScore += i;

        //CurrencyManager.incrementMoney(i / 2);//TODO MAARTEN einfach irgendeinen value addieren, können wir uns ja noch entscheiden
    }

    public void OnDestroy()
    {
        currentDiff = difficulty.nothing;
    }

}
