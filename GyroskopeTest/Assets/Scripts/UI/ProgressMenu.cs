﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ProgressMenu : MonoBehaviour {

    public TMP_Text MoneyTotalText;

    //TODO: install a subscriber that updates the current and requried stability values
    public List<Text> level1;
    public List<Text> level2;
    public List<Text> level3;

    

	// Use this for initialization
	void Start () {
        MoneyTotalText.text = "and have " + CurrencyManager.currentMoney + " in total to spend.";
        CurrencyManager.resetRoundMoney();

        updateValuesOnScreen();

        foreach(Button button in FindObjectsOfTypeAll(typeof(Button)))//add a listener that updates the UiValues whenever we click a button
        {
            button.onClick.AddListener(updateValuesOnScreen);
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        MoneyTotalText.text = CurrencyManager.currentMoney + " in total to spend";
    }

    public void updateValuesOnScreen()//gets desired information from savefile and updates the ui elements
    {
        LevelData d = SaveSystem.LoadData();

        printValue(level1, d, 0);
        printValue(level2, d, 1);
        printValue(level3, d, 2);

    }

    public void printValue(List<Text> level, LevelData d, int i)
    {
        if (d.lData[i].unlocked)
        {
            level[0].text = "Already unlocked";
        }
        else
        {
            level[0].text = "Unlock: " + d.lData[i].curStab + "/" + d.lData[i].reqStab + "\n100 money to increment";
        }

        if (d.lData[i].unlockedPos.Count > 1 && d.lData[i].unlockedPos[1])
        {
            level[1].text = "Pos 1 unlocked!";
        }
        else
        {
            level[1].text = "Pay 500 money to unlock optional Position 1";
        }

        if (d.lData[i].unlockedPos.Count > 2 && d.lData[i].unlockedPos[2])
        {
            level[2].text = "Pos 2 unlocked!";
        }
        else
        {
            level[2].text = "Pay 500 money to unlock optional Position 2";
        }

        //level[1].text = "Pos 1: " + "not yet implemented";
        //level[2].text = "Pos 2: " + "not yet implemented";
    }


    public void activePanel(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void deactivatePanel(GameObject obj)
    {
        obj.SetActive(false);
    }

}
