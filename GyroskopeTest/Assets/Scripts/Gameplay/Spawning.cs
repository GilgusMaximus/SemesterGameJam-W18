using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour {

    private Rocks[] rocks;  //Alle steine
    [Tooltip("Die vordefinierten SChätze")]
    public GameObject[] Tresures; 
   
   
    private List<int> treasureNumbers;

   // Diamant,smaragd,rubin,saphire,gold,silber , mine, uhr


	// Use this for initialization
	void Awake () {
return;
         rocks =  GameObject.FindObjectsOfType<Rocks>();  // alle steine in der szene finden


        treasureNumbers = new List<int>();      
       


        while(treasureNumbers.Count!= Tresures.Length)      // eine int Liste wird erstellt und mit verschiedenen zufälligen werten befüllt
        {
            int x =Random.Range(0, rocks.Length);

            if (!treasureNumbers.Contains(x))
            {
                treasureNumbers.Add(x);
            }



        }


        int[] temp = treasureNumbers.ToArray();  //die Liste wird zum array gemacht


        for (int i = 0; i < temp.Length; i++)       // für jede Zahl in der treasureNumbersListe wird der dem ensprechende stein aus dem rock array befüllt
        {


            rocks[temp[i]].hasTreasure = true;
            rocks[temp[i]].Treasure = Tresures[i];  //count entfernt vllt bug?
             
            //Debug.Log(temp[i]);
        }

        


	}
	

}

 