using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScan : MonoBehaviour {

    public GameObject scanner;
    public float scanTime = 3.0f;
    public float scanOfftime = 2.0f;
    public bool buttonActive = true;
    private float scanTimeLeft;
    private float scanOfftimeLeft;

	// Use this for initialization
	void Start () {
        buttonActive = true;
        scanTimeLeft = scanTime;
        scanOfftimeLeft = scanOfftime;
        scanner.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(scanner.activeSelf) {
            scanTimeLeft -= Time.deltaTime;
            if(scanTimeLeft < 0) {
                scanner.SetActive(false);
                scanTimeLeft = scanTime;
                buttonActive = false;
            }
        }
        else {
            if (!buttonActive) {
                scanOfftimeLeft -= Time.deltaTime;
                if(scanOfftimeLeft < 0) {
                    scanOfftimeLeft = scanOfftime;
                    buttonActive = true;
                }
            } 
        }
	}

    void Startscan() {
        scanner.SetActive(true);
    }
}
