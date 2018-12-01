using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : MonoBehaviour {

    public int health;
    public Vector3 TreasurePoint;
    public float TreasurePointDiffrence;
    public bool hasTreasure;

    public GameObject Treasure;

    private AudioSource audioSource;

    public AudioClip miningSound;
    public AudioClip DestroySoundNothing;
    public AudioClip DestroySoundTreasure;

    public ParticleSystem MiningParticle;
    public ParticleSystem DestroyParticle;

    public int index;
    private GameObject tr;
    private bool instantiated;

    public AudioClip Explosion;


    // Use this for initialization
    void Start () {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);

        TreasurePoint = this.transform.position + new Vector3(x, y, z).normalized*Random.Range(0,TreasurePointDiffrence);
        audioSource = GameObject.Find("Audio").GetComponent<AudioSource>();
        if (Treasure != null)
        {
            tr = Instantiate(Treasure, TreasurePoint, Quaternion.identity);
        }

    }
	
	// Update is called once per frame
	void Update () {


     
	}

    public void ReduceHealth()
    {
        health--;

        if (health <= 0)
        {
            DestroySelf();
            return;
        }
        Instantiate(MiningParticle, this.transform.position, Quaternion.identity);
        audioSource.clip = miningSound;
        audioSource.Play();


    }

    public void DestroySelf()
    {
        if (hasTreasure)
        {
             
            //  Instantiate(MiningParticle, this.transform.position, Quaternion.identity);
            // Instantiate(DestroyParticle,this.transform.position, Quaternion.identity);
            //noch Randomisieren?
            tr.GetComponent<Treasure>().discovered = true;

            GameScoreManager.addScore(Treasure.GetComponent<Treasure>().Wert);//TODO add specific score

            TimeDynamite t = Treasure.GetComponent<TimeDynamite>();//invoke special behaviour for time or dynamite


            if (t == null)
            {
                audioSource.clip = DestroySoundTreasure;
                audioSource.Play();
            }

            if (t!=null)
            {
                if (!t.istime)
                {
                    audioSource.clip = Explosion;
                    audioSource.Play();
                }
                t.apply();
            }

            Instantiate(DestroyParticle, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            return;

        }

        Instantiate(DestroyParticle, this.transform.position, Quaternion.identity);

        audioSource.clip = DestroySoundNothing;
        audioSource.Play();

        Destroy(this.gameObject);

    }

}
