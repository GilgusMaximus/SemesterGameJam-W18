using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pickaxe : MonoBehaviour {
    [Tooltip("Das Pickaxe prefab")]
    public GameObject pickaxe;
    [Tooltip("Die Entfernung vom stein ,wo das object erzeugt wird")]
    public float distanceFromRock;
    [Tooltip("Wie lange das object bleibt bevor es wieder verschwindet(min die animations zeit)")]
    public float stayTime;
    private GameObject pick;
    private bool spawned;
    private float time;
    // Use this for initialization
    void Start () {
        time = stayTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (spawned) //wenn es eine picke gibt
        {
            time -= Time.deltaTime; //zeit herunterzählen und dann die picke nach gewisser zeit zerstören
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
        Destroy(pick);// falls noch eine picke da ist wird sie zerstört
            pick = Instantiate(pickaxe, eventData.pointerPress.transform.position + (Camera.main.transform.position - eventData.pointerPress.transform.position).normalized * distanceFromRock,Quaternion.identity); // die picke wird erzeugt
      
        pick.transform.LookAt(Camera.main.transform); // die rotation der picke wird richtig gesetzte;
            spawned = true;  //die flag dass eine spitzhacke erzeugt wurde wird gesetzte
        

    }
}
