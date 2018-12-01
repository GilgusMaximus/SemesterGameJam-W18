using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle : MonoBehaviour {

    private ParticleSystem system;
    private float time;
	// Use this for initialization
	void Start () {
        system = this.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        time = time + Time.deltaTime;
        if(time >= system.startLifetime)
        {
            Destroy(this.gameObject);
        }


	}
}
