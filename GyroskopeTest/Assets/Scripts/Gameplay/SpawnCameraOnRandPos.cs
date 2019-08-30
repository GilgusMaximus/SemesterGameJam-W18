using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCameraOnRandPos : MonoBehaviour {

    public static sVector3 currentSpawnPos; //change this before loading the level in order to spawn camera on right position

    private void Awake()
    {
        if (GameScoreManager.currentDiff != GameScoreManager.difficulty.nothing) //Maarten TODO: only load random pos in highscore modus? 
        {
            Vector3 spawnPos = new Vector3(currentSpawnPos.x, currentSpawnPos.y, currentSpawnPos.z);
            this.gameObject.transform.position = spawnPos;

            Debug.Log("Spawned Pos: " + spawnPos.x + "/" + spawnPos.y + "/" + spawnPos.z);
        }

    }

}
