using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControl : MonoBehaviour {

    //witty comment here

    private bool gEnabled;//since not every device has a gyroscope
    private Gyroscope myGyro;//actual gyroscope which lets us get the attitude

    private GameObject cameraContainer;

   // private Quaternion prevRot=new Quaternion(0,0,0,0);

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

            if (gEnabled&&Time.timeScale!=0)
            {
            //cameraContainer.transform.Rotate(0,-myGyro.rotationRateUnbiased.y,0); //ALt: Rotate(), dies verursacht ungenauigkeiten
            //transform.Rotate(-myGyro.rotationRateUnbiased.x, 0, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, GyroToUnity(myGyro.attitude), 60 * Time.deltaTime);

            //transform.rotation = v1;//GyroToUnity(myGyro.attitude);//Neu: rotation=Quaternion
            transform.Rotate(90f,0,0);

           // prevRot = transform.rotation;

            }

	}

    private bool enableGyro()//checks and sets the gyroscope
    {
        if (SystemInfo.supportsGyroscope)
        {
            myGyro = Input.gyro;//sets our gyroscpe to the default gyro of device
            myGyro.enabled = true;//activate it

            //transform.rotation = GyroToUnity(myGyro.attitude);//ALt: set offset zu anfang
            //transform.Rotate(90f, 0, 0);//solves issue that gyro's z axis is same as unity z axis

            return true;
        }
        return false;
    }

}
