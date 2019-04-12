using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class StartMenuUIController : MonoBehaviour
{
	
	[SerializeField]
	private Animator playButtonAnimator;

	[SerializeField]
	private Animator[] startupMenu, optionsMenu, playMenu, highscoreDisplayMenu;
	[SerializeField]
	private GameObject startUpMenuParent;
	
	[SerializeField]
	private Sprite[] muteSprites = new Sprite[2];


	[SerializeField] 
	private Image muteButtonImage;

	[SerializeField] private AudioSource backgroundMusic;

	private bool isAudioMuted = false; 
	
	private int currentMuteInt = 1;
	//------------------------------------------------------------------------
	//                          General
	//------------------------------------------------------------------------	
	
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.A))
			triggerFadeIn();
	}


	private void Start()
	{
		triggerFadeIn();
	}
		
	
	
	public void backToStartupmenu(int menuID){
		//menu 0 = play; 1 = options; 2 = credits
		Animator[] animators = null;
		switch (menuID){
			case 0: animators = playMenu;
				break;
			case 1: animators = optionsMenu;
				break;
		}

		for (int i = 0; i < animators.Length; i++){
			animators[i].SetTrigger("PlayFadeOut");
		}
		for(int i = 0; i < startupMenu.Length; i++)
			startupMenu[i].SetTrigger("PlayFadeIn");
	}
	
	//------------------------------------------------------------------------
	//                          StartupMenu
    //------------------------------------------------------------------------
    
    
    //triggers fadeOut of the startup menu and the fadeIn of the menu corresponding to menuID
	public void triggerFadeOut(int menuId)
	{
		Animator[] animators = null;

		switch (menuId){
			case 0: animators = playMenu;
				break;
			case 1: animators = optionsMenu;
				break;
		}
		//trigger reset to false after being activated
		for(int i = 0; i < startupMenu.Length; i++)
			startupMenu[i].SetTrigger("PlayFadeOut");
		
		for(int i = 0; i < animators.Length; i++)
			animators[i].SetTrigger("PlayFadeIn");
		
	}
	
	public void triggerFadeIn()
	{

		//trigger reset to false after being activated
		for(int i = 0; i < startupMenu.Length; i++)
			startupMenu[i].SetTrigger("PlayFadeIn");
		
		
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

	public void sliderValueChanged(Slider slider)
	{
		backgroundMusic.volume = slider.value/100f;
	}

	
	//------------------------------------------------------------------------
	//                          Play Menu
	//------------------------------------------------------------------------

	public void backToPlayMenu(int menuId)
	{
		Animator[] animator = null;
		switch (menuId){
			case 0: animator = highscoreDisplayMenu;
				break;
		}
		for (int i = 0; i < animator.Length; i++){
			animator[i].SetTrigger("PlayFadeOut");
		}
		for(int i = 0; i < playMenu.Length; i++)
			playMenu[i].SetTrigger("PlayFadeIn");
	}

	public void fadeInSubPlayMenu(int menuId)
	{
		Animator[] animator = null;
		switch (menuId){
			case 0: animator = highscoreDisplayMenu;
				break;
		}
		for (int i = 0; i < playMenu.Length; i++){
			playMenu[i].SetTrigger("PlayFadeOut");
		}
		for(int i = 0; i < animator.Length; i++)
			animator[i].SetTrigger("PlayFadeIn");
	}
	
	
}
