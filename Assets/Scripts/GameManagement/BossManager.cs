using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour {

    public GameObject obstacle;

    public GameObject vidaBoss;

    public GameObject player;

    public GameObject boss;

    public GameObject camara;

    public AudioClip musicaLluita;

    public GameObject portal;

    PlayerMovement mov;

    AudioSource musicaFons;

    CharacterLife vidaPlayer, vidaCuc;

    BossFinal jefe;

    AudioClip musicaInicial;

    bool activat = false;

    bool reset = false;

    bool finish = false;

    bool once = false;

	// Use this for initialization
	void Start () {
        mov = player.GetComponent<PlayerMovement>();
        musicaFons = camara.GetComponent<AudioSource>();

        vidaPlayer = player.GetComponent<CharacterLife>();
        vidaCuc = boss.GetComponent<CharacterLife>();

        musicaInicial = musicaFons.clip;

        jefe = boss.GetComponent<BossFinal>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!vidaPlayer.estaViu())
        {
            if (!finish && !once)
            {
                reset = true;
                once = true;
                
            }
        }
        else if(!vidaCuc.estaViu())
        {
            if (!reset && !once)
            {
                once = true;
                finish = true;
            }
        }

        if (reset)
        {
            reset = false;

            StartCoroutine(resetejar());
        }
        else if (finish)
        {
            finish = false;
            StartCoroutine(morir());
        }
	}

    IEnumerator morir()
    {
        musicaFons.Stop();
        yield return new WaitForSeconds(2);
        vidaBoss.SetActive(false);
        portal.SetActive(true);
    }

    IEnumerator resetejar()
    {
        
        yield return new WaitForSeconds(6);
        musicaFons.Stop();
    
        camara.GetComponent<Animator>().Play("idle", 0, 0);
        camara.GetComponent<Camera>().orthographicSize = 11.38235f;
        camara.GetComponent<CameraFollow>().enabled = true;
        vidaBoss.SetActive(false);
        obstacle.SetActive(false);
        activat = false;

        vidaCuc.augmentarVida(vidaCuc.vidaMaxima, 100);

        jefe.activar = false;

        jefe.lluita = false;

        musicaFons.clip = musicaInicial;
        musicaFons.Play();       


    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player" && !activat)
        {
            activat = true;
            StartCoroutine(activarBoss());
        }
    }

    IEnumerator activarBoss()
    {
        once = false;
        mov.move = false;

        camara.GetComponent<CameraFollow>().enabled = false;
        camara.GetComponent<Animator>().Play("boss", 0, 0);
        print("bosws");

        player.GetComponent<Animator>().SetFloat("velocitat", 0);

        jefe.activar = true;
        musicaFons.clip = musicaLluita;
        musicaFons.Play();
        yield return new WaitForSeconds(7);
        vidaBoss.SetActive(true);
        obstacle.SetActive(true);
        mov.move = true;
        
    }
}
