using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static float MasterVolume;
    AudioSource[] audios;
    float[] volume;

    public bool options;
    // Use this for initialization
    void Start () {

       MasterVolume= PlayerPrefs.GetFloat("MasterVolume",1);
        audios = FindObjectsOfType<AudioSource>();
        volume = new float[audios.Length];

        for (int i = 0; i< volume.Length; i++)
        {
            volume[i] = audios[i].volume;
        }
        for (int i = 0; i < volume.Length; i++) //vllt nur wenn man sich im optionsmenu befindet
        {
            audios[i].volume = volume[i]*MasterVolume;
        }


    }
	
	// Update is called once per frame
	void Update () {
        if (options)
        {
            for (int i = 0; i < volume.Length; i++) //vllt nur wenn man sich im optionsmenu befindet
            {
                audios[i].volume = volume[i]*MasterVolume;
            }
        }
    }
  
}
