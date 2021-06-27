using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SCRIPT ENCARREGAT DE APLICAR PROFUNDITAT EN EL JOC
 */

[RequireComponent(typeof(SpriteRenderer))]
public class Depth : MonoBehaviour {

    private SpriteRenderer Sr;  //Component que controla la profunditat.
    float posicio;              //Posicio del personatge

	// Use this for initialization
	void Start () {
        Sr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //Depenent de a la altura a la que estigui el personatge, canvia la profunditat. Quan mes abaix estigui un personatge, mes per devant estara
        posicio = transform.position.y * 100;
        Sr.sortingOrder = -(int)posicio;
	}
}
