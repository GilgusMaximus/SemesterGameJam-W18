using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProgressMenu : MonoBehaviour {

    public Text MoneyTotalText;

    //TODO: install a subscriber that updates the current and requried stability values

	// Use this for initialization
	void Start () {
        MoneyTotalText.text = "and have " + CurrencyManager.currentMoney + " in total to spend.";
        CurrencyManager.resetRoundMoney();

    }
	
	// Update is called once per frame
	void Update () {
        MoneyTotalText.text = CurrencyManager.currentMoney + " in total to spend";
       
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
