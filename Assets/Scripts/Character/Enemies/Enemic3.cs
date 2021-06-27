using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemic3 : EnemyMovement {


    public bool cabrejat = false;

   

    public SpriteRenderer cos, dret, esquerra;

    public Sprite cosE, dretE, esquerraE;

	// Use this for initialization
	public override void Start () {
        base.Start();

        inmortal = true;
	}
	
	// Update is called once per frame
	void Update () {

        isTriggered = false;

		if(SaveData.volumGeneral == 0 || SaveData.volumMusica == 0)
        {
            StartCoroutine(transformar());
        }

        if (cabrejat)
        {
            so.enabled = false;
            inmortal = false;
            atacCabrejat();
        }
	}

    //Transformarse
    IEnumerator transformar()
    {
        anim.SetTrigger("rage");
        yield return new WaitForSeconds(1.5f);
        cabrejat = true;
    }

    //Funcio per canviar els sprites al transformarse
    public void canviarSprites()
    {
        cos.sprite = cosE;
        dret.sprite = dretE;
        esquerra.sprite = esquerraE;
    }

    //Ataca quan es transforma
    void atacCabrejat()
    {

        if(target != null)
        {
            if (target.parent.GetComponent<CharacterLife>().estaViu())
            {

                rangeX = Vector2.Distance(transform.position, target.position);

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

                    //Si no esta a la posicio objectiu, es mou
                    if (transform.position != targetPosition)
                    {
                        //Moure fins la posicio del personatge + un rang de X
                        pos = Vector3.MoveTowards(transform.position, targetPosition, velocitat * Time.deltaTime);

                        transform.position = pos;
                    }
                }
            }
            else
            {
                vida.augmentarVida(vida.vidaMaxima, 10);
                target = null;
            }
        }
        else
        {
            if (transform.position != posInicial)
            {
                transform.position = Vector3.MoveTowards(transform.position, posInicial, velocitat * Time.deltaTime);
            }
            else
            {
                velocitat = velocitatInicial;
                target = GameObject.FindGameObjectWithTag("Player").transform.Find("cuerpo");
            }
        }
       
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

  
}
