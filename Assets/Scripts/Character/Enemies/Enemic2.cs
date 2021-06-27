using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemic2 : EnemyMovement {


    bool cabrejat = false;
    bool once = false;

	// Use this for initialization
	public override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update ()
    {
        isTriggered = false;

        anim.SetFloat("velocitat", 0);

       //Si tant l'enemic com el jugador estan vius i es pot moure.
        if (vida.estaViu() && move)
        {
            if(target != null)
            {
                rangeX = Vector2.Distance(transform.position, target.position);

                if (rangeX <= distanciaMaxima)
                {
                    if (!cabrejat && !once)
                    {
                        once = true;
                        StartCoroutine(cabrejar());
                    }
                }
                else
                {
                    once = false;
                    cabrejat = false;
                }
            }
            

            if (cabrejat)
            {
                atacarJugador();
            }
        }
    }

    IEnumerator cabrejar()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("rage"))
        {
            anim.SetTrigger("rage");
        }
        yield return new WaitForSeconds(3f);
        cabrejat = true;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    public void activarSo()
    {
        if (so.isPlaying)
        {
            so.Stop();
        }
        else
        {
            so.Play();
        }
    }
}
