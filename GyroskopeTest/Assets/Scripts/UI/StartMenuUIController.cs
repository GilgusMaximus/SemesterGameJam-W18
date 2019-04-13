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

	//Every array holds all UI Items of the corresponding menu
	[SerializeField]
	private Animator[] startupMenu, optionsMenu, playMenu, highscoreDisplayMenu;
	
	
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


	private int playFadeInId, playFadeOutId;
	
	//used for changing the mute buttons sprite (0 = muted; 1 = not muted) 
	private int currentMuteInt = 1;
	
	//------------------------------------------------------------------------
	//                          General
	//------------------------------------------------------------------------	


	private void Start(){
		playFadeInId = Animator.StringToHash("PlayFadeIn");
		playFadeOutId = Animator.StringToHash("PlayFadeOut");
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
			default: Debug.LogError("StartMenuUIController.cs: backToPlayMenu: wrong menuID");
				return;
		}
		foreach (Animator animator in animators)
			animator.SetTrigger(playFadeOutId);
		
		foreach (Animator animator in playMenu)
			animator.SetTrigger(playFadeInId);
	}

	//called when one of the sub-playMenu's is clicked
	public void fadeInSubPlayMenu(int menuId)
	{
		Animator[] animators = null;
		switch (menuId){
			case 0: animators = highscoreDisplayMenu;
				break;
			default: Debug.LogError("StartMenuUIController.cs: fadeInSubPlayMenu: wrong menuID");
				return;
		}
		
		foreach (Animator animator in playMenu)
			animator.SetTrigger(playFadeOutId);
		
		foreach (Animator animator in animators)
			animator.SetTrigger(playFadeInId);
	}
	
	
}
