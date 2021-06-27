using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escupitajo : MonoBehaviour {

    public GameObject baba;
    AudioSource so;

    SpriteRenderer color;

	// Use this for initialization
	void Start () {
        so = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "sombra")
        {
            StartCoroutine(explotar(col.gameObject));
        }
    }

    IEnumerator explotar(GameObject col)
    {
        col.GetComponent<Animator>().SetTrigger("explotar");
        so.Play();
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(.1f);
        Destroy(transform.parent.gameObject);
    }
}
