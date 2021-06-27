using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemicInvocacio : EnemyMovement {

    public float comptador = 5;

    // Use this for initialization
    public override void Start() {
        base.Start();
    }

    // Update is called once per frame
    void Update() {

        comptador -= Time.deltaTime;

        if(comptador <= 0)
        {
            StartCoroutine(explotar());
        }
        else
        {
            targetPosition = target.position + new Vector3(distanciaMinima, 0);

            if (transform.position != targetPosition)
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
                {
                    //Moure fins la posicio del personatge + un rang de X
                    

                    pos = Vector3.MoveTowards(transform.position, targetPosition, velocitat * Time.deltaTime);
                    //Girar personatge depenent de la posicio de l'onjectiu
                    girar(pos.x - target.position.x, transform);

                    transform.position = pos;
                }
            }
            else
            {
                StartCoroutine(explotar());
            }
        }
    }


    IEnumerator explotar()
    {
        anim.SetTrigger("attack");
        so.Play();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "attackPlayer")
        {
            StartCoroutine(explotar());
        }
    }
}
