using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collect : MonoBehaviour {

    Text coin;

    AudioSource so;
	// Use this for initialization
	void Start () {
        coin = GameObject.FindGameObjectWithTag("canvas").transform.Find("coin").GetChild(0).GetComponent<Text>();
        so = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            if(tag == "coin")
            {
                StartCoroutine(soroll());
                SaveData.monedes++;
                coin.text = SaveData.monedes.ToString();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator soroll()
    {
        so.Play();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}
