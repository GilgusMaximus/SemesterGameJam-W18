using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class StartMenuUIController : MonoBehaviour
{
	
	[SerializeField]
	private Animator playButtonAnimator;

	[SerializeField]
	private Animator[] startupMenu;
	
	//TODO Node: im new state zwischen FadeIn und FadeOut muss Write defaults aus sein

	private void Start()
	{
		
	}

	public void triggerFadeOut()
	{

		//trigger reset to false after being activated
		for(int i = 0; i < startupMenu.Length; i++)
			startupMenu[i].SetTrigger("PlayFadeOut");
		
		
	}
	
	public void triggerFadeIn()
	{

		//trigger reset to false after being activated
		for(int i = 0; i < startupMenu.Length; i++)
			startupMenu[i].SetTrigger("PlayFadeIn");
		
		
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.A))
			triggerFadeIn();
	}
}
