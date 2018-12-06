using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Touchable : MonoBehaviour, IPointerClickHandler {

  

    public void OnPointerClick(PointerEventData eventData)
    {
        Rocks r = eventData.pointerPress.GetComponent<Rocks>();  //überprüfung ob das angeklickte element ein stein ist
        if (r != null)
        {
            if (GameScoreManager.isCounting && !StartScan.scannerActive)    //wenn man abbauen darf
            {   

                GameObject.FindObjectOfType<Pickaxe>().SpawnPickaxe(eventData);   //das event wird an den Pickaxespawner weitergegeben

                r.ReduceHealth();  //das Leben des steins wird reduziert
            }
        }
        else //wenn kein stein
        {
            Ladder l = eventData.pointerPress.GetComponent<Ladder>();//überprüfung ob das angeklickte element eine Leiter ist
            if (l != null)
            {
                l.Respawn();  //die scene wird neu geladen
            }

        }

       
    }


}
