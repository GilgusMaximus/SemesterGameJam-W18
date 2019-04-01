using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public static string playerName;

    public InputField input;
    public GameObject text;

    public GameObject PauseMenu;

   /* public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;
    */
    public bool Playmenu;

    public List<string> levels;

    // Use this for initialization
    void Start () {
     /*   if (Playmenu)
        {
            Button1.GetComponent<Button>().enabled = true;
            Button1.GetComponent<Image>().color = Color.white;
            if (CurrencyManager.CurrentLevelToUnlock > 0)
            {
                Button2.GetComponent<Button>().enabled = true;
                Button2.GetComponent<Image>().color = Color.white;
            }
            if (CurrencyManager.CurrentLevelToUnlock > 1)
            {
               // Button3.GetComponent<Button>().enabled = true;
              //  Button3.GetComponent<Image>().color = Color.white;
            }

        }
        */
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadScene(string sceneName)
    {
        CurrencyManager.resetRoundMoney();
       

        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
        if (sceneName.Equals("Menu"))//Maarten: reset some stats
        {
            GameScoreManager.menuExit = true;
            GameScoreManager.resetScore();
        }

    }


    public void Exit()
    {
        Application.Quit();
    
        
    }


    public void InputAppear()
    {
        input.gameObject.SetActive(true);

        input.ActivateInputField();

    }

    public void SetName(string sceneName)
    {
        playerName =input.text;
        if (playerName.Equals(""))
        {
            playerName = "Anomyous_Owl";
        }
        GameScoreManager.resetScore();
        
        SceneManager.LoadScene(sceneName);
    }

    public void openText()
    {
        text.SetActive(true);

    }

    public void openPauseMenu()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive( true);

    }

    public void Continue()
    {

        Time.timeScale = 1;
        PauseMenu.SetActive(false);
    }


    public void SetDiffNormal()
    {
        GameScoreManager.currentDiff = GameScoreManager.difficulty.normal;
        LoadRandomLevel();
    }
    public void SetDiffHard()
    {
        GameScoreManager.currentDiff = GameScoreManager.difficulty.hard;
        LoadRandomLevel();
    }

    private void LoadRandomLevel()
    {
        int r = Random.Range(0,levels.Count);
        SceneManager.LoadScene(levels[r]);

    }

}
