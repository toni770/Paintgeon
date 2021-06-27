using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFinal : MonoBehaviour {

    Animator anim;
    CharacterLife vida;
    BoxCollider2D mal;
    AudioSource crit;

    public GameObject atac;
    public GameObject exclamacio;
    public GameObject vomit;
    public GameObject block;

    public AudioSource soVomit;

    public bool activar = false;

    bool ataca = false;

    Animator animVomit;

    public GameObject moc;
    public GameObject baba;

    bool isTriggered = false;

    public bool lluita = false;

    public float fuerza = 10;

    [Header("Cooldown dels atacs")]
    public float cdAtac = 10;
    public float cdVomit = 10;
    public float cdCrit = 10;

    GameObject target;

    public AudioClip crit2;

    float contAtac = 0, contVomit = 0, contCrit = 0;

    int num;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        vida = GetComponent<CharacterLife>();
        mal = GetComponent<BoxCollider2D>();
        crit = GetComponent<AudioSource>();

        animVomit = vomit.GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Player");

        mal.enabled = false;

        contAtac = 0;
        contVomit = cdVomit;
        contCrit = cdCrit;

	}
	
	// Update is called once per frame
	void Update () {
        isTriggered = false;

        if (activar)
        {
            StartCoroutine(cabrejar());
            activar = false;
        }
        if (lluita)
        {
            if (target.GetComponent<CharacterLife>().estaViu())
            {
                atacar();
            }
        }
    }

    IEnumerator cabrejar()
    {
        yield return new WaitForSeconds(3);
        anim.SetTrigger("alerta");
        yield return new WaitForSeconds(4);
        lluita = true;
    }

    void atacar()
    {
        contAtac -= Time.deltaTime;
        contCrit -= Time.deltaTime;
        contVomit -= Time.deltaTime;

        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("attack") && !ataca)
        {
            num = Random.Range(1, 101);

            ataca = true;
            //Ataca normal
            if (num <= 50)
            {
                if (contAtac <= 0 && Mathf.Abs(transform.position.x - target.transform.position.x) < 22 && target.transform.position.y > -26.22f)
                {
                    anim.SetTrigger("attack");
                    contAtac = cdAtac;
                }
            }

            //Crida
            else if (num > 50 && num <= 80)
            {
                if (contCrit <= 0)
                {
                    print("cridar: " + num);
                    StartCoroutine(invocar());
                    contCrit = cdCrit;
                }
            }

            //Vomita
            else
            {
                if (contVomit <= 0)
                {
                    print("vomitar: " + num);
                    StartCoroutine(escup());
                    contVomit = cdVomit;
                }
            }

            ataca = false;
        }

    }

    IEnumerator escup()
    {
        anim.SetTrigger("vomitar");
        yield return new WaitForSeconds(2);

        Instantiate(baba, new Vector2(603, Random.Range(-24.9f, -31.8f)), Quaternion.identity);
        yield return new WaitForSeconds(.2f);
        Instantiate(baba, new Vector2(596.8f, Random.Range(-24.9f, -31.8f)), Quaternion.identity);
        yield return new WaitForSeconds(.2f);
        Instantiate(baba, new Vector2(591.2f, Random.Range(-24.9f, -31.8f)), Quaternion.identity);

    }
   IEnumerator invocar()
    {
        anim.SetTrigger("cridar");
        yield return new WaitForSeconds(1);

        Instantiate(moc,new Vector2(transform.position.x+3, transform.position.y + 2),Quaternion.identity);

        Instantiate(moc, new Vector2(transform.position.x-5, transform.position.y -2), Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "attackPlayer" && !isTriggered)
        {
            isTriggered = true;
            vida.treureVida(coll.transform.parent.GetComponent<PlayerMovement>().fuerza, 10);

            if(vida.vidaPersonatge <= 0)
            {
                mal.enabled = false;
                StartCoroutine(morir());
            }
        }
    }

    IEnumerator morir()
    {
        print("morir");
        crit.Stop();
        crit.clip = crit2;
        crit.Play();
        SaveData.enemics[5] = true;
        anim.SetTrigger("death");
        yield return new WaitForSeconds(5);
        Destroy(block);
        Destroy(gameObject);

    }

    //FUNCIONS PER ACTIVAR EFECTES I ATACS
    public void activarAtac()
    {
        atac.SetActive(!atac.activeSelf);
    }

    public void activarMal()
    {
        cridar();
        mal.enabled = !mal.enabled;
    }

    public void activarExclamcio()
    {
        exclamacio.SetActive(!exclamacio.activeSelf);
    }

    public void activarVomit()
    {
        vomit.SetActive(!vomit.activeSelf);
        animVomit.Play("vomito",0,0);
        if (soVomit.isPlaying)
        {
            soVomit.Stop();
        }
        else
        {
            soVomit.Play();
        }
    }

    public void cridar()
    {
        if (crit.isPlaying)
        {
            crit.Stop();
        }
        else
        {
            crit.Play();
        }
        
    }
}
