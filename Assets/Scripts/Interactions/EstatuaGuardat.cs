using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SCRIPT QUE CONTROLA L'OBJECTE ENCARREGAT DE GUARDAR LA PARTIDA
 */
public class EstatuaGuardat : MonoBehaviour {

    GameObject jugador;             //GameObject del jugador
    PlayerMovement moviment;     //Script que controla el moviment del personatge
    public GameObject resplandor;   //Animacio de resplandor

    AudioSource so;

    bool guardar;

	// Use this for initialization
	void Start () {

        so = transform.parent.GetComponent<AudioSource>();
        
        jugador = GameObject.FindGameObjectWithTag("Player");
        moviment = jugador.GetComponent<PlayerMovement>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("A_Button") && guardar)
        {
            so.Play();
            resplandor.SetActive(true);
            //Desactivar l'objecte que detecta si esta davant
            SaveData.Save("save" + SaveData.partida + ".sav");
            guardar = false;
            transform.gameObject.SetActive(false);
        }
	}

    //Quan colisiona amb el jugador
    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.tag == "Player")
        {
            moviment.imatgeInteraccio.SetActive(true);
            moviment.missatgeInteraccio("[E] Guardar");
            guardar = true;
        }
    }

    //Quan el jugador surt de l'area
    void OnTriggerExit2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            moviment.imatgeInteraccio.SetActive(false);
            guardar = false;
        }
    }
}
