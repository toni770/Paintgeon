using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PalancaLlum : MonoBehaviour {


    Animator animacions;            //Animacions de l'objecte que guarda
    GameObject jugador;             //GameObject del jugador
    PlayerMovement moviment;        //Script que controla el moviment del personatge

    bool activar;               //Controla si es pot activar
    public bool on = false;               //Controla si esta activada la palanca

    puzzle2 parentScript;           //Script que controla el puzzle

    // Use this for initialization
    void Start () {
        animacions = transform.parent.GetComponent<Animator>();

        jugador = GameObject.FindGameObjectWithTag("Player");
        moviment = jugador.GetComponent<PlayerMovement>();

        parentScript = transform.parent.parent.parent.Find("obstacle_niebla2").GetComponent<puzzle2>();

        if (on)
        {
            animacions.SetBool("activar", true);
            animacions.Play("on",0);
            
        }
        else
        {
            animacions.SetBool("activar", false);
            animacions.Play("off", 0);
            
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("A_Button") && activar)
        {

            canviarAnim();

            switch (transform.parent.name)
            {
                case "1":
                    parentScript.palanques[1].canviarAnim();
                    break;

                case "6":
                    parentScript.palanques[4].canviarAnim();
                    break;

                default:
                    parentScript.palanques[Int32.Parse(transform.parent.name)].canviarAnim();
                    parentScript.palanques[Int32.Parse(transform.parent.name)-2].canviarAnim();
                    break;
            }
            activar = false;
            moviment.imatgeInteraccio.SetActive(false);
            moviment.move = false;
            StartCoroutine(activacio());
        }

    }

    //Funcio que canvia la animacio de la palanca
    public void canviarAnim()
    {
        on = !on;
        animacions.SetBool("activar", on);
    }

    IEnumerator activacio()
    {
 
        yield return new WaitForSeconds(.3f);
        moviment.imatgeInteraccio.SetActive(true);
        activar = true;
        moviment.move = true;

    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            moviment.imatgeInteraccio.SetActive(true);
            moviment.missatgeInteraccio("[E] Activar");
            activar = true;
        }
    }

    //Quan el jugador surt de l'area
    void OnTriggerExit2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            moviment.imatgeInteraccio.SetActive(false);
            activar = false;
        }
    }
}
