using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * SCRIPT QUE CONTROLA EL MOVIMENT DEL PROTAGONISTA
 */
public class PlayerMovement : CharacterMovement {

    public GameObject imatgeInteraccio;        //Canvas del personatge

    Vector2 pos;                        //Posicio a la que es moura el personatge
    Transform cos;                      //Cos de personatge. Es necesita per girar el personatge

    GameObject resp;

    FadeManager fd;

    bool once = false;                  //Variable per executat una vegada el respawn

    // Use this for initialization
    public override void Start () {

        base.Start();

        imatgeInteraccio = transform.Find("CanvasPersonatge").transform.Find("Interaccio").gameObject;

        resp = GameObject.FindGameObjectWithTag("Respawn");


        cos = transform.Find("cuerpo");

        fd = GameObject.Find("GameManager").GetComponent<FadeManager>();
    }


    // Update is called once per frame
    void FixedUpdate () {

        isTriggered = false;

        if (vida.estaViu() && move)
         {
            if (!this.anim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
            {
                Moviment(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            }
            else
            {
                rb.velocity = Vector2.zero;
            }

            if (Input.GetButtonDown("X_Button"))
            {
                atac = true;
                atacar();
            }
       }
    }

    //Moure personatge
    void Moviment(float x, float y)
    {
        pos = new Vector2(x, y);
        animarMoviment(x, y);
        rb.velocity = pos * velocitat;

        girar(x,cos);
    }

    //Missatge que surt al interaccionar amb objectes
    public void missatgeInteraccio(string missatge)
    {
        //Canvia el text de la imatge d'interaccio
        Text interaccio = imatgeInteraccio.transform.GetChild(0).gameObject.GetComponent<Text>();
        interaccio.text = missatge;
    }
    //Indicar que estas en modo inmortal, fent desapareixer i aparecer les parts del cos 

    IEnumerator indicarInmortal()
    {
        while (inmortal)
        {
            GameObject[] parts = GameObject.FindGameObjectsWithTag("Part");

            foreach (GameObject t in parts)
            {
                 t.GetComponent<SpriteRenderer>().enabled = false;
            }

            yield return new WaitForSeconds(.1f);

            foreach(GameObject t in parts)
            {
                t.GetComponent<SpriteRenderer>().enabled = true;
            }

            yield return new WaitForSeconds(.1f);

        }
    }

    //Funcio per fer reapareixer al jugador
    IEnumerator respawn()
    {
        yield return new WaitForSeconds(3f);

        fd.Fade(true, 3f);
        move = false;
        yield return new WaitForSeconds(3f);

        //Reset valors
        transform.position = resp.transform.position;
        rb.velocity = Vector2.zero;
        anim.SetFloat("velocitat", 0);
        vida.augmentarVida(vida.vidaMaxima, 100);
        anim.Play("idle");
        fd.Fade(false, 3f);
        move = true;
        once = false;

    }

    //Quan colisiona amb un objecte o atac enemic
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "vida")
        {
            vida.augmentarVida(30, 10);
            Destroy(col.gameObject);
        }

        else if(col.tag == "attackEnemy" && !isTriggered)
        {
            isTriggered = true;

            if (!inmortal)
            {
                if(col.name == "boss")
                {
                    vida.treureVida(col.transform.parent.GetComponent<BossFinal>().fuerza, 10);
                }
                else if(col.name == "baba")
                {
                    vida.treureVida(10, 10);
                }
                else
                {
                    vida.treureVida(col.transform.parent.GetComponent<EnemyMovement>().fuerza, 10);
                }

                StartCoroutine(damageAnimacio());
            }

            if (vida.estaViu())
            {
                StartCoroutine(indicarInmortal());
            }
            else
            {
                if (!once)
                {
                    StartCoroutine(respawn());
                    once = true;
                }
            }
        }
    }
}
