using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class Mapes : MonoBehaviour {

    public GameObject info;

    public NpcPoble npc;

    public GameObject pref;         //Prefab del boto de l'enemic  

    public GameObject butons;
     
    public Transform content;       //Contingut de l'scrollView on aniran els botons dels enemics

    CharacterMovement mov;

    List<Enemics> enemics;

    List<Nivells> nivells;

    Animator anim;

    Image imatge;
    Text descripcio;
    Text titol;

    bool habilitat = false;


    void Awake()
    {
        nivells = BaseDades.getAllNivells().ToList<Nivells>();


        imatge = info.transform.Find("Image").GetComponent<Image>();

        descripcio = info.transform.Find("Descripcio").GetComponent<Text>();

        titol = info.transform.Find("Titol").GetComponent<Text>();

        mov = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();

        anim = GetComponent<Animator>();

    }

    void OnEnable()
    {
        StartCoroutine(activar());
    }

    IEnumerator activar()
    {
        Cursor.visible = true;

        mov.move = false;
        npc.enabled = false;

        anim.Play("open");
        yield return new WaitForSeconds(0.5f);

        habilitat = true;
        butons.SetActive(true);
        mov.move = true;
        Time.timeScale = 0;
    }

    IEnumerator desactivar()
    {
        Cursor.visible = false;
        habilitat = false;
        butons.SetActive(false);

        Time.timeScale = 1;

        anim.Play("close");
        
        yield return new WaitForSeconds(0.7f);

        npc.enabled = true;
        

        gameObject.SetActive(false);
    }



    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("B_Button") && habilitat)
        {
            if (info.activeSelf)
            {
                eliminarDades();
                info.SetActive(false);
                
            }
            else
            {
                StartCoroutine(desactivar());
            }
        }
    }

    public void OnClick(int id)
    {
        enemics = BaseDades.getAllEnemiesByNivell(id+1).ToList<Enemics>();

        info.SetActive(true);

        imatge.sprite = Resources.Load<Sprite>(nivells[id].Imatge);
        titol.text = nivells[id].Nom;
        descripcio.text = nivells[id].Descripcio;

        foreach(Enemics e in enemics)
        {
            pref.GetComponent<InfoMonstre2>().setDades(e);
            Instantiate(pref,content);
        }


    }

    //Funcio per eliminar les dades per triar enemic
    public void eliminarDades()
    {

        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }

    }
}
