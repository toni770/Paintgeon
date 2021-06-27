using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


/*
 * SCRIPT QUE CREA ELS FITXER PER GUARDAR I CARREGAR LES DADES DEL JOC
 */
public static class SaveData
{
    //Dades que es guardaran
    public static bool[] nivells = new bool[3] { false, false, false };     //Nivells completats = True
    public static int monedes = 0;                  //Monedes agafades
    public static float tempsPartida = 0;           //Temps de joc
    public static int partida = 1;                  //Num de partida
    public static float volumGeneral = 1;        //Volum general 
    public static float volumMusica = 1;         //Volum de la musica de fons
    public static bool[] npc = new bool[2] { false, false };         //Npc desbloquejats en el joc
    public static bool[] enemics = new bool[6] { false , false, false, false, false, false };     //Enemics derrotats


    public static PlayerData data=null;             //Variable on es guardaran les dades que volem desar

    //Guardar Partida
    public static void Save(string file) //Player_Movement es l'script on estan les dades a guardar
    {
        //Carpeta on es guardara l'arxiu
        //Comprova si existeix
        if (!Directory.Exists(Application.persistentDataPath+"/Saves"))
            Directory.CreateDirectory(Application.persistentDataPath + "/Saves");

        
        BinaryFormatter formatter = new BinaryFormatter();
        //Arxiu de guardat
        FileStream saveFile = File.Create(Application.persistentDataPath+"/Saves/" +file); 

        //Agafem les dades que volem guardar i les posem a la variable
        data = new PlayerData(file);

        //Guardem la variable data
        formatter.Serialize(saveFile, data);

        //Tenquem fitxer
        saveFile.Close();
    }

    //Carregar Partida
    public static PlayerData Load(string file)
    {
        //Comprova si existeix
        if (File.Exists(Application.persistentDataPath+"/Saves/" +file))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fitxer = File.Open(Application.persistentDataPath+"/Saves/" +file, FileMode.Open);

            //Carrega les dades en la variable data
            data = (PlayerData)bf.Deserialize(fitxer);
           
            fitxer.Close();

            return data;
        }

        return null;
    }

}


[Serializable]          //Permetre guardar les dades de sota
public class PlayerData //Clase on es guarden per centralitzar totes les dades que volem guardar en una sola clase
{
    public int monedes;
    public float tempsPartida;
    public bool[] nivells = new bool[3];
    public float volumGeneral;        
    public float volumMusica;
    public bool[] npc = new bool[2];
    public bool[] enemics = new bool[6];

    //Constructor
    public PlayerData(string fitxer) 
    {

        monedes = SaveData.monedes;
        tempsPartida = SaveData.tempsPartida + Time.time;
        nivells = SaveData.nivells;
        volumGeneral = SaveData.volumGeneral;
        volumMusica = SaveData.volumMusica;
        npc = SaveData.npc;
        enemics = SaveData.enemics;
    }
}