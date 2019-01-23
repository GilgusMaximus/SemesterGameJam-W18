using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rocks : MonoBehaviour {
    [Tooltip("das leben")]
    public int health; 
    [Tooltip("der Punkt an dem der Stein gespawnt wird")]
    private Vector3 TreasurePoint; 
    [Tooltip("der radius des kreises in dem der treasurePoint liegen kann")]
    public float TreasurePointDiffrence; 
    [Tooltip("ob der stein einen treasure enthält")]
    public bool hasTreasure;  
    [Tooltip("der schatz")]
    public GameObject Treasure;

    private AudioSource audioSource;

    public AudioClip miningSound;
    public AudioClip DestroySoundNothing;  //sounds vllt auf treasure auslagern
    public AudioClip DestroySoundTreasure;

    public ParticleSystem MiningParticle;
    public ParticleSystem DestroyParticle;

    //public int index;
    private GameObject tr;
    //private bool instantiated;
    private Water WaterScript;
    
    public AudioClip Explosion;


    // Use this for initialization
    void Start () {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);

        TreasurePoint = this.transform.position + new Vector3(x, y, z).normalized*Random.Range(0,TreasurePointDiffrence);  //der punkt bei dem der schatz gespawnt wird wird zufällig gesetzt
        audioSource = GameObject.Find("Audio").GetComponent<AudioSource>();  // die audioSource in der Scene wird gefunden          //!!!!!TODO:müssen das anders machen mit mehreren Soundsourcen,damit mehrere töne gleichzeitig abgespielt werden können
        if (hasTreasure) //wenn es einen schatz gibt
        {
            tr = Instantiate(Treasure, TreasurePoint, Quaternion.identity); //spawne ihn
        }

        WaterScript = GameObject.FindWithTag("MainCamera").GetComponent<Water>();
    }
	
	// Update is called once per frame
	

    public void ReduceHealth() //der stein nimmt schaden
    {
        health--; //leben runtersetzen

        if (health <= 0) //wenn leben kleiner null wird das objekt zerstört
        {
            DestroySelf();
            return;
        }
        Instantiate(MiningParticle, this.transform.position, Quaternion.identity); //  mining particle erzeugen
        audioSource.clip = miningSound;
        audioSource.Play();  // mining sound spielen


    }

    public void DestroySelf() // der stein wird zerstört
    {
        if(WaterScript != null)
            WaterScript.spawnWater(transform);
        if (hasTreasure) //wenn es einen treasure gibt
        {
            audioSource.clip = DestroySoundTreasure;
            audioSource.Play();      //treasure sound abspielen
             
            
            
            tr.GetComponent<Treasure>().discovered = true;   //den schatz auf discovered setzen(damit er anfängt zum spieler zu fliehen)


            //vllt noch unterscheiden ob man in highscorerun ist, oder nicht
            GameScoreManager.addScore(Treasure.GetComponent<Treasure>().Wert);//score added
            CurrencyManager.incrementMoney(Treasure.GetComponent<Treasure>().Wert);  //money added
            CurrencyManager.incrementRoundMoney(Treasure.GetComponent<Treasure>().Wert);  //money added

            TimeDynamite t = Treasure.GetComponent<TimeDynamite>();//invoke special behaviour for time or dynamite

            if (t != null)
            {
                if (!t.istime) //wenn dynamite
                {
                    audioSource.clip = Explosion;
                    audioSource.Play(); //explosionsaudio wird abgespielt 
                    ParticleSystem ps = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<ParticleSystem>();
                    ps.playbackSpeed = 1f;
                    ps.Play();
                    //vibration auslösen
                }
                t.apply(); //dynamite/clock behaviour auslösen
            }

            Instantiate(DestroyParticle, this.transform.position, Quaternion.identity);  //particle erzeugen
            Destroy(this.gameObject); //Stein zerstören
         

        }
        else//wenn kein treasure 
        {

            Instantiate(DestroyParticle, this.transform.position, Quaternion.identity);   //particle effect erzeugen

            audioSource.clip = DestroySoundNothing;
            audioSource.Play();  //sound abspielen

            Destroy(this.gameObject);  //zerstören
        }
    }

}
