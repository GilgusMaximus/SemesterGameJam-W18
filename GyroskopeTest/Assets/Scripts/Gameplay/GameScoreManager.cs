using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScoreManager : MonoBehaviour {

    public static int currentScore=0;
    public float timer = 600;//time in seconds

    public static float rtime;//remaining time
    public static bool isCounting;

    public Text TimeDisplay;

    public GameObject scoredisplay;

    public static bool l;

    private void Start()//Aufpassen: wenn man in einem level mehrere scenen läd, darf man den score nicht resetten!
    {
        if (rtime <= 0)
        {
            rtime = timer;
        }
        isCounting = true;
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
                //TODO verloren, naechste scene?
                if (SceneManager.GetActiveScene().name=="PlayScene")//only upload in highscore scene
                {
                    Highscores.AddNewHighscore(Menu.playerName, currentScore);//hochladen des highscores TODO
                }


                //Anzeigen Highscore und enable laden naechster scene durch button
                if (!scoredisplay.activeInHierarchy)
                {
                    Text scored = scoredisplay.transform.GetChild(0).GetComponent<Text>();
                    scored.text = "Your score is: "+GameScoreManager.currentScore.ToString();

                    scoredisplay.SetActive(true);
                }

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

        CurrencyManager.incrementMoney(i / 2);//TODO MAARTEN einfach irgendeinen value addieren, können wir uns ja noch entscheiden
    }

    public void ButtonHandleLoad()//invoked by button at end of round to load back into menu
    {
        if (scoredisplay.activeInHierarchy)
        {
            scoredisplay.SetActive(false);
            //isCounting = true;
            SceneManager.LoadScene("Menu");
        }
    }

}
