using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProgressMenu : MonoBehaviour {

    public Text RoundMoneytext;
    public Text MoneyTotalText;
    public Text stabilitätsText;




	// Use this for initialization
	void Start () {
        stabilitätsText.text = "" + CurrencyManager.stabilität + "/" + CurrencyManager.getStabilitätsRequirement();
        RoundMoneytext.text = "You made " + CurrencyManager.RoundMoney + " this round!";
        MoneyTotalText.text = "and have " + CurrencyManager.currentMoney + " in total to spend.";
        CurrencyManager.resetRoundMoney();

        Debug.Log("Hehe" + CurrencyManager.CurrentLevelToUnlock);

    }
	
	// Update is called once per frame
	void Update () {
        MoneyTotalText.text = "and have " + CurrencyManager.currentMoney + " in total to spend";
        stabilitätsText.text = "" + CurrencyManager.stabilität + "/" + CurrencyManager.getStabilitätsRequirement();
        if(CurrencyManager.CurrentLevelToUnlock >= CurrencyManager.stabilitätsReq.Length)
        {
            stabilitätsText.text = "None";
        }
    }
}
