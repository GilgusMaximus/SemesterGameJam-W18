using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDynamite : MonoBehaviour {

    public bool istime;
    public int time;//in seconds
#if UNITY_ANDROID && !UNITY_EDITOR
public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
#endif

  

    public static void Vibrate(long milliseconds)
    {
        if (isAndroid())
        {
            vibrator.Call("vibrate", milliseconds);
        }
        else
        {
            Handheld.Vibrate();
        }
    }

    private static bool isAndroid()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return true;
#else
        return false;
#endif
    }


    public void apply()  //zeit wird abgezofen oder hinzugefügt
    {


      
        if (istime)
        {
            GameScoreManager.rtime += time;   
        }
        else
        {
            GameScoreManager.rtime -= time;
            Vibrate(20);
        }

    }
	
}
