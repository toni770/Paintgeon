using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SCRIPT QUE CONTROLA LA PEDRA
 */
public class AreaPedra : MonoBehaviour {


    GameObject jugador;              //GameObject del jugador
    PlayerMovement moviment;         //Script que controla el moviment del personatge
    Rigidbody2D rb;                  //Rigidbody de la caixa

    AudioSource so;
    bool push;                      //Variable que controla quan es pot empenyer la pedra

    
    Vector3 direccio;

    // Use this for initialization
    void Start () {

        rb = transform.parent.GetComponent<Rigidbody2D>();
        jugador = GameObject.FindGameObjectWithTag("Player");
        moviment = jugador.GetComponent<PlayerMovement>();

        so = transform.parent.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("A_Button") && push)
        {
            switch (name)
            {
                case "down":
                    direccio = Vector2.up;
                    break;

                case "up":
                    direccio = Vector2.down;
                    break;

                case "right":
                    direccio = Vector2.left;
                    break;

                case "left":
                    direccio = Vector2.right;
                    break;

            }

            so.Play();
            rb.AddForce(direccio * 30000000,ForceMode2D.Impulse);
            push = false;
        }

	}

    //Quan colisiona amb el jugador
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            moviment.imatgeInteraccio.SetActive(true);
            moviment.missatgeInteraccio("[E] Empujar");
            push = true;
        }
    }

    //Quan el jugador surt de l'area
    void OnTriggerExit2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            moviment.imatgeInteraccio.SetActive(false);
            push = false;
        }
    }
}
