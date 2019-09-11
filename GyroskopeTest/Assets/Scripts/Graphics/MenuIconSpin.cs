using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuIconSpin : MonoBehaviour
{

	[SerializeField] private RectTransform iconTransform;
	// Update is called once per frame
	void Update () {
		iconTransform.Rotate(new Vector3(0, 1, 0), Space.Self);
	}
}
