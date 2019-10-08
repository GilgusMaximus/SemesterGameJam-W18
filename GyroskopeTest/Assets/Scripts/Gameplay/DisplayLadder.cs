using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DisplayLadder : MonoBehaviour {

    //public Text[] highscoreText;
    public TMPro.TextMeshProUGUI[] highscoreText;
    Highscores highscoreManager;

	// Use this for initialization
	void Start () {

        for (int i=0;i<highscoreText.Length;i++)
        {
            highscoreText[i].text = i + 1 + ". Fetching... ";
        }

        highscoreManager = GameObject.Find("Highscores").GetComponent<Highscores>();

        StartCoroutine("RefreshHighscores");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnHighscoresDownloaded(Highscore[] hList)
    {
        doFormatting(hList, 8);

        for (int i = 0; i < highscoreText.Length; i++)
        {
            highscoreText[i].text = i + 1 + ". ";
            if (hList.Length>i)
            {
                highscoreText[i].text += hList[i].username + " - " + hList[i].score;
            }
        }
    }

    public void doFormatting(Highscore[] hList, int numName)//Format the highscores so they fit in the UI
    {
        for (int i = 0; i < highscoreText.Length; i++)
        {
            if (hList[i].username.Length >= numName)
            {
                hList[i].username = hList[i].username.Substring(0, 5)+"..";
            }

        }
    }

    IEnumerator RefreshHighscores()
    {
        while (SceneManager.GetActiveScene().name=="3DStartmenu")//"HighscoreMenu")
        {
            highscoreManager.Downloadighscores();
            yield return new WaitForSeconds(30);
        }
    }

}
