using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LightFlickering : MonoBehaviour{

	private float intensity;
	private float startIntensity;
	public float minIntensity = 5f, maxIntensity = 15f;
	private bool flickering = false;
	public float minTimeBetweenFLicks = 2f;
	private float remainingTimeTillFLickering;
	
	// Use this for initialization
	void Start () {
		startIntensity = intensity = GetComponent<Light>().intensity;
		remainingTimeTillFLickering = minTimeBetweenFLicks;
	}
	
	// Update is called once per frame
	void Update () {
		if (remainingTimeTillFLickering < 0) {
			int value = Random.Range(-1, 1);
			if (value < 0) {
				intensity -= Random.Range(1, 5) * 0.1f;
				if (intensity < minIntensity) {
					intensity = startIntensity-2;
				}
			} else {
				intensity += Random.Range(1, 5) * 0.1f;
				if (intensity > maxIntensity)
					intensity = startIntensity+2;
			}

			GetComponent<Light>().intensity = intensity;
			remainingTimeTillFLickering = minTimeBetweenFLicks;
		} else {
			remainingTimeTillFLickering -= Time.deltaTime;
		}
	}
}
