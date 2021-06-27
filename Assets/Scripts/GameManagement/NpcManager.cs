using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour {

    public GameObject[] npc;

    public GameObject[] obres;

	// Use this for initialization
	void Start () {


		for(int i=0; i< SaveData.npc.Length; i++)
        {
            if (SaveData.npc[i])
            {
                npc[i].SetActive(true);
            }
            else
            {
                obres[i].SetActive(true);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
