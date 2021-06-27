using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SCRIPT PER CONTROLAR QUE ELS ENEMICS NO SORTIR D'UN CERT RANG
 */
public class EnemyZone : MonoBehaviour {

    EnemyMovement en;
    CharacterLife vid;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Enemy")
        {
            en = coll.GetComponent<EnemyMovement>();
            vid = coll.GetComponent<CharacterLife>();

            en.velocitat = 15;
            en.target = null;

            vid.augmentarVida(vid.vidaMaxima, 5);



        }
    }
}
