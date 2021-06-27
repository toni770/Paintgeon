using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * SCRIPT QUE CONTROLA LA INTERACCIO AMB ELS NPC
 */
public class NpcPoble : MonoBehaviour {

    public GameObject menu;              //Menu que s'obrira

    Animator animacions;            //Animacions de l'NPC
    GameObject jugador;             //GameObject del jugador
    PlayerMovement moviment;     //Script que controla el moviment del personatge

    public AudioClip[] sons = new AudioClip[4];

    AudioSource so;

    public int id;


    bool parlar = false;

    // Use this for initialization
    void Start () {
        animacions = transform.parent.GetComponent<Animator>();
        jugador = GameObject.FindGameObjectWithTag("Player");
        moviment = jugador.GetComponent<PlayerMovement>();

        so = transform.parent.GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
        if (parlar && Input.GetButtonDown("A_Button"))
        {
            int num = Random.Range(0, 4);
            so.clip = sons[num];

            so.Play();
            StartCoroutine(mostrarMenu());
            moviment.imatgeInteraccio.SetActive(false);
            animacions.SetTrigger("open");
            moviment.move = false;
        }
	}

    IEnumerator mostrarMenu()
    {
        yield return new WaitForSeconds(.9f);
        moviment.move = true;
        menu.SetActive(true);
       
        moviment.imatgeInteraccio.SetActive(true);

    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            moviment.imatgeInteraccio.SetActive(true);
            moviment.missatgeInteraccio("[E] "+ menu.name);
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
