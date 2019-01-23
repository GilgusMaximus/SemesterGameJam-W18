using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Water : MonoBehaviour{


	[SerializeField]
	private GameObject waterPlane;

    private GameObject currWater;

	[SerializeField] private ParticleSystem particleSystem;
	private ParticleSystem.MainModule main;
	[SerializeField] private float waterFlowingSpeed, minLifeTimeWater, maxLifeTimeWater, damagePerSecond;
	private float waterLevel;
	private bool isFLowing, waited10S, decreaseWater;
	private static Water waterScript;
	private PostProcessLayer postLayer;
	void Start() {
		waterLevel = 0f;
		isFLowing = false;
		waited10S = false;
        StartCoroutine(wait10Seconds());
		main = particleSystem.main;
		waterScript = this;
		postLayer = GetComponent<PostProcessLayer>();
		postLayer.enabled = false;
	}

    IEnumerator wait10Seconds()
    {
        yield return new WaitForSeconds(10);
        waited10S = true;
        Debug.Log("WASSSER MAAAAAAAAAAAAARRRRRSCH");
    }

    public void decreaseWaterLevel() {
		
		Vector3 waterPlanePosition = waterPlane.transform.position;
		//Debug.Log("WATERPLANE IN DURATION 1 : " + waterPlanePosition.y);
		if (waterPlanePosition.y > -1.5f) {
            if (waterLevel <= 0.5)
            {
                waterLevel = 0;
            }
            else
            {
                waterLevel -= 0.5f;
            }

			waterPlane.transform.Translate(new Vector3(0, -.5f, 0));
			decreaseWater = true;
		}
		//Debug.Log("WaterLevel Decreases");
	}

	public ParticleSystem getps() {
		return particleSystem;
	}
	public void spawnWater(Transform position) {
		Debug.Log("ISFLOWING: " + isFLowing);
		if (!isFLowing&&waited10S) {
			if (Random.Range(0, 100) <= 33) {
				//Debug.Log("Water Spawned");
				isFLowing = true;
				Vector3 stoneForward = (position.position - transform.position).normalized;
				particleSystem.transform.position =
					position.position +  stoneForward * 2.5f; //reposition particleSystem
				main.duration = Random.Range(minLifeTimeWater, maxLifeTimeWater);
				Vector3 psForward = particleSystem.transform.forward.normalized;
				//mithilfe des Skalarprodukts den zu drehenden Winkel rausfinden
				//float winkel = Mathf.Acos((psForward.x * stoneForward.x + psForward.z * stoneForward.z) /
				 //                         (Mathf.Sqrt(psForward.x * psForward.x + psForward.z * psForward.z) *
				 //                         Mathf.Sqrt(stoneForward.x * stoneForward.x + stoneForward.z +
				 //                                    stoneForward.z)));
				
				//particleSystem.transform.Rotate(new Vector3(0, 1, 0)*winkel, Space.World);
				particleSystem.transform.LookAt(transform.position);
				particleSystem.Play();
			}
		}
	}

	public static Water getWaterScript() {
		return waterScript;
	}

	private void Update() {
		//Debug.Log("Water Flowing: " + isFLowing);
		Vector3 waterPlanePosition = waterPlane.transform.position;
		if (particleSystem.isPlaying == false) {
			isFLowing = false;
            //resetting
            if (waterLevel <= 0) {
	            waterLevel = 0;
	            waterPlane.transform.position = new Vector3(-24f, -1.5f, 3.5f);
            }
		}

		if (isFLowing) {
			waterLevel += waterFlowingSpeed * Time.deltaTime;
			waterPlane.transform.Translate(new Vector3(0, waterFlowingSpeed * Time.deltaTime, 0));
            //apply damage
            if (waterLevel>0)
            {
               // GameScoreManager.rtime -= waterLevel*Time.deltaTime;
            }
		}

		if (waterPlanePosition.y > transform.position.y) {
			//TODO Access stability and decrease it or set a flag, which is checked by stability
			GameScoreManager.rtime -= waterLevel*Time.deltaTime;
			postLayer.enabled = true;
		} else
			postLayer.enabled = false;


	}

	/*
	void Start () {
		waterLevel = 0f;
		isFLowing = false;
        waited10S = false;
        StartCoroutine(wait10Seconds());
	}

	IEnumerator  wait10Seconds() {
		yield return new WaitForSeconds(10);
        StartCoroutine(spawnWater());
    }

    IEnumerator spawnWater()
    {
        for(; ; )
        {
            if (!isFLowing)
            {
                if (Random.Range(0,100)<=30)
                {
                    //Spawning
                    isFLowing = true;
                    waterLevel = this.transform.position.y - 2;
                    currWater = Instantiate(waterPlane, new Vector3(transform.position.x, waterLevel, transform.position.z), Quaternion.identity);
                    
                }
            }

            yield return new WaitForSeconds(1);
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (isFLowing&&currWater!=null)
        {

            if (waterLevel<transform.position.y-2)
            {
                Destroy(currWater);
                isFLowing = false;
            }

            waterLevel += waterFlowingSpeed * Time.deltaTime;
            currWater.transform.position = Vector3.Slerp(currWater.transform.position, new Vector3(currWater.transform.position.x, waterLevel, currWater.transform.position.z)
                                                            , 60*Time.deltaTime); //TODO testing
        }
        Debug.Log(waterLevel);

	}*/
}
