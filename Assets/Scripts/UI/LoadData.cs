using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
/*
 * SCRIPT QUE CARREGA LES DADES EN EL MENU D'INICI I CARREGA LA PARTIDA QUE ES SELECCIONA
 */
public class LoadData : MonoBehaviour {

    public GameObject messageBox;                           //Objecte que es mostrara que es vulgui borrar una partida

    GameObject[] partides = new GameObject[3];              //Partides disponibles
    GameObject text;                                        //Text per mostrar les dades
    PlayerData[] pd = new PlayerData[3];                    //Dades de cada partida
    FadeManager fd;                                         //Script que aplica l'efecte de fade

    AudioSource so;

    int numBorrar;                                          //Numero de la partida que es vol borrar
    
	// Use this for initialization
	void Start () {

        fd = GameObject.Find("GameManager").GetComponent<FadeManager>();

        so = GetComponent<AudioSource>();

        mostrarDades();
        
	}

    void mostrarDades()
    {
        //Mostrar les dades de cada partida
        for (int i = 0; i < partides.Length; i++)
        {
            partides[i] = GameObject.Find("Partida" + i);
            CarregarDades(i);
        }
    }
    //Mostrar les dades per pantalla
    void CarregarDades(int partida)
    {
        //Guardar les dades depen de la partida
        pd[partida] = SaveData.Load("save" + partida+".sav");

        //Si no hi han dades
        if (pd[partida] == null)
        {
            //Mostrar el text que es el primer fill: El de nova partida
            text = partides[partida].transform.GetChild(0).gameObject;
            text.SetActive(true);
        }
        //Si hi han dades
        else
        {
            //Mostrar el text que es el segon fill: El de partida ja creada
            text = partides[partida].transform.GetChild(1).gameObject;
            text.SetActive(true);

            //Activar boto de borrar partida
            partides[partida].transform.GetChild(2).gameObject.SetActive(true);

            //Posar textos
            text.GetComponent<Text>().text = "Partida " + (partida + 1);
            
            Text mon = text.transform.GetChild(0).gameObject.GetComponent<Text>();
            mon.text = "Monedas: " + pd[partida].monedes;

            Text temps = text.transform.GetChild(1).gameObject.GetComponent<Text>();

            temps.text = "Tiempo de partida: " + GetTime(pd[partida].tempsPartida);
        }
    }

    //Variable que transforma els segons en hores, minuts i segons
    string GetTime(float segons)
    {
        TimeSpan time = TimeSpan.FromSeconds(segons);
        return string.Format("{0:D2}:{1:D2}:{2:D2}",
                time.Hours,
                time.Minutes,
                time.Seconds);
    }

    //Carregar la partida seleccionada
    public void CarregarPartida(int valor)
    {
        so.Play();
        SaveData.partida = valor;

        if (pd[valor] != null)
        {
            SaveData.monedes = pd[valor].monedes;
            SaveData.tempsPartida = pd[valor].tempsPartida;
            SaveData.nivells = pd[valor].nivells;
            SaveData.volumGeneral = pd[valor].volumGeneral;
            SaveData.volumMusica = pd[valor].volumMusica;
            SaveData.enemics = pd[valor].enemics;
            SaveData.npc = pd[valor].npc;
            fd.Fade(true, 1);
            StartCoroutine(fd.LoadNewScene(2));
        }
        else
        {
            fd.Fade(true, 1);
            StartCoroutine(fd.LoadNewScene(1));
        }
    }

   
    //Funcio que borra la partida
    public void borrarPartida(int num)
    {
        messageBox.SetActive(true);

        numBorrar = num;
    }

    //Si vols borrar
    public void dialogYes()
    {
        messageBox.SetActive(false);

        File.Delete(Application.persistentDataPath + "/Saves/" + "save" + numBorrar + ".sav");

        //Desactivem botons
        partides[numBorrar].transform.GetChild(1).gameObject.SetActive(false);
        partides[numBorrar].transform.GetChild(2).gameObject.SetActive(false);

        //Tornem a carregar les dades
        mostrarDades();
        
    }

    //Si no vols borrar
    public void dialogNo()
    {
        messageBox.SetActive(false);
    }

    //Funcio per tencar el joc
    public void close()
    {
        Application.Quit();
    }
}
