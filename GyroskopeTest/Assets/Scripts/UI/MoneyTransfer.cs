using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyTransfer : MonoBehaviour
{


	[SerializeField] 
	private TMP_Text totalCash, earnedCash, totalCashDummy;

	private int earnedCashInt, totalCashInt;
	
	[SerializeField] 
	private string earnedCashString;

	private int a = 0;
	
	private void Start(){
		earnedCashInt = CurrencyManager.addedMoney;
		CurrencyManager.addedMoney = 0;
		Debug.Log("ADDED MONEY " + earnedCashInt);
		totalCashInt = CurrencyManager.currentMoney-earnedCashInt;
		totalCash.color = new Color32(0, 0, 0, 0);
		
	}

	// Update is called once per frame
	void Update () {
		if (earnedCashInt > 0 && a == 2){
			earnedCashInt -= 10;
			earnedCash.text = earnedCashString + earnedCashInt;
			totalCashInt += 10;
			totalCashDummy.text = "" + totalCashInt;
			a = 0;
		}
		else if(earnedCashInt > 0){
			a++;
		}
	}
}
