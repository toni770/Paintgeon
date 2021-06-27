/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public static class DataBase
{
    static IDbCommand dbcmd;
    static IDbConnection dbconn;
    static IDataReader reader;

    //Conectar a la base de dades
    static void conectar()
    {
        string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/joc.db;password=P@ssw0rd"; //Path to database.


        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        dbcmd = dbconn.CreateCommand();
    }

    //Desconectar a la base de dades
    static void desconectar()
    {

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

    //Retorna tots els enemics de la base de dades
    public static List<Enemics> getAllEnemics()
    {
        conectar();

        List<Enemics> lEnemics = new List<Enemics>();

        string sqlQuery = "SELECT id, nom, descripcio, img, tipus from Enemigos;";
        dbcmd.CommandText = sqlQuery;
        reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            Enemics e = new Enemics(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
            lEnemics.Add(e);
        }

        desconectar();
        return lEnemics;
    }

    //Retorna tots els enemics de la base de dades depenent del tipus pasat per parametre
    public static List<Enemics> getEnemicByType(string type)
    {
        conectar();

        List<Enemics> lEnemics = new List<Enemics>();

        string sqlQuery = "SELECT id, nom, descripcio, img, tipus from Enemigos where tipus = '" + type+"';";
        dbcmd.CommandText = sqlQuery;
        reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            Enemics e = new Enemics(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
            lEnemics.Add(e);
        }

        desconectar();

        return lEnemics;
    }

    //Retorna tots els enemics de la base de dades que contenen el nom pasat per parametre
    public static List<Enemics> getEnemicByNom(string text)
    {
        conectar();

        List<Enemics> lEnemics = new List<Enemics>();

        string sqlQuery = "SELECT id, nom, descripcio, img, tipus from Enemigos where nom LIKE '%"+text+"%'";
        dbcmd.CommandText = sqlQuery;
        reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            Enemics e = new Enemics(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
            lEnemics.Add(e);
        }

        desconectar();

        return lEnemics;
    }

    //Retorna tots els enemics de la base de dades que contenen el nom pasat per parametre i el tipus
    public static List<Enemics> getEnemicByNomTipus(string text, string tipus)
    {
        conectar();

        List<Enemics> lEnemics = new List<Enemics>();

        string sqlQuery = "SELECT id, nom, descripcio, img, tipus from Enemigos where nom LIKE '%" + text + "%' AND tipus = '" + tipus+"'";
        dbcmd.CommandText = sqlQuery;
        reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            Enemics e = new Enemics(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
            lEnemics.Add(e);
        }

        desconectar();

        return lEnemics;
    }

    //Retorna tots els tipus
    public static List<string> getAllTipus()
    {
        conectar();

        List<string> ltipus = new List<string>();

        string sqlQuery = "SELECT DISTINCT tipus from Enemigos";
        dbcmd.CommandText = sqlQuery;
        reader = dbcmd.ExecuteReader();

        while (reader.Read())
        {
            ltipus.Add(reader.GetString(0));
        }

        desconectar();

        return ltipus;
    }


}
*/