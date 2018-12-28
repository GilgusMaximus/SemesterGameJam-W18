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

    private Vector3 lowPassValue = Vector3.zero;
    private static float AccelerometerUpdateInterval = 1.0f / 60.0f;
    private static float LowPassKernelWithInSeconds = 0.5f;
    private float LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWithInSeconds;

    void Start () {

        cameraContainer = new GameObject("CameraContainer");
        cameraContainer.transform.position = transform.position;//putting script on camera to look around

        transform.SetParent(cameraContainer.transform);

        gEnabled = enableGyro();

        lowPassValue = Input.acceleration;
    }

    Quaternion GyroToUnity(Quaternion q) //covert gyro left hand coords to unity right hand coords
    {
        return new Quaternion(q.x,q.z,q.y,-q.w);
    }
	
	void Update () {

            if (gEnabled&&Time.timeScale!=0)
            {
            //getAcceleration();

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

    /*
     * 
     * 
     */

    public bool getAcceleration()
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

        //lowPassValue = Vector3.Lerp(lowPassValue, AccVal, LowPassFilterFactor);

        float mag = AccVal.magnitude;
        AccelerometerDebug.text = mag.ToString();//debugging

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
