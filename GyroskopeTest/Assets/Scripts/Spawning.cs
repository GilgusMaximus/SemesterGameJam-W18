using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour {

    public int treasureNumber;
    private Rocks[] rocks;

    public GameObject[] Tresures;
     private int count;
   
    private List<int> treasureNumbers;

   // Diamant,smaragd,rubin,saphire,gold,silber , mine, uhr


	// Use this for initialization
	void Start () {

         rocks =  GameObject.FindObjectsOfType<Rocks>();


        treasureNumbers = new List<int>();
       


        while(treasureNumbers.Count!= treasureNumber)
        {
            int x =Random.Range(0, rocks.Length);

            if (!treasureNumbers.Contains(x))
            {
                treasureNumbers.Add(x);
            }



        }
        int[] temp = treasureNumbers.ToArray();


        for (int i = 0; i < temp.Length; i++)
        {


            rocks[temp[i]].hasTreasure = true;
            rocks[temp[i]].Treasure = Tresures[count];
                count ++;
            Debug.Log(temp[i]);
        }

        


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

 