using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MicrophoneInput : MonoBehaviour {

    public static float MicLoudness;
    bool isInit;
    AudioSource myAudio;
    [SerializeField] private ParticleSystem ps;
    bool isWhisteling;

    public Text debugText;
    int n = 0;


    private void OnEnable()
    {
        InitMic();
    }

    private void OnDisable()
    {
        StopMicrophone();
    }

    void StopMicrophone()
    {
        Microphone.End(null);
    }

    void InitMic()
    {
        myAudio = GetComponent<AudioSource>();
        myAudio.clip = Microphone.Start(null, true, 10, 44100);
        //myAudio.loop = true;
        while (!(Microphone.GetPosition(null) > 0))
        { }

        myAudio.Play();
        isInit = true;
    }

    private void Update()
    {
        MicLoudness = RMS();
        //debugText.text = MicLoudness.ToString();
        Analyze();
    }

    private float RMS()
    {
        float oResult = 0.0f;
        float[] wavedata = new float[128];
        int mSamplePos = Microphone.GetPosition(null) - wavedata.Length;

        if (isInit)
        {
            //mSamplePos = mSamplePos - wavedata.Length;
            if (mSamplePos < 0)
            {
                mSamplePos = 0;
            }

            myAudio.clip.GetData(wavedata, mSamplePos);//befuellen der wavedata

            float sum = 0;
            for (int i = 0; i < wavedata.Length; i++)//determine root mean square
            {
                sum += Mathf.Pow(wavedata[i],2);
            }

            //oResult *= (1 / wavedata.Length);
            //oResult = Mathf.Sqrt(oResult);
            oResult = Mathf.Sqrt(sum / wavedata.Length);

        }

        return oResult;
    }

    private void Analyze()
    {
        if (!isWhisteling && MicLoudness>0.35)
        {
            //n++;
           // debugText.text = n.ToString();
            isWhisteling = true;
            if(ps.isPlaying)
              ps.playbackSpeed *= 1.5f;
            //TODO event here
        }
        if (isWhisteling && MicLoudness<=0.25)
        {
            isWhisteling = false;
        }
    }

}