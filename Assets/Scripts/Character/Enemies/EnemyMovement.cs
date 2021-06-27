using System.Collections;
using System.Collections.Generic;
using UnityEngine;


abstract public class EnemyMovement : CharacterMovement
{
    public Transform target;                //Objectio a seguir

    protected Vector3 posInicial;               //Posicio inicial

    public float distanciaMaxima = 20f;     //Distancia maxima a la que l'enemic seguira al personatge
    public float distanciaMinima = 3f;      //Distancia minima a la que l'enemic seguira al personatge

    protected float rangeX;                    //Distancia entre jugador i enemic
    float rangeY;                    //Distancia entre jugador i enemic

    protected Vector3 pos;                            //Posicio a la que es moura

    protected float velocitatInicial;

    GameObject coin, health;         //Objectes que deixara anar al morir

    protected Vector3 targetPosition;                 //Posicio objectiu

    public float attackTime = 1;            //Temps entre atacs

    float timer = 3;                        //Timer per controlar que ataca cada cert temps.

    GameObject[] enemics;

    public int id;                          //Id del personatge

    bool curar = false;

    // Use this for initialization
    override public void Start () {
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player").transform.Find("cuerpo");

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), target.transform.parent.GetComponent<Collider2D>());

        enemics = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject o in enemics)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), o.transform.GetComponent<Collider2D>());
        }

        velocitatInicial = velocitat;

        posInicial = transform.position;

        targetPosition = new Vector3(0, 0, 0);

        coin = (GameObject)Resources.Load("coin");
        health = (GameObject)Resources.Load("Food");

        timer = attackTime;
    }

    protected void atacarJugador()
    {
        //Si el jugador esta dintre de la zona dels enemics
        if(target != null)
        {
            rangeX = Vector2.Distance(transform.position, target.position);

            timer += Time.deltaTime;

            if (target.parent.GetComponent<CharacterLife>().estaViu())
            {
                if (rangeX < distanciaMaxima)
                {
                    //Calcular posicio objectiu, devant del jugador
                    if (transform.position.x >= target.position.x)
                    {
                        targetPosition = target.position + new Vector3(distanciaMinima, 0, 0);
                    }
                    else
                    {
                        targetPosition = target.position - new Vector3(distanciaMinima, 0, 0);
                    }

                    if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
                    {
                        //Si no esta a la posicio objectiu, es mou
                        if (transform.position != targetPosition)
                        {
                            //Moure fins la posicio del personatge + un rang de X
                            pos = Vector3.MoveTowards(transform.position, targetPosition, velocitat * Time.deltaTime);

                            //Animar moviment
                            animarMoviment(pos.x - transform.position.x, pos.y - transform.position.y);

                            //Girar personatge depenent de la posicio de l'onjectiu
                            girar(pos.x - target.position.x, transform.Find("cuerpo"));
                            transform.position = pos;
                        }
                        //Si esta a la posicio objectiu, ataca
                        else
                        {
                            if (target.parent.GetComponent<CharacterLife>().estaViu() && timer >= attackTime)
                            {
                                timer = 0;
                                atac = true;
                                atacar();
                            }
                        }
                    }
                }
            }

            else
            {
                target = null;
                curar = true;
            }
        }

        //Si el jugador esta fora de la zona dels enemics
        else
        {
            if (curar)
            {
                vida.augmentarVida(vida.vidaMaxima, 10);
                curar = false;
            }
            if (transform.position != posInicial)
            {
                //Moure fins la posicio del personatge + un rang de X
                pos = Vector3.MoveTowards(transform.position, posInicial, velocitat * Time.deltaTime);

                //Animar moviment
                animarMoviment(pos.x - transform.position.x, pos.y - transform.position.y);

                //Girar personatge depenent de la posicio de l'onjectiu
                girar(pos.x - posInicial.x, transform.Find("cuerpo"));
                transform.position = pos;

            }
            else
            {
                anim.SetFloat("velocitat", 0);
                velocitat = velocitatInicial;
                target = GameObject.FindGameObjectWithTag("Player").transform.Find("cuerpo");
            }
        }

    }

	// Update is called once per frame
	void Update () {

        
        isTriggered = false;

        //Si tant l'enemic com el jugador estan vius i es pot moure.
        if (vida.estaViu() && move )
        {
            atacarJugador();
        } 
    }

    //Deixar objectes al morir
    public void loot()
    {
        if(gameObject.name != "tuto")
        {
            int rand = Random.Range(1, 100);

            if (rand > 50 && rand <= 90)
            {
                Instantiate(coin, transform.position, transform.rotation);
            }
            if (rand > 90 && rand <= 100)
            {
                Instantiate(health, transform.position, transform.rotation);
            }
        }
        else
        {
            Instantiate(coin, transform.position, transform.rotation);
        }
       
    }

    //Al chocar amb l'atac del jugador
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "attackPlayer" && !isTriggered)
        {
            isTriggered = true;
            if (!inmortal)
            {
                vida.treureVida(other.transform.parent.GetComponent<PlayerMovement>().fuerza, 10);
                StartCoroutine(damageAnimacio());
            }
        }
    }
}
