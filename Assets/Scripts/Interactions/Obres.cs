using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obres : MonoBehaviour {

    public GameObject npc;              //Npc que es comprara

    public Comprar comp;              //Script per comprar l'edifici
    GameObject jugador;             //GameObject del jugador
    PlayerMovement moviment;     //Script que controla el moviment del personatge

    public int id;

    public int preu;

    bool parlar = false;

    GameObject msg;

    // Use this for initialization
    void Start () {

        jugador = GameObject.FindGameObjectWithTag("Player");
        moviment = jugador.GetComponent<PlayerMovement>();

    }
	
	// Update is called once per frame
	void Update () {
        if (parlar && Input.GetButtonDown("A_Button"))
        {
            Cursor.visible = true;
            moviment.move = false;
            comp.comprar(transform.parent.gameObject, npc);
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            moviment.imatgeInteraccio.SetActive(true);
            moviment.missatgeInteraccio("[E] Construir");
            parlar = true;
        }
    }
    void OnTriggerExit2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            parlar = false;
            moviment.imatgeInteraccio.SetActive(false);
        }
    }

}
