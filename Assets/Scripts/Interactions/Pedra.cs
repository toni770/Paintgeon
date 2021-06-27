using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SCRIPT QUE CONTROLA QUE LES PEDRES NO ES MOGUINS QUAN CHOQUEN ENTRE ELLES
 */
public class Pedra : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "box")
        {
           GetComponent<Rigidbody2D>().velocity = Vector2.zero;
           
        }
    }
}
