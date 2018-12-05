using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pickaxe : MonoBehaviour {
    public GameObject pickaxe;
    public float distanceFromRock;
    public float stayTime;
    public GameObject pick;
    public bool spawned;
    private float time;
    // Use this for initialization
    void Start () {
        time = stayTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (spawned)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                Destroy(pick);
                spawned = false;
                time = stayTime;
            }
        }
    }

    public void SpawnPickaxe(PointerEventData eventData)
    {
        Destroy(pick);

        //float hypothenuse = (Camera.main.transform.position - eventData.pointerPress.transform.position).magnitude;
        //float kathete = (new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.z) - new Vector2(eventData.pointerPress.transform.position.x, eventData.pointerPress.transform.position.z)).magnitude;


            pick = Instantiate(pickaxe, eventData.pointerPress.transform.position + (Camera.main.transform.position - eventData.pointerPress.transform.position).normalized * distanceFromRock,Quaternion.identity);
        // pick.GetComponent<Animation>().Play();
        pick.transform.LookAt(Camera.main.transform);
            spawned = true;
        

    }
}
