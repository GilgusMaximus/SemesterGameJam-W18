using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour{


	[SerializeField]
	private GameObject waterPlane;

    private GameObject currWater;

	[SerializeField] private ParticleSystem particleSystem;
	
	[SerializeField] private float waterFlowingSpeed, minLifeTimeWater, maxLifeTimeWater, damagePerSecond;
	public float waterLevel;
	private bool isFLowing, waited10S; 


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

	}
}
