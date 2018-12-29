using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GyroControl : MonoBehaviour {

    //witty comment here

    private bool gEnabled;//since not every device has a gyroscope
    private Gyroscope myGyro;//actual gyroscope which lets us get the attitude

    private GameObject cameraContainer;

    public float sens = 60f;//var for accelerating or slowing the look around-motion

    //Accelerometer Zeugs
    public Text AccelerometerDebug;

    private int tiltcount = 0;
    private bool isTilted=false;//flag to check if we tilt the phone back

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

                if (getAcceleration())
                {
                    //TODO change level, do whatever event when the phone is tilted
                }

            //cameraContainer.transform.Rotate(0,-myGyro.rotationRateUnbiased.y*sens*Time.deltaTime,0); //use Rotate() with gyroRotation, smooting with sens and deltatime
            //transform.Rotate(-myGyro.rotationRateUnbiased.x*sens*Time.deltaTime, 0, 0);

             transform.rotation = Quaternion.Slerp(transform.rotation, GyroToUnity(myGyro.attitude), 60 * Time.deltaTime); //this is also a very good approach to gyro, 
                                                                                                                             //but setting the rotation directly results 
                                                                                                                             //in errors for specific values
             transform.Rotate(90f,0,0);

            }

	}

    public void recalibrateCamera()
    {
        transform.rotation = GyroToUnity(myGyro.attitude);//setting the initial rot to gyro attitude
        transform.Rotate(90f, 0, 0);//solves issue that gyro's z axis is same as unity z axis
    }


    public bool getAcceleration()//Function to sample the accelerometer input. returns true if the phone was tilted on the x axis
    {
      
        float fPeriod = 0.0f;
        Vector3 AccVal = Vector3.zero;
        
        foreach (AccelerationEvent ac in Input.accelerationEvents)//calculate accelerometer average for this frame
        {
            AccVal += ac.acceleration * Time.deltaTime;
            fPeriod += ac.deltaTime;
        }
        if (fPeriod != 0)
        {
            AccVal *= (1.0f / fPeriod);//average acc value for frame
        }

        AccVal = (AccVal.magnitude) > 1 ? AccVal.normalized : AccVal;//normalize
                                                                 
        //AccelerometerDebug.text = AccVal.x.ToString();//debugging

        if( isTilted && Mathf.Abs(AccVal.x) <= 0.2)//bringing phone back in upright position
        {
            isTilted = false;
        }

        if (Mathf.Abs(AccVal.x)>=0.75 && !isTilted)//tilt is detected
        {
            tiltcount++;
            AccelerometerDebug.text = "Tilts: "+tiltcount.ToString();//debugging
            isTilted = true;

            return true;
        }

        return false;
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
