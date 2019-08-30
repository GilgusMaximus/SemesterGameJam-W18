using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ladder : MonoBehaviour {

	
	
	// Update is called once per frame
	


    public void Respawn()  //die scene wird neu geladen
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}
