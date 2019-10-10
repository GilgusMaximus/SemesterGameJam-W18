using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class StartMenu3D : MonoBehaviour {

    public List<string> levels;
    LevelData levelData;

    public static string playerName;
    public string Username = "-435897gbfhjsdf28737";
    public GameObject UsernameInput;
    public GameObject initialUserNameInput;
    private static bool nameSet = true;

    public GameObject initial;
    
    public Slider VolumeSlider;
    public AudioSource menuMusic;

    // Use this for initialization
    void Start () {

        //Maarten: load all possible levels for highscore run
        levelData = SaveSystem.LoadData();
        if (levelData == null || levelData.levels == null)
        {
            levels = new List<string> { "Level1" };
        }
        else
        {
            levels = levelData.levels;
        }


        //load the username
        Username = PlayerPrefs.GetString("Username", "-435897gbfhjsdf28737");
        if (Username.Equals("-435897gbfhjsdf28737") && !nameSet)
        {
            initial.SetActive(true);
        }
        else
        {
            nameSet = true;
            playerName = Username;
        }

        //set the colume slider on correct value
        if (VolumeSlider != null)
        {
            VolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1);
        }

    }
    
    

    public void playPressed(){
        Debug.Log(StartMenuUIController.currentSelectedLocation);
        LoadLevel(StartMenuUIController.currentSelectedLocation);
    }
	
    public void LoadLevel(int position)
    {
        Debug.Log("NameSet: " + nameSet);
        if (nameSet)
        {
            CurrencyManager.resetRoundMoney();
            GameScoreManager.currentDiff = GameScoreManager.difficulty.nothing;//Maarten: When we load a level, we always play it without a difficulty
            Time.timeScale = 1;

            //parsing args
            int pos = position;//int.Parse(args.Substring(1));
            int level = StartMenuUIController.currentModelIndex;//Watch out for bugs here, selected in menu
            string sceneName = levelData.lData[level].levelName;

            if (levelData.lData[level].unlocked && pos < levelData.lData[level].unlockedPos.Count && levelData.lData[level].unlockedPos[pos])
            {
                SpawnCameraOnRandPos.currentSpawnPos = levelData.lData[level].spawnPos[pos];
                SceneManager.LoadScene(sceneName);
            }
            
        }

    }

    public bool isLevelUnlocked(int level)
    {
        return levelData.lData[level].unlocked;
    }

    public bool isLocationUnlocked(int level, int location)
    {
        return levelData.lData[level].unlockedPos[location];
    }
    
    public void SetDiffNormal()
    {
        if (nameSet)
        {
            GameScoreManager.currentDiff = GameScoreManager.difficulty.normal;
            LoadRandomLevel();
        }
    }
    public void SetDiffHard()
    {
        if (nameSet)
        {
            GameScoreManager.currentDiff = GameScoreManager.difficulty.hard;
            LoadRandomLevel();
        }
    }

    private void LoadRandomLevel()
    {
        int r = Random.Range(0, levels.Count);

        //Maarten: load random position, very hacky approach
        LevelPositions curLevelData = null;
        foreach (LevelPositions l in levelData.lData) //sadly we can't just save the LevelPos as Listdata, since problems with UI elemnts
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

    public void setUsername()
    {
        Username = UsernameInput.GetComponent<TMPro.TMP_InputField>().text;
        Debug.Log("Username: " + Username);
        if (Username.Equals(""))
        {
            Username = "Anomyous_Owl";
        }
        PlayerPrefs.SetString("Username", Username);

        nameSet = true;
        playerName = Username;
    }

    public void SetVolume()
    {
        SoundManager.MasterVolume = VolumeSlider.value;
        PlayerPrefs.SetFloat("MasterVolume", SoundManager.MasterVolume);
    }

    public void mute()//still need audio source in scene
    {
        if (menuMusic.mute)
        {
            menuMusic.mute = false;
        }
        else
        {
            menuMusic.mute = true;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
