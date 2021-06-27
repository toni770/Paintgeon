using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SCRIPT ENCARREGAT DE CONFIGURAR LES PORTES DEL POBLE
 */
public class PortaPoble : MonoBehaviour {

    public Vector3 posicioDesti;            //Posicio on es teltransportara el personatge
    public float maxX, minX, maxY, minY;    //Limits de la camera al canviar de sala


    Animator animacions;            //Animacions de l'NPC
    GameObject jugador;             //GameObject del jugador
    PlayerMovement moviment;     //Script que controla el moviment del personatge
    FadeManager fade;               //Efecte de fade
    GameObject camara;              //Camera de l'escena
    CameraFollow follow;            //Script del seguiment de la camera

    AudioSource so;

    bool obrir;
    // Use this for initialization
    void Start () {
        animacions = transform.parent.GetComponent<Animator>();
        jugador = GameObject.FindGameObjectWithTag("Player");
        moviment = jugador.GetComponent<PlayerMovement>();
        camara = GameObject.FindGameObjectWithTag("MainCamera");
        follow = camara.GetComponent<CameraFollow>();

        so = transform.parent.GetComponent<AudioSource>();

        fade = GameObject.Find("GameManager").GetComponent<FadeManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (obrir && Input.GetButtonDown("A_Button"))
        {
            so.Play();
            StartCoroutine(canviarEscena());

        }
    }
    
    IEnumerator canviarEscena()
    {
        moviment.move = false;
        fade.Fade(true, 1.5f);
        yield return new WaitForSeconds(1.5f);
        jugador.transform.position = posicioDesti;

        follow.canviarLimits(maxX, minX, maxY, minY);

        fade.Fade(false, 1.5f);
        moviment.move = true;



    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            obrir = true;
            animacions.SetBool("open", obrir);
            moviment.imatgeInteraccio.SetActive(true);

            moviment.missatgeInteraccio("[E] " + transform.parent.name);
        }
    }
    void OnTriggerExit2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            obrir = false;
            animacions.SetBool("open", obrir);
            moviment.imatgeInteraccio.SetActive(false);
        }
    }
}
