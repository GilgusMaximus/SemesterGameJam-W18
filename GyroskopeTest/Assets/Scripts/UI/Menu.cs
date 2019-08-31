using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Menu : MonoBehaviour {

    public static string playerName;

   // public InputField input;
    public GameObject text;

    public GameObject PauseMenu;

   /* public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;
    */

    public List<string> levels;

    public string Username= "-435897gbfhjsdf28737";
    public GameObject inputf;
    private static bool nameSet= false;

    public Slider slider;
    public Text VolumeText;
    public bool options;

    LevelData levelData;

    [SerializeField]
    private IngamePauseMenu ingamePauseMenu;

    public void setName(bool a){
        nameSet = a;
    }
    
    // Use this for initialization
    void Start () {


        if (VolumeText == null){
            Debug.LogError("Menu: Start: VolumeText no set to an object - is null");
        }
        
        //Maarten: load all possible levels for highscore run
        levelData = SaveSystem.LoadData();
        if (levelData==null || levelData.levels==null)
        {
            levels = new List<string> { "Level1" };
        }
        else
        {
            levels = levelData.levels;
        }

        if (SceneManager.GetActiveScene().name == "LevelSelection")
        {
            GameObject Button2 = transform.Find("Level2").gameObject;
            GameObject Button3 = transform.Find("Level3").gameObject;

            if (levelData!=null && levelData.lData[1].unlocked)
            {
                Button2.GetComponent<Button>().enabled = true;
                Button2.GetComponent<Image>().color = Color.white;
            }
            if (levelData != null && levelData.lData[2].unlocked)
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
            playerName = Username;
        }

    }
	
	// Update is called once per frame
	void Update () {
            VolumeText.text = "Volume: " + ((SoundManager.MasterVolume * 100)/1 -(SoundManager.MasterVolume*100)%1) + "%";
    }

    public void exitToMenu(String sceneName){
        if (nameSet == false){
            setName(true);
            LoadScene(sceneName);
            setName(false);
        }
        else{
            setName(true);
            LoadScene(sceneName);
        }
    }

    public void LoadScene(string sceneName)
    {
        if (nameSet)
        {
            CurrencyManager.resetRoundMoney();

            GameScoreManager.currentDiff = GameScoreManager.difficulty.nothing;//Maarten: When we load a level, we always play it without a difficulty
            if (sceneName.Equals("Menu")){
                SceneManager.LoadScene(0);   
                Debug.Log("Scene 0 Loaded");
            }else{
                SceneManager.LoadScene(sceneName);
            }
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

    public void openText()
    {
        if (nameSet)
        {
            text.SetActive(true);
        }
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

        //Maarten: load random position, very hacky approach
        LevelPositions curLevelData=null;
        foreach(LevelPositions l in levelData.lData) //sadly we can't just save the LevelPos as Listdata, since problems with UI elemnts
        {
            if (l.levelName.Equals(levels[r]))
            {
                curLevelData = l;
                break;
            }
        }

        bool posFound = false;
        while (!posFound)
        {
            int rPos = Random.Range(0, curLevelData.spawnPos.Count);
            if (curLevelData.unlockedPos[rPos])
            {
                SpawnCameraOnRandPos.currentSpawnPos = curLevelData.spawnPos[rPos];
                posFound = true;
                break;
            }
        }

        SceneManager.LoadScene(levels[r]);
        
    }

    public static bool deactivate = false;
    
    //------------------------------------------------------------------------
    //                    used extern by buttons
    //------------------------------------------------------------------------
    public void openPauseMenu()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive( true);
        ingamePauseMenu.playFadeIn();
    }
    
    public void Continue()
    {
        ingamePauseMenu.playFadeOut();

        deactivate = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
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
        playerName = Username;
    }

    public void SetVolume()
    {
        Debug.Log("SetVolume");
        SoundManager.MasterVolume = slider.value;
        PlayerPrefs.SetFloat("MasterVolume", SoundManager.MasterVolume);
    }
}
