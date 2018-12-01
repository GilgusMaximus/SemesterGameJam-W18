using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ladder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
	}


    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}
