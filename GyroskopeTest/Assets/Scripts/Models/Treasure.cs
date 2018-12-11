using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour {

    private Vector3 playerpos;
    [Tooltip("die geschw. mit der der schatz zum spieler fliegt")]
    public float speed;
    private Vector3 direction;
    [Tooltip("der radius in dem aufgesammelt wird")]
    public float aufsammelRadius;
    private bool dissolve;
    private MeshRenderer render;
    [Tooltip("die geschw. mit der es verschwindet")]
    public float dissolveSpeed;
    [Tooltip("Ob der stein in dem sich der Schatz befindet zerstört wurde")]
    public bool discovered;

    public int Wert;
    private bool instantiated;
    public ParticleSystem treasureparticle;

	// Use this for initialization
	void Start () {

        playerpos = GameObject.Find("PlayerPosition").transform.position;
        Debug.Log(playerpos);
        direction = (playerpos - this.transform.position).normalized;

        render = this.gameObject.GetComponentInChildren<MeshRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
        if (discovered) //Wenn discovered/der stein der den schatz beinhaltet zerstört wurde
        {
            if (!instantiated) //das wird nur beim ersten update nach dem entdecken ausgeführt
            {
                if (treasureparticle != null)
                {
                    Instantiate(treasureparticle, this.transform.position, Quaternion.identity);  //erschaffung des particle systems
                    instantiated = true;
                }
            }
            if ((this.transform.position - playerpos).magnitude >= aufsammelRadius)  //wenn die entfernung zum spieler/ziel größer als der aufsammelradius ist
            {
            
                this.transform.Translate(direction * Time.deltaTime * speed); //gehe zum spieler/ziel
            }
            else
            {
                Dissapear();//flag um das verwinden einzuleiten wird gesetzt
            }

            if (dissolve) //wenn dissolve flag gesetzt
            {

                render.material.color = new Color(render.material.color.r, render.material.color.g, render.material.color.b, render.material.color.a - dissolveSpeed * Time.deltaTime); //alpha des maerials runersetzen

                if (render.material.color.a <= 0) //wenn alpha klein genug ist zerstöre das Object
                {
                    Destroy(this.gameObject);
                }
            }


        }

	}

    public void Dissapear()  //flag um das verwinden einzuleiten wird gesetzt
    {
        

        dissolve = true;





    }

   


}
