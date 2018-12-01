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
            r.ReduceHealth();
        }

        //Destroy(gameObject);
    }

}
