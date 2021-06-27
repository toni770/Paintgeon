using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SCRIPT PER MOURE ELS NUVOLS
 */
public class MoureNuvols : MonoBehaviour {


    public float max_X;     //Fins a on arribara el nuvol
    public float max_Y;     //Maxim random on apareixera
    public float min_X;     //On apareixera quan arribi al final
    public float min_Y;     //Minim random on apareixera

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(transform.position.x > max_X)
        {
            transform.position = new Vector3(min_X,Random.Range(min_Y, max_Y));
        }
        else
        {
            transform.position += Vector3.right*Time.deltaTime*2;
        }
	}
}
