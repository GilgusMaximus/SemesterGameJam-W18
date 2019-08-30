using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChooseMenuModelSpin : MonoBehaviour {
    
    
	private Transform transform;

	[SerializeField]
	private float spinningSpeed = 5f; 
	// Use this for initialization
	void Start (){
		transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * spinningSpeed, Space.World);
	}
}
