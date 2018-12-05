using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour {

    private Vector3 playerpos;
    public float speed;
    private Vector3 direction;
    public float aufsammelRadius;
    public bool dissolve;
    private MeshRenderer renderer;
    public float dissolveSpeed;
    public bool discovered;

    public int Wert;
    private bool instantiated;
    public ParticleSystem treasureparticle;

	// Use this for initialization
	void Start () {

        playerpos = GameObject.Find("PlayerPosition").transform.position;
        Debug.Log(playerpos);
        direction = (playerpos - this.transform.position).normalized;

        renderer = this.gameObject.GetComponent<MeshRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
        if (discovered)
        {
            if (!instantiated)
            {
                if (treasureparticle != null)
                {
                    Instantiate(treasureparticle, this.transform.position, Quaternion.identity);
                    instantiated = true;
                }
            }
            if ((this.transform.position - playerpos).magnitude >= aufsammelRadius)
            {
                // Debug.Log("Go");
                this.transform.Translate(direction * Time.deltaTime * speed);
            }
            else
            {
                Dissapear();
            }

            if (dissolve)
            {

                renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, renderer.material.color.a - dissolveSpeed * Time.deltaTime);

                if (renderer.material.color.a <= 0)
                {
                    Destroy(this.gameObject);
                }
            }


        }

	}

    public void Dissapear()
    {
        //addScore

        dissolve = true;





    }

   


}
