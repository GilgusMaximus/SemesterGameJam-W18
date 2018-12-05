using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Touchable : MonoBehaviour, IPointerClickHandler {

  

    public void OnPointerClick(PointerEventData eventData)
    {
        Rocks r = eventData.pointerPress.GetComponent<Rocks>();
        if (r != null)
        {
            if (GameScoreManager.isCounting && !StartScan.scannerActive)
            {   

                GameObject.FindObjectOfType<Pickaxe>().SpawnPickaxe(eventData);

                r.ReduceHealth();
            }
        }
        else
        {
            Ladder l = eventData.pointerPress.GetComponent<Ladder>();
            if(l != null)
            {
                l.Respawn();
            }

        }

        //Destroy(gameObject);
    }


}
