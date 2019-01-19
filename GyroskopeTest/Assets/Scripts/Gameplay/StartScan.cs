using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScan : MonoBehaviour {

    public GameObject scanner;
    public Image image;
    public Button button;
    public float scanTime = 3.0f;
    public float scanOfftime = 2.0f;
    private float scanTimeLeft;
    private float scanOfftimeLeft;
    public static bool scannerActive = false;

	// Use this for initialization
	void Start () {
        button.enabled = true;
        scanTimeLeft = scanTime;
        scanOfftimeLeft = scanOfftime;
        scanner.SetActive(false);
        scannerActive = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.touchCount == 2 && button.enabled) {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            if (touchZero.deltaPosition.y > 0f && touchOne.deltaPosition.y > 0f) {
                Startscan();
            }
        }


        if (scanner.activeSelf) {
            scanTimeLeft -= Time.deltaTime;
            image.fillAmount -= Time.deltaTime / scanTime;
            if(scanTimeLeft < 0) {
                scanner.SetActive(false);
                scannerActive = false;
                scanTimeLeft = scanTime;
            }
        }
        else {
            if (!button.enabled) {
                scanOfftimeLeft -= Time.deltaTime;
                image.fillAmount += Time.deltaTime / scanOfftime;
                if (scanOfftimeLeft < 0) {
                    scanOfftimeLeft = scanOfftime;
                    button.enabled = true;
                }

            }
        }
	}

    public void Startscan() {
        button.enabled = false;
        scanner.SetActive(true);
        scannerActive = true;
    }
}
