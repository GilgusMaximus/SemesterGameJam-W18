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
            if (GameScoreManager.isCounting)
            {
                GameObject.FindObjectOfType<Pickaxe>().SpawnPickaxe(eventData);

                r.ReduceHealth();
            }
        }

        //Destroy(gameObject);
    }


}
