using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControl : MonoBehaviour {

    //witty comment here

    private bool gEnabled;//since not every device has a gyroscope
    private Gyroscope myGyro;//actual gyroscope which lets us get the attitude

    private GameObject cameraContainer;

    public float sens = 60f;//var for accelerating or slowing the look around-motion

	void Start () {

        cameraContainer = new GameObject("CameraContainer");
        cameraContainer.transform.position = transform.position;//putting script on camera to look around

        transform.SetParent(cameraContainer.transform);

        gEnabled = enableGyro();

    }

    Quaternion GyroToUnity(Quaternion q) //covert gyro left hand coords to unity right hand coords
    {
        return new Quaternion(q.x,q.z,q.y,-q.w);
    }
	
	void Update () {

            if (gEnabled)//&&Time.timeScale!=0)
            {
            cameraContainer.transform.Rotate(0,-myGyro.rotationRateUnbiased.y*sens*Time.deltaTime,0); //use Rotate() with gyroRotation, smooting with sens and deltatime
            transform.Rotate(-myGyro.rotationRateUnbiased.x*sens*Time.deltaTime, 0, 0);

            // transform.rotation = Quaternion.Slerp(transform.rotation, GyroToUnity(myGyro.attitude), 60 * Time.deltaTime); //this is also a very good approach to gyro, 
                                                                                                                             //but setting the rotation directly results 
                                                                                                                             //in errors for specific values

            //transform.Rotate(90f,0,0);

            }

	}

    private bool enableGyro()//checks and sets the gyroscope
    {
        if (SystemInfo.supportsGyroscope)
        {
            myGyro = Input.gyro;//sets our gyroscpe to the default gyro of device
            myGyro.enabled = true;//activate it

            transform.rotation = GyroToUnity(myGyro.attitude);//setting the initial rot to gyro attitude
            transform.Rotate(90f, 0, 0);//solves issue that gyro's z axis is same as unity z axis

            return true;
        }
        return false;
    }

}
