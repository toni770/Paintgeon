using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/*
 * SCRIPT QUE CONTROLA EL BESTIARI DEL JOC
 */
public class Bestiari : MonoBehaviour {

    CharacterMovement mov;          //Script que controla el moviment del personatge
    Animator anim;                  //Animacio del llibre   
    bool habilitat = false;         //Controla si s'ha habilitat el bestiari
    List<Enemics> enemics;          //Llista d'enemics
    List<string> ltipus;            //Llista dels tipus disponibles

    public GameObject triar;        //Pagina per triar els enemics
    public GameObject info;         //Pagina que mostra la informacio de l'enemic seleccionat
    public GameObject pref;         //Prefab del boto de l'enemic   
    public Transform content;       //Contingut de l'scrollView on aniran els botons dels enemics
    public GameObject cap;          //Text que surt quan no hi ha cap monstre a la llista
    public Text txt;                //Text de cerca
    public Dropdown combobox;       //ComboBox on estan els tipus
    public NpcPoble npc;            //Script del npc que obre el bestiari

    int contadorEnemics = 0;

	// Use this for initialization
	void Awake () {

        anim = GetComponent<Animator>();
        mov = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
        //Carrega tots els tipus dintre del comboBox

        ltipus = new List<string>();

        var tipus = BaseDades.getAllTipus();

        foreach(Enemics tp in tipus)
        {
            ltipus.Add(tp.Tipus);
        }
        combobox.AddOptions(ltipus);
	}

    void OnEnable()
    {
        Cursor.visible = true;

        contadorEnemics = 0;
        StartCoroutine(activar());
        enemics = BaseDades.getAllEnemics().ToList<Enemics>();
        carregarDades();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("B_Button") && habilitat)
        {
            Cursor.visible = false;
            StartCoroutine(desactivar());
        }
	}

    //Activar el bestiari
    IEnumerator activar()
    {
        npc.enabled = false;
        //Desactiva moviment i comença l'animacio
        mov.move = false;
        anim.Play("OpenBook", 0);
        yield return new WaitForSeconds(1f);
        //Quan acaba l'animacio, activa el moviment i para el temps
        mov.move = true;
        Time.timeScale = 0;

        //Marca que ja s'ha carregat el bestiari i activa la informacio de les pagines
        habilitat = true;
        if (contadorEnemics > 0)
        {
            triar.SetActive(true);
            info.SetActive(true);
        }
        else
        {
            cap.SetActive(true);
        }
        
    }

    //Desactivar el bestiari
    IEnumerator desactivar()
    {
        if (txt.text.Length > 0)
        {
            txt.text.Remove(0);
        }
        combobox.value = 0;
        //Marca que el bestiari ja no esta habilitat i activa la animacio
        habilitat = false;
        anim.Play("CloseBook", 0);
        info.SetActive(false);
        //Elimina tots els enemics de la llista
        eliminarDades();
        //Desactiva la liista
        triar.SetActive(false);
        cap.SetActive(false);
        //Es torna a activarel temps
        Time.timeScale = 1;
        yield return new WaitForSeconds(.5f);
        npc.enabled = true;
        //A l'acabar l'animacio, es desactiva l'objecte
        gameObject.SetActive(false);
    }

    //Funcio per carregar les dades per triar enemic
    public void carregarDades()
    {
        contadorEnemics = 0;
        foreach (Enemics e in enemics)
        {
            if (SaveData.enemics[e.Id - 1])
            {
                enemics[contadorEnemics] = e;
                pref.GetComponent<InfoMonstre>().setDades(e);
                Instantiate(pref, content);
                contadorEnemics++;
            }
        }
        loadInfo();
    }

    //Funcio per eliminar les dades per triar enemic
    public void eliminarDades()
    {
        
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }

    }

    //Mostra la informacio del enemic clickat
    public void loadInfo()
    {
        if(contadorEnemics > 0)
        {
            info.transform.Find("ImatgeEnemic").GetComponent<Image>().sprite = Resources.Load<Sprite>(enemics[0].Img);
            info.transform.Find("Nom").GetComponent<Text>().text = enemics[0].Nom;
            info.transform.Find("Descripcio").GetComponent<Text>().text = enemics[0].Descripcio;
            info.transform.Find("Tipus").GetComponent<Text>().text = enemics[0].Tipus;
        }
    }

    //Quan el buscador canvia de text
    public void OnTextChanged(string text)
    {
        filtrar(text);
       
    }

    //Qual el combobox canvia de valor
    public void OnValueChanged(int value)
    {
        filtrar(txt.text);
    }  
    
    //Funcio que filtra els enemics depenent de les dades entrades
    void filtrar(string text)
    {
        if (habilitat)
        {
            if (text == "" && combobox.value == 0)
            {
                enemics = BaseDades.getAllEnemics().ToList<Enemics>();
            }
            else if (combobox.value > 0 && text != "")
            {
                enemics = BaseDades.getEnemicByNomTipus(text, ltipus[combobox.value - 1]).ToList<Enemics>();
            }
            else if (text == "" && combobox.value > 0)
            {
                enemics = BaseDades.getEnemicByType(ltipus[combobox.value - 1]).ToList<Enemics>();
            }
            else if (text != "" && combobox.value == 0)
            {
                enemics = BaseDades.getEnemicByNom(text).ToList<Enemics>();
            }

            eliminarDades();
            carregarDades();
        }

    }
}
