using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scanbutton : MonoBehaviour {

    public Image image;
    public Button button;
    public float RechargeTime;
    private float time;
  

	// Use this for initialization
	void Start () {
        time = 0;
        button.enabled = true;
        image.fillAmount = 1;
        
	}
	
	// Update is called once per frame
	void Update () {

        if (!button.enabled)
        {
            time = time + Time.deltaTime;
            image.fillAmount = image.fillAmount + Time.deltaTime/RechargeTime;
            Debug.Log(time);
            if (time >= RechargeTime)
            {
                time = 0;
                Debug.Log("enabling");
                button.enabled = true;
                image.fillAmount = 1;
            }
        }

	}

    public void BeginnScan(){
        //Scanmethod

        button.enabled = false;
        image.fillAmount = 0;

    }


}
