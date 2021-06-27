using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * SCRIPT QUE CONTROLA ELS SCRIPTS DE MOVIMENT DE TOTS EL PERSONATGES
 */
abstract public class CharacterMovement : MonoBehaviour {

    public float velocitat = 15.0f;         //Velocitat del personatge

    [HideInInspector]
    public bool move = true;                      //Bool per comprobar si pots moure el personatge

    public GameObject attackCollider;           //Collider d'atac

    protected Rigidbody2D rb;                     //Rigidbody del personatge

    protected Animator anim;                      //Animació del personatge
    protected bool flip = false;                  //Variable que controla si el personatge esta girat

    protected CharacterLife vida;                 //Script que controla la vida

    protected bool atac = false;                  //Bool que controla si esta atacant

    public float fuerza = 15;                     //Força del personatge

    [HideInInspector]
    public bool inmortal = false;                        //Indica si esta inmortal

    public float inmortalTime = 0;                //Temps que sera inmortal

    public float deathTime = .7f;                 //Temps per la animació de morir

    protected bool isTriggered = false;               //Comprova si ha colisionat amb algun trigger.

    protected AudioSource so = null;

    // Use this for initialization
    virtual public void Start () {

        if(GetComponent<Rigidbody2D>() != null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        if(GetComponent<CharacterLife>() != null)
        {
            vida = GetComponent<CharacterLife>();
        }

        anim = GetComponent<Animator>();

        if(GetComponent<AudioSource>() != null)
        {
            so = GetComponent<AudioSource>();
        }

     
    }

    //Animar el moviment
    protected void animarMoviment(float x, float y)
    {
        anim.SetFloat("velocitat", Mathf.Abs(x) + Mathf.Abs(y));
    }

    //Funcio per girar el personatge
    protected void girar(float x, Transform cos)
    {
        //Si va cap a l'esquerra i no esta girat, el gira.
        if (x < 0 && !flip)
        {
            flip = true;
            cos.localScale = new Vector3(-cos.localScale.x, cos.localScale.y, cos.localScale.z);
            attackCollider.transform.localScale = new Vector3(-attackCollider.transform.localScale.x, attackCollider.transform.localScale.y, attackCollider.transform.localScale.z);
        }
        //Si va cap a la dreta i esta girat, el posa be.
        else if (x > 0 && flip)
        {
            flip = false;
            cos.localScale = new Vector3(-cos.localScale.x, cos.localScale.y, cos.localScale.z);
            attackCollider.transform.localScale = new Vector3(-attackCollider.transform.localScale.x, attackCollider.transform.localScale.y, attackCollider.transform.localScale.z);
        }
    }

    //Activar el poder fer mal al atacar
    protected void activarAtac()
    {
        attackCollider.SetActive(!attackCollider.activeSelf);
    }

    //Funcio per atacar
    protected void atacar()
    {
        if (atac && !this.anim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            anim.SetTrigger("attack");
            atac = false;

            if(rb != null)
            {
                rb.velocity = Vector2.zero;
            }

            atac = true;
        }
    }

    //Animacio al rebre mal
    protected IEnumerator damageAnimacio()
    {
        if (!vida.estaViu() && !this.anim.GetCurrentAnimatorStateInfo(0).IsTag("death"))
        {

            anim.SetTrigger("death");

            if (rb != null)
            {
                rb.velocity = Vector3.zero;
            }



            yield return new WaitForSeconds(deathTime);
            if(tag != "Player")
            {
                
                GetComponent<EnemyMovement>().loot();

                //Si no l'havies derrotat mai, es guarda a la partida.

                if (!SaveData.enemics[GetComponent<EnemyMovement>().id])
                {
                    SaveData.enemics[GetComponent<EnemyMovement>().id] = true;
                }

                Destroy(gameObject);

            }

        }
        else
        {
            if(GetComponent<EnemyMovement>() == null)
            {
                so.Play();
            }
            else
            {
                if (so != null && GetComponent<EnemyMovement>().id != 3)
                {
                    so.Play();
                }
            }


            anim.SetTrigger("damage");

            inmortal = true;
            yield return new WaitForSeconds(inmortalTime);
            inmortal = false;
        }

     }
}
