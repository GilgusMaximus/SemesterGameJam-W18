using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanfieldBehaviour : MonoBehaviour {

    //Set in inspector
    public LayerMask layerMask;
    public Transform[] particles;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<Treasure>()!=null) {
            //Cast ray from scanner to treasure and spawn particle at intersection
            
            RaycastHit hit;
            Physics.Raycast(transform.parent.position, (other.gameObject.transform.position - transform.parent.position), out hit,
                                Mathf.Infinity, layerMask);

            Vector3 spawnPoint = hit.point;
            Vector3 treasureToPlayer = transform.parent.position - hit.point;
            treasureToPlayer = treasureToPlayer.normalized;
            treasureToPlayer *= 0.2f;
            spawnPoint += treasureToPlayer;

            switch (other.gameObject.tag)
            {
                case "Diamond":
                    Instantiate(particles[0], spawnPoint, Quaternion.identity);
                    break;

                case "Ruby":
                    Instantiate(particles[1], spawnPoint, Quaternion.identity);
                    break;

                case "Emerald":
                    Instantiate(particles[2], spawnPoint, Quaternion.identity);
                    break;

                case "Sapphire":
                    Instantiate(particles[3], spawnPoint, Quaternion.identity);
                    break;
                case "Gold":
                    Instantiate(particles[4], spawnPoint, Quaternion.identity);
                    break;
                case "Silver":
                    Instantiate(particles[5], spawnPoint, Quaternion.identity);
                    break;
                case "Dynamite":
                    Instantiate(particles[6], spawnPoint, Quaternion.identity);
                    break;
                case "Clock":
                    Instantiate(particles[7], spawnPoint, Quaternion.identity);
                    break;



            }
           
        }
    }
}
