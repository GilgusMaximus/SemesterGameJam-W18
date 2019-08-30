using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NewGameScoreManager : MonoBehaviour
{
	
	
	public enum difficulty //je nach Schwierigkeit werden Features (de)aktiviert
	{
		nothing,normal,hard
	};
	
	private static int currentScore; 
	private float timer = 600f;		//Zeit die es in jedem Level gibt
	private float remainingTime;	//Zeit die noch übrig ist

	private bool timeIsRunning;		//Läuft die Zeit oder haben wir pausiert
	
	[SerializeField]
	private TMP_Text timeDisplayText, scoreDisplayText, moneyBalanceText;


	public static bool menuExit;

	public static difficulty currentDiff;
	//geskippte Variablen: l, cm,
	
	
	
	// Use this for initialization
	void Start (){
		menuExit = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
