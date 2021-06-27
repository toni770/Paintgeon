using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_premut : MonoBehaviour {
    Animator animacio;
    public GameObject obstacle;

    [HideInInspector]
    public bool butoActivat; // sapiguer si esta premut

    // Use this for initialization
    void Start () {
        animacio = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
	}
    void OnTriggerEnter2D(Collider2D other) // quan el pj entri al button, es clicara
    {
        if(other.tag == "box" || other.tag == "Player") // si no és un enemic s'executara
        {
            animacio.SetBool("premut", true);
            butoActivat = true;

            obstacle.SetActive(false);

        }
    }

    void OnTriggerExit2D(Collider2D other) // quan pj surti del button, el button es desclicara
    {
        if (other.tag == "box" || other.tag == "Player") // si no és un enemic s'executara
        {
            animacio.SetBool("premut", false);
            butoActivat = false;
            obstacle.SetActive(true);
        }
    }

    void OnTriggerStay2D(Collider2D other) // quan pj surti del button, el button es desclicara
    {
        if (other.tag == "box" || other.tag == "Player") // si no és un enemic s'executara
        {
            if (!butoActivat)
            {
                animacio.SetBool("premut", true);
                butoActivat = true;
                obstacle.SetActive(false);
            }
        }
    }

}
