using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public static string playerName;

   // public InputField input;
    public GameObject text;

    public GameObject PauseMenu;

   /* public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;
    */
    public bool Playmenu;

    public List<string> levels;

    public string Username= "-435897gbfhjsdf28737";
    public GameObject inputf;
    private static bool nameSet= false;

    public Slider slider;
    public Text VolumeText;
    public bool options;
    // Use this for initialization
    void Start () {

        //Maarten: load all possible levels for highscore run
        LevelData d = SaveSystem.LoadData();
        if (d==null || d.levels==null)
        {
            levels = new List<string> { "Level1" };
        }
        else
        {
            levels = d.levels;
        }

        if (SceneManager.GetActiveScene().name == "LevelSelection")
        {
            GameObject Button2 = transform.Find("Level2").gameObject;
            GameObject Button3 = transform.Find("Level3").gameObject;

            if (d!=null && d.lData[1].unlocked)
            {
                Button2.GetComponent<Button>().enabled = true;
                Button2.GetComponent<Image>().color = Color.white;
            }
            if (d != null && d.lData[2].unlocked)
            {
                 Button3.GetComponent<Button>().enabled = true;
                  Button3.GetComponent<Image>().color = Color.white;
            }

        }


        if (slider != null)
        {
            slider.value = PlayerPrefs.GetFloat("MasterVolume", 1);
        }
        Username = PlayerPrefs.GetString("Username", "-435897gbfhjsdf28737");
        if (Username.Equals("-435897gbfhjsdf28737")&&!nameSet)
        {
            inputf.SetActive(true);
        }
        else
        {
           
            nameSet = true;
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (VolumeText != null)
        {
            Debug.Log("1:"+(SoundManager.MasterVolume*100)/1);
            Debug.Log((SoundManager.MasterVolume * 100) % 1);
            VolumeText.text = "Volume: " + ((SoundManager.MasterVolume * 100)/1 -(SoundManager.MasterVolume*100)%1) + "%";
           // SetVolume();
        }
    }

    public void LoadScene(string sceneName)
    {
        if (nameSet)
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
    }


    public void Exit()
    {
        Application.Quit();
    
        
    }


   /* public void InputAppear()
    {
        input.gameObject.SetActive(true);

        input.ActivateInputField();

    }
    */

   /* public void SetName(string sceneName)
    {
        playerName =input.text;
        if (playerName.Equals(""))
        {
            playerName = "Anomyous_Owl";
        }
        GameScoreManager.resetScore();
        
        SceneManager.LoadScene(sceneName);
    }

    */

    public void openText()
    {
        if (nameSet)
        {
            text.SetActive(true);
        }
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

    public void setUsername()
    {
        Username = inputf.GetComponent<InputField>().text;
        if (Username.Equals(""))
        {
           Username = "Anomyous_Owl";
        }
        PlayerPrefs.SetString("Username", Username);
        if (!options) //ob man im optionsmenu ist
        {
            inputf.SetActive(false);
        }
        nameSet = true;
    }

    public void SetVolume()
    {
        Debug.Log("SetVolume");
        SoundManager.MasterVolume = slider.value;
        PlayerPrefs.SetFloat("MasterVolume", SoundManager.MasterVolume);
    }
   
}
