using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/*
 * SCRIPT ENCARREGAT QUE GESTIONAR LES OPCIONS DEL JOC
 */
public class Opcions : MonoBehaviour {

    public Dropdown qualitat, resolucio;                //Combobox que controles la qualitat i la resolucio de la pantalla
    public Toggle finestra;                             //Checkbox que controla si esta en modo finestra o no
    public Slider musicaFons, musicaGeneral;            //Sliders que controlen la musica de fons i la musica general


    List<string> llistaQualitats, llistaResolucions;    //Llista de resolucions i qualitats per emplenar el combobox
    Resolution[] resolucions;                           //Llista de resolucions disponibles
    Resolution resolucioActual;                         //Resolucio actual
    int qualitatActual;                                 //Qualitat actual
    int posicioResolucio;                               //Posicio de la resolucio actual a la llista
    bool finestraInicial;                               //Si esta en modo finestra inicialment
    bool pausa = true;                                         //Comprova si esta en pausa abans d'obrir el menu

    public AudioSource maracas = null;

    AudioSource fons;                                   //Musica que sona de fons

    public NpcPoble npc = null;                         //Npc del poble

    
    void Awake()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>() != null)
        {
            fons = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        }

        //Carreguem resolucions disponibles
        resolucions = Screen.resolutions;

        //Emplenem la llista de qualitats
        llistaQualitats = new List<string>(QualitySettings.names);

        //Emplenem la llista de resolucions amb les resolucions disponibles
        llistaResolucions = new List<string>();
        if (Screen.fullScreen)
        {
            resolucioActual = Screen.currentResolution;
        }

        for (int i = 0; i < resolucions.Length; i++)
        {
            Resolution res = resolucions[i];
            llistaResolucions.Add(res.width + " x " + res.height);
        }


        //Posem les dades de les llistes als combobox
        qualitat.AddOptions(llistaQualitats);
        resolucio.AddOptions(llistaResolucions);
    }

    // Use this for initialization
    void OnEnable () {

        Cursor.visible = true;

        if(npc != null)
        {
            npc.enabled = false;
        }
        if(Time.timeScale > 0)
        {
            Time.timeScale = 0;
            pausa = false;
        }


        //Trobat la posicio de la resolucio actual
        bool trobat = false;
        int i = 0;
        while (!trobat &&i<resolucions.Length)
        {
            //Si la resolucio es igual a la actual, guarda la posicio
            if (resolucions[i].width + resolucions[i].height == resolucioActual.width + resolucioActual.height)
            {
                posicioResolucio = i;
                trobat = true;
            }

            i++;
        }
        //Posar parametres per defecte
        qualitatActual = QualitySettings.GetQualityLevel();
        qualitat.value = qualitatActual;

        resolucio.value = posicioResolucio;

        finestraInicial = !Screen.fullScreen;

        finestra.isOn = finestraInicial;

        musicaFons.value = SaveData.volumMusica;
        musicaGeneral.value = SaveData.volumGeneral;


    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("B_Button"))
        {
            cancelar();
        }
	}

    //La funció s'executara quan el combobox de la resolucio canvii
    public void canviarResolucio(int value)
    {
        string[] parts = llistaResolucions[value].Split(new char[] { ' ' });
        Screen.SetResolution(Int32.Parse(parts[0]), Int32.Parse(parts[2]),!finestra.isOn);
    }

    //La funció s'executara quan el combobox de la qualitat canvii
    public void canviarQualitat(int value)
    {
        QualitySettings.SetQualityLevel(value);
    }


    //Quan el checkbox canvii, canviara el valor de la pantalla completa
    public void canviarFinesra(bool value)
    {
        Screen.fullScreen = !value;
    }

    //Quan es canvia el valor del slider de  Volum General
    public void canviarVolumGeneral()
    {
        AudioListener.volume = musicaGeneral.value;
    }

    //Quan es canvia el valor del slider de  Volum de Fons
    public void canviarVolumFons()
    {

        fons.volume = musicaFons.value;
        if (maracas != null)
        {
            maracas.volume = fons.volume;
        }
    }

    //Quan es cancela els canvis
    public void cancelar()
    {
        //Posar parametres per defecte
        QualitySettings.SetQualityLevel(qualitatActual);

        Screen.SetResolution(resolucioActual.width,resolucioActual.height, !finestraInicial);

        musicaFons.value = SaveData.volumMusica;
        musicaGeneral.value = SaveData.volumGeneral;

        desactivar();

    }

    //Quan es guarda la configuració
    public void guardar()
    {
        SaveData.volumGeneral = musicaGeneral.value;
        SaveData.volumMusica = musicaFons.value;
        resolucioActual = resolucions[resolucio.value];

        desactivar();
    }

    void desactivar()
    {
        //Si s'ha obert mitjançant un NPC
        if (npc != null)
        {
            npc.enabled = true;
        }

        if (!pausa)
        {
            Time.timeScale = 1;
            Cursor.visible = false;
        }


        if (pausa)
        {
            transform.parent.transform.GetChild(0).gameObject.SetActive(true);
            transform.parent.transform.parent.GetComponent<Pausa>().menuPausa = true;
        }
        gameObject.SetActive(false);
    }
}
