using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SCRIPT QUE CONTROLA LA PALANCA
 */
public class Palanca : MonoBehaviour {

    Animator animacions;            //Animacions de l'objecte que guarda
    GameObject jugador;             //GameObject del jugador
    PlayerMovement moviment;        //Script que controla el moviment del personatge

    public Transform[] caixes = new Transform[3];
    Vector2[] posicioInicial = new Vector2[3];

    bool activar;               //Controla si es pot activar

                                 // Use this for initialization
    void Start () {
        animacions = transform.parent.GetComponent<Animator>();

        jugador = GameObject.FindGameObjectWithTag("Player");
        moviment = jugador.GetComponent<PlayerMovement>();

        for(int i = 0; i < 3; i++)
        {
            posicioInicial[i] = caixes[i].position;
           
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("A_Button") && activar)
        {
            activar = false;
            moviment.imatgeInteraccio.SetActive(false);

            moviment.move = false;

            StartCoroutine(activacio());
        }
    }

    IEnumerator activacio()
    {
        animacions.SetTrigger("activar");

        for (int i = 0; i < caixes.Length; i++)
        {
            caixes[i].position = posicioInicial[i];
        }

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
