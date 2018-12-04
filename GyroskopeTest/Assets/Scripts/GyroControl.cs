using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControl : MonoBehaviour {

    //witty comment here

    private bool gEnabled;//since not every device has a gyroscope
    private Gyroscope myGyro;//actual gyroscope which lets us get the attitude

    //stuff to test out gyro
    private GameObject cameraContainer;

	void Start () {

        cameraContainer = new GameObject("CameraContainer");
        cameraContainer.transform.position = transform.position;//putting script on camera to look around

        transform.SetParent(cameraContainer.transform);

        gEnabled = enableGyro();
	}
	
	void Update () {

        if (gEnabled)
        {
            //TODO stabelizer
            cameraContainer.transform.Rotate(0,-myGyro.rotationRateUnbiased.y,0);
            transform.Rotate(-myGyro.rotationRateUnbiased.x, 0, 0);
        }

	}

    private bool enableGyro()//checks and sets the gyroscope
    {
        if (SystemInfo.supportsGyroscope)
        {
            myGyro = Input.gyro;//sets our gyroscpe to the default gyro of device
            myGyro.enabled = true;//activate it

            cameraContainer.transform.rotation = myGyro.attitude; //TODO test

            return true;
        }
        return false;
    }

}
