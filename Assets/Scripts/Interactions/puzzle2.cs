using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * SCRIPT QUE CONTROLA QUAN ES COMPLETA EL SEGON PUZZLE
 */
public class puzzle2 : MonoBehaviour {

    public GameObject parent;              //Objecte pare de les palanques
    [HideInInspector]
    public PalancaLlum[] palanques = new PalancaLlum[6];        //Palanques
	// Use this for initialization
	void Start () {

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            palanques[i] = parent.transform.GetChild(i).GetChild(0).gameObject.GetComponent<PalancaLlum>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(palanques[0].on && palanques[1].on &&  palanques[2].on &&  palanques[3].on &&  palanques[4].on && palanques[5].on)
        {
            this.gameObject.SetActive(false);
        }
	}
}
