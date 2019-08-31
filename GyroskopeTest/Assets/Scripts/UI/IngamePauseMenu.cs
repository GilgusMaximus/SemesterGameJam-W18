using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngamePauseMenu : MonoBehaviour
{

	[SerializeField]
	private Animator pauseMenuAnimator;
	
	//hash values for the animation triggers -> faster lookup of the trigger when setTrigger() is called
	private int playFadeInId, playFadeOutId, playNextLevelId;
	
	// Use this for initialization
	void Start () {
		playFadeInId = Animator.StringToHash("FadeIn");
		playFadeOutId = Animator.StringToHash("FadeOut");
	}

	public void playFadeIn(){
		pauseMenuAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
		pauseMenuAnimator.SetTrigger(playFadeInId);
	}

	public void playFadeOut(){
		pauseMenuAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
		pauseMenuAnimator.SetTrigger(playFadeOutId);
	}
	
	
	
}
