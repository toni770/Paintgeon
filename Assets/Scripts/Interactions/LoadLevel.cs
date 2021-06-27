using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

    public int level = 0;

    Animator animacions;            //Animacions de l'NPC
    GameObject jugador;             //GameObject del jugador
    PlayerMovement moviment;     //Script que controla el moviment del personatge
    FadeManager fade;               //Efecte de fade

    // Use this for initialization

    AudioSource so;

    bool obrir;

    void Start () {
        animacions = transform.parent.GetComponent<Animator>();
        jugador = GameObject.FindGameObjectWithTag("Player");
        moviment = jugador.GetComponent<PlayerMovement>();
        fade = GameObject.Find("GameManager").GetComponent<FadeManager>();

        so = transform.parent.GetComponent<AudioSource>();

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
        SceneManager.LoadScene(level);
        
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            obrir = true;
            animacions.SetBool("open", obrir);
            moviment.imatgeInteraccio.SetActive(true);

            moviment.missatgeInteraccio("[E] "+ transform.parent.name);
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
