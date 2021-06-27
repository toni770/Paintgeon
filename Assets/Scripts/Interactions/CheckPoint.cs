using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SCRIPT QUE CONTROLA EL RESPAWN
 */
public class CheckPoint : MonoBehaviour {

    GameObject respawn;
    Animator anim;
    bool guardat = false;

    AudioSource so;

	// Use this for initialization
	void Start () {
        respawn = GameObject.FindGameObjectWithTag("Respawn");
        anim = GetComponent<Animator>();

        so = GetComponent<AudioSource>();
	}

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (!guardat)
            {
                so.Play();
                anim.SetTrigger("Saved");
                respawn.transform.position = new Vector2(transform.position.x, transform.position.y - 7);
                guardat = true;
            }
        }
    }
}
