using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameScoreManager : MonoBehaviour {

    public static int currentScore=0;
    public float timer = 600;//time in seconds
    

    public static float rtime;//remaining time
    public static bool isCounting;

    //public Text TimeDisplay;
    public TMP_Text TimeDisplay;

    public GameObject scoredisplay;

    public static bool l;

    CurrencyManager cm;
    public static bool menuExit=false;

    public enum difficulty //we enable/disable stuff based on the set diffiulty
    {
        nothing,normal,hard
    };

    public static difficulty currentDiff;

    private void Start()//Aufpassen: wenn man in einem level mehrere scenen läd, darf man den score nicht resetten!
    {
        menuExit = false;
        if (rtime <= 0)
        {
            rtime = timer;
        }
        isCounting = true;

        cm = gameObject.GetComponent<CurrencyManager>();


        switch (currentDiff)
        {
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

    private void Update()
    {

        if (isCounting)
        {
            rtime -= Time.deltaTime;
            string minutes = Mathf.Floor(rtime / 60).ToString("00");
            string seconds = Mathf.RoundToInt(rtime%60).ToString("00");

            TimeDisplay.text = minutes + ":" + seconds;

           
            if (rtime<=0)//abbruch
            {
                TimeDisplay.text = "0";
                isCounting = false;
                //verloren, naechste scene?
                if (currentDiff!=GameScoreManager.difficulty.nothing)//only upload in highscore scene Maarten: we are in highscore Modus if diff is not nothing
                {
                    Highscores.AddNewHighscore(Menu.playerName, currentScore);//hochladen des highscores TODO how to get name
                    if (!scoredisplay.activeInHierarchy)
                    {
                        TMP_Text scored = scoredisplay.transform.GetChild(0).GetComponent<TMP_Text>();
                        scored.text = "Your score is: " + GameScoreManager.currentScore.ToString();

                        scoredisplay.SetActive(true);
                    }
                }

                
                if (currentDiff==GameScoreManager.difficulty.nothing && menuExit==false)//Maarten: Do we always enable the upgrade, even after highscorerun?
                {
                    SceneManager.LoadScene("ProgressScene");
                }


                //Anzeigen Highscore und enable laden naechster scene durch button
               

                Debug.Log("Times's up guys!");
            }

        }

    }

    public static void resetScore()//Caution: Always call this before embarking a new run!
    {
        currentScore = 0;
        rtime = 0;
    }

    public static void addScore(int i)
    {
        currentScore += i;

        //CurrencyManager.incrementMoney(i / 2);//TODO MAARTEN einfach irgendeinen value addieren, können wir uns ja noch entscheiden
    }

    public void ButtonHandleLoad()//invoked by button at end of round to load back into menu
    {
        if (scoredisplay.activeInHierarchy)
        {
            scoredisplay.SetActive(false);
            //isCounting = true;
            SceneManager.LoadScene("ProgressScene");
        }
    }

    public void OnDestroy()
    {
        currentDiff = difficulty.nothing;
    }

}
