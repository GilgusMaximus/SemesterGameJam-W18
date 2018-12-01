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



	// Use this for initialization
	void Start () {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);

        TreasurePoint = this.transform.position + new Vector3(x, y, z).normalized*Random.Range(0,TreasurePointDiffrence);
        audioSource = this.GetComponent<AudioSource>();


	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReduceHealth();
        }
	}

    public void ReduceHealth()
    {
        health--;

        if (health <= 0)
        {
            DestroySelf();
            return;
        }
       // Instantiate(MiningParticle, this.transform.position, Quaternion.identity);
      //  audioSource.clip = miningSound;
       // audioSource.Play();


    }

    public void DestroySelf()
    {
        if (hasTreasure)
        {
            //   audioSource.clip = DestroySoundTreasure;
            //   audioSource.Play();
            //  Instantiate(MiningParticle, this.transform.position, Quaternion.identity);
            //  Instantiate(DestroyParticle,this.transform.position, Quaternion.identity);
            Instantiate(Treasure, TreasurePoint, Quaternion.identity);  //noch Randomisieren?

            Debug.Log("Haha");

            GameScoreManager.addScore(10);
            Highscores.AddNewHighscore("Test",GameScoreManager.currentScore);
        }

        Destroy(this.gameObject);
    }

}
