using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroSetup : MonoBehaviour {//This script just enables the system gyro early in order to avoid bugs in the play scene

	// Use this for initialization
	void Start () {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
	}
	
}
