using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lvl1Manager : MonoBehaviour {

    public GameObject[] obstacles;
	// Use this for initialization
	void Start () {
        StartCoroutine(inici());

        GameObject.FindGameObjectWithTag("canvas").transform.Find("coin").GetChild(0).GetComponent<Text>().text = SaveData.monedes.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator inici()
    {
        yield return new WaitForSeconds(5);
        foreach(GameObject g in obstacles)
        {
            g.SetActive(true);
        }
    }
}
