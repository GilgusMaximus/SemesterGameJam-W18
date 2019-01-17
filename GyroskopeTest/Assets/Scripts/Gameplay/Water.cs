using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour{


	[SerializeField]
	private GameObject waterPlane;

	[SerializeField] private ParticleSystem particleSystem;
	
	[SerializeField] private float waterFlowingSpeed, minLifeTimeWater, maxLifeTimeWater, damagePerSecond;
	private float waterLevel;
	private bool isFLowing, waited10S; 
	// Use this for initialization
	void Start () {
		waterLevel = 0f;
		isFLowing = false;
		
	}

	IEnumerator  wait10Seconds() {
		yield return new WaitForSeconds(10);
	}
	
	// Update is called once per frame
	void Update () {
		if (!waited10S) {
			wait10Seconds();
		}
			
		Debug.Log("Letzts go");
	}
}
