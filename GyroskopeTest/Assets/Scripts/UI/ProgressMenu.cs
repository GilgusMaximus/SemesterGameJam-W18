using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ProgressMenu : MonoBehaviour {

    public TMP_Text MoneyTotalText;

    //TODO: install a subscriber that updates the current and requried stability values
    public List<Text> level1;
    public List<Text> level2;
    public List<Text> level3;


    [SerializeField]
    private Button[] mineButtons;

    private Button currentlySelectedMine;
    private int currentMine = 0;
    
    
    [SerializeField]
    private Image[] mineButtonsClikedUI;

    [SerializeField] 
    private GameObject[] locationButtons;
    
    private static Color32 unselectedButtonColor = new Color32(245, 245, 245, 255), selectedButtonColor = new Color32(31, 159, 23, 255), selectedLockedButtonColor = new Color32(231, 19, 23, 255), unavailableButtonColor = new Color32(168,168,168,255);

	// Use this for initialization
	void Start () {
        MoneyTotalText.text = "and have " + CurrencyManager.currentMoney + " in total to spend.";
        CurrencyManager.resetRoundMoney();

        updateValuesOnScreen();

        foreach(Button button in FindObjectsOfTypeAll(typeof(Button)))//add a listener that updates the UiValues whenever we click a button
        {
           // button.onClick.AddListener(updateValuesOnScreen);
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        MoneyTotalText.text = CurrencyManager.currentMoney + " in total to spend";
    }

    public void updateValuesOnScreen()//gets desired information from savefile and updates the ui elements
    {
        LevelData d = SaveSystem.LoadData();
        Debug.Log("PAAAAATH" + Application.persistentDataPath);
        printValue(level1, d, 0);
        printValue(level2, d, 1);
        printValue(level3, d, 2);

    }

    public void printValue(List<Text> level, LevelData d, int i)
    {
      
        
        if (d.lData[i].unlocked)
        {
            Debug.Log(i + "unlocked");
            Button currentButton = mineButtons[i];
            currentButton.interactable = true;
            
            ColorBlock buttonColors;
		
            buttonColors = currentButton.colors;
            buttonColors.highlightedColor = unselectedButtonColor;
            buttonColors.normalColor = unselectedButtonColor;
            currentButton.colors = buttonColors;
            
            
            level[0].text = "Already unlocked";
        }
        else
        {
            //Buttons dunkel grau machen und nicht klickbar
            Button currentButton = mineButtons[i];
            currentButton.interactable = false;
            
            ColorBlock buttonColors;
		
            buttonColors = currentButton.colors;
            buttonColors.highlightedColor = selectedLockedButtonColor;
            buttonColors.normalColor = selectedLockedButtonColor;
            currentButton.colors = buttonColors;
            
            level[0].text = "Unlock: " + d.lData[i].curStab + "/" + d.lData[i].reqStab + "\n100 money to increment";
        }

        if (d.lData[i].unlockedPos.Count > 1 && d.lData[i].unlockedPos[1])
        {
                TMP_Text[] texts = locationButtons[0].GetComponentsInChildren<TMP_Text>();
                texts[0].faceColor = unavailableButtonColor;
                texts[1].enabled = true;
         

            level[1].text = "Pos 1 unlocked!";
        }
        else
        {
            if (currentMine > 0)
            {
                TMP_Text[] texts = locationButtons[0].GetComponentsInChildren<TMP_Text>();

                texts[0].color = unselectedButtonColor;
                texts[1].enabled = false;
            }
            else
            {
                TMP_Text[] texts = locationButtons[0].GetComponentsInChildren<TMP_Text>();
                texts[0].color = unavailableButtonColor;
                texts[1].enabled = true;
                Button currentButton = locationButtons[0].GetComponent<Button>();
                currentButton.interactable = false;
            
                ColorBlock buttonColors;
		
                buttonColors = currentButton.colors;
                buttonColors.highlightedColor = selectedLockedButtonColor;
                buttonColors.normalColor = selectedLockedButtonColor;
                currentButton.colors = buttonColors;
            }

            level[1].text = "Pay 500 money to unlock optional Position 1";
        }

        if (d.lData[i].unlockedPos.Count > 2 && d.lData[i].unlockedPos[2])
        {
            level[2].text = "Pos 2 unlocked!";
        }
        else
        {
            if (currentMine > 0)
            {
                TMP_Text[] texts = locationButtons[1].GetComponentsInChildren<TMP_Text>();
                texts[0].color = unselectedButtonColor;
                texts[1].enabled = false;
            }
            else
            {
                TMP_Text[] texts = locationButtons[1].GetComponentsInChildren<TMP_Text>();
                texts[0].color = unavailableButtonColor;
                texts[1].enabled = true;
                Button currentButton = locationButtons[1].GetComponent<Button>();
                currentButton.interactable = false;
            
                ColorBlock buttonColors;
		
                buttonColors = currentButton.colors;
                buttonColors.highlightedColor = selectedLockedButtonColor;
                buttonColors.normalColor = selectedLockedButtonColor;
                currentButton.colors = buttonColors;
            }
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


    public void mineButtonClicked(int buttonId)
    {
        currentMine = buttonId;
        updateValuesOnScreen();
        ColorBlock buttonColors;
        if (currentlySelectedMine != null){
            buttonColors = currentlySelectedMine.colors;
            buttonColors.highlightedColor = unselectedButtonColor;
            buttonColors.normalColor = unselectedButtonColor;
            currentlySelectedMine.colors = buttonColors;
        }
        
        currentlySelectedMine = mineButtons[buttonId];
		
        buttonColors = currentlySelectedMine.colors;
        buttonColors.highlightedColor = selectedButtonColor;
        buttonColors.normalColor = selectedButtonColor;
        currentlySelectedMine.colors = buttonColors;

        
        //deactivate lines
        for (int i = 0; i < mineButtonsClikedUI.Length - 3; i++)
        {
            mineButtonsClikedUI[i].enabled = false;
        }
        //reenable line which were deactivated
        Debug.Log(mineButtonsClikedUI[buttonId*2].enabled + " " + buttonId*2);
        mineButtonsClikedUI[buttonId * 2].enabled = true;
        
        Debug.Log(mineButtonsClikedUI[buttonId*2].enabled + " " + buttonId*2);
        mineButtonsClikedUI[buttonId * 2 + 1].enabled = true;
        //activate the lines to the location
        if (!mineButtonsClikedUI[mineButtonsClikedUI.Length - 1].enabled)
        {
            mineButtonsClikedUI[mineButtonsClikedUI.Length - 1].enabled = true;
            mineButtonsClikedUI[mineButtonsClikedUI.Length - 2].enabled = true;
            mineButtonsClikedUI[mineButtonsClikedUI.Length - 3].enabled = true;
        }

        locationButtons[0].SetActive(true);
        locationButtons[1].SetActive(true);
        locationButtons[2].SetActive(true);
    }
    
}
