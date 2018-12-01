using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanfieldBehaviour : MonoBehaviour {

    //Set in inspector
    public LayerMask layerMask;
    public Transform particles;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Treasure") {
            //Cast ray from scanner to treasure and spawn particle at intersection
            RaycastHit hit;
            Physics.Raycast(transform.parent.position, (other.gameObject.transform.position - transform.parent.position), out hit,
                                Mathf.Infinity, layerMask);
            Vector3 spawnPoint = hit.point;
            Instantiate(particles, spawnPoint, Quaternion.identity);
        }
    }
}
