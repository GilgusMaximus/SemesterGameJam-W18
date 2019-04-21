﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class StartMenuUIController : MonoBehaviour
{
	
	[SerializeField]
	private Animator playButtonAnimator;

	//Every array holds all UI Items of the corresponding menu
	[SerializeField]
	private Animator[] startupMenu, optionsMenu, playMenu, highscoreDisplayMenu, levelChooseMenu;

	//the 4 models displayed in the level choose menu
	[SerializeField] 
	private GameObject[] levelModels;
	
	//the 2 sprite images, which represent the mute button in the options menu - image 0 is the not muted image
	[SerializeField]
	private Sprite[] muteSprites = new Sprite[2];

	//the reference to the image object in the scene
	[SerializeField] 
	private Image muteButtonImage;

	//background music source
	[SerializeField] 
	private AudioSource backgroundMusic;

	//is false at start
	private bool isAudioMuted;	

	//hash values for the animation triggers -> faster lookup of the trigger when setTrigger() is called
	private int playFadeInId, playFadeOutId, playNextLevelId;
	
	//used for changing the mute buttons sprite (0 = muted; 1 = not muted) 
	private int currentMuteInt = 1;

	//index which model is currently displayed in level choose menu 
	private int currentModelIndex = 0;

	//the index of the location selected - 0 = location 1; 1 = location 2; -1 = no location
	private int currentSelectedLocation = -1;
	
	//which arrow is presses in the level choose menu - left = false; right = true;
	private Boolean levelChooseArrow = false;
	
	//------------------------------------------------------------------------
	//                          General
	//------------------------------------------------------------------------	


	private void Start(){
		playFadeInId = Animator.StringToHash("PlayFadeIn");
		playFadeOutId = Animator.StringToHash("PlayFadeOut");
		playNextLevelId = Animator.StringToHash("PlayNextLevel");
		triggerFadeIn();
	}
		
	
	//------------------------------------------------------------------------
	//                          StartupMenu
    //------------------------------------------------------------------------
    
    //from the options, credit and play menu, this method is called, when the back button is pressed
    public void backToStartupMenu(int menuId){
	    //get the correct Animator array
	    //menu 0 = play; 1 = options; 2 = credits
	    Animator[] animators = null;
	    switch (menuId){
		    case 0: animators = playMenu;
			    break;
		    case 1: animators = optionsMenu;
			    break;
		    default: Debug.LogError("StartMenuUIController.cs: backToStartupMenu: wrong menuID");
			    return;
	    }
	    
		//play fadeout animation for all sub-startupMenu items
		foreach (Animator animator in animators)
			animator.SetTrigger(playFadeOutId);
		
	    //play fadein animation for all startupMenu items
	    foreach (Animator animator in startupMenu)
		    animator.SetTrigger(playFadeInId);

    }
    
    //triggers fadeOut of the startup menu and the fadeIn of the menu corresponding to menuID
	public void triggerFadeOut(int menuId)
	{
		Animator[] animators = null;

		switch (menuId){
			case 0: animators = playMenu;
				break;
			case 1: animators = optionsMenu;
				break;
			default: Debug.LogError("StartMenuUIController.cs: triggerFadeOut: wrong menuID"); 
				return;
		}
		//trigger reset to false after being activated
		foreach (Animator animator in startupMenu)
			animator.SetTrigger(playFadeOutId);
		
		foreach (Animator animator in animators)
			animator.SetTrigger(playFadeInId);
		
	}
	
	private void triggerFadeIn(){
		//trigger reset to false after being activated
		foreach (Animator animator in startupMenu)
			animator.SetTrigger(playFadeInId);	
	}

	public void exitProgram()
	{
		Application.Quit();
	}
	
	//------------------------------------------------------------------------
	//                          OptionsMenu
	//------------------------------------------------------------------------

	//changes between the 2 mute images
	public void muteAudio(){
		muteButtonImage.sprite = muteSprites[currentMuteInt++];
		currentMuteInt %= 2;
		isAudioMuted = !isAudioMuted;
		backgroundMusic.mute = isAudioMuted;
	}

	//called when the sound volume slider value changes
	public void sliderValueChanged(Slider slider){
		backgroundMusic.volume = slider.value/100f;
	}

	
	//------------------------------------------------------------------------
	//                          Play Menu
	//------------------------------------------------------------------------

	//called when one of the sub-playMenu's back button is clicked
	public void backToPlayMenu(int menuId)
	{
		Animator[] animators = null;
		switch (menuId){
			case 0: animators = highscoreDisplayMenu;
				break;
			case 1: animators = levelChooseMenu;
				break;
			default: Debug.LogError("StartMenuUIController.cs: backToPlayMenu: wrong menuID");
				return;
		}
		foreach (Animator animator in animators)
			animator.SetTrigger(playFadeOutId);
		
		foreach (Animator animator in playMenu)
			animator.SetTrigger(playFadeInId);
	}

	//called when one of the sub-playMenu's is clicked
	public void fadeInSubPlayMenu(int menuId){
		Animator[] animators = null;
		switch (menuId){
			case 0: animators = highscoreDisplayMenu;
				break;
			case 1: animators = levelChooseMenu;
				foreach (GameObject model in levelModels){
					model.SetActive(false);
				}

				currentModelIndex = 0;
				levelModels[0].SetActive(true);
				break;
			default: Debug.LogError("StartMenuUIController.cs: fadeInSubPlayMenu: wrong menuID");
				return;
		}
		
		foreach (Animator animator in playMenu)
			animator.SetTrigger(playFadeOutId);
		
		foreach (Animator animator in animators)
			animator.SetTrigger(playFadeInId);
	}
	
	//function called when one of the arrow buttons is pressed 
	public void arrowPressed(Boolean arrow){
		if ((!arrow && currentModelIndex == 0)||(arrow && currentModelIndex == 3))
			return;
		levelChooseArrow = arrow;
		levelChooseMenu[6].SetTrigger(playNextLevelId);
	}
	
	//called when the fade in state for the model fade in begins - switches the active model
	public void modelFadeOutDone(){
		//corner cases do nothing
		if ((!levelChooseArrow && currentModelIndex == 0)||(levelChooseArrow && currentModelIndex == 3))
			return;
		//right arrow pressed
		if (levelChooseArrow){
			levelModels[currentModelIndex++].SetActive(false);
			levelModels[currentModelIndex].SetActive(true);
		}else{	//left arrow pressed
			levelModels[currentModelIndex--].SetActive(false);
			levelModels[currentModelIndex].SetActive(true);
		}
	}

	public void locationButtonClicked(int id){
		currentSelectedLocation = id;
	}
}