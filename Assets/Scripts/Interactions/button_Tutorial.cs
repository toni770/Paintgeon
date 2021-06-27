using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_Tutorial : MonoBehaviour {

    Animator animacio;
    [HideInInspector]
    public bool activat = false;

	// Use this for initialization
	void Start () {
        animacio = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other) // quan el pj entri al button, es clicara
    {
        if (other.tag == "box") // si no és un enemic s'executara
        {
            activat = true;
        }
        animacio.SetBool("premut", true);
    }

    void OnTriggerExit2D(Collider2D other) // quan pj surti del button, el button es desclicara
    {
        animacio.SetBool("premut", false);
    }
}
