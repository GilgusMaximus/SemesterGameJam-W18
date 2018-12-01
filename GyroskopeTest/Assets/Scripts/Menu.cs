﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public static string playerName;

    public InputField input;
    public GameObject text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

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
        SceneManager.LoadScene(sceneName);
    }

    public void openText()
    {
        text.SetActive(true);

    }

}
