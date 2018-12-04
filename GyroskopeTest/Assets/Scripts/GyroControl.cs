using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControl : MonoBehaviour {

    //witty comment here

    private bool gEnabled;//since not every device has a gyroscope
    private Gyroscope myGyro;//actual gyroscope which lets us get the attitude

    //stuff to test out gyro
    private GameObject cameraContainer;

   // private Quaternion offset;

	void Start () {

        cameraContainer = new GameObject("CameraContainer");
        cameraContainer.transform.position = transform.position;//putting script on camera to look around

        transform.SetParent(cameraContainer.transform);

        gEnabled = enableGyro();

    }

    /*Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x,q.y,-q.z,-q.w);
    }*/
	
	void Update () {

            if (gEnabled)
            {
                cameraContainer.transform.Rotate(0,-myGyro.rotationRateUnbiased.y,0);
                transform.Rotate(-myGyro.rotationRateUnbiased.x, 0, 0);

               // cameraContainer.transform.rotation = offset * GyroToUnity(myGyro.attitude); Testing this right now

            }

	}

    private bool enableGyro()//checks and sets the gyroscope
    {
        if (SystemInfo.supportsGyroscope)
        {
            myGyro = Input.gyro;//sets our gyroscpe to the default gyro of device
            myGyro.enabled = true;//activate it

            //offset = transform.rotation * Quaternion.Inverse(GyroToUnity(myGyro.attitude));
            //cameraContainer.transform.rotation = offset* GyroToUnity(myGyro.attitude);

            return true;
        }
        return false;
    }

}
