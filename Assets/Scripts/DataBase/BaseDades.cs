using SqlCipher4Unity3D;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;


public static class BaseDades{

    static SQLiteConnection con;

    //Conectar a la base de dades
    static void conectar()
    {
      
        string conn = Application.dataPath + "/StreamingAssets/joc.db";

        con = new SQLiteConnection(conn, "P@ssw0rd");

    }

    //Retorna tots els enemics de la base de dades
    public static IEnumerable<Enemics> getAllEnemics()
    {
        conectar();

        IEnumerable<Enemics> lEnemics = con.Query<Enemics>("Select * from Enemics");
        return lEnemics;
    }

    //Retorna tots els enemics de la base de dades del tipus entrat
    public static IEnumerable<Enemics> getEnemicByType(string tipus)
    {
        conectar();

        IEnumerable<Enemics> lEnemics = con.Query<Enemics>("Select * from Enemics Where Tipus = '" + tipus + "'");

        return lEnemics;
    }

    //Retorna tots els enemics de la base de dades del nom entrat
    public static IEnumerable<Enemics> getEnemicByNom(string nom)
    {
        conectar();

        IEnumerable<Enemics> lEnemics = con.Query<Enemics>("Select * from Enemics Where Nom LIKE '%" + nom + "%'");

        return lEnemics;
    }

    //Retorna tots els enemics de la base de dades del nom i tipus entrat
    public static IEnumerable<Enemics> getEnemicByNomTipus(string nom, string tipus)
    {
        conectar();

            IEnumerable<Enemics> lEnemics = con.Query<Enemics>("SELECT * from Enemics where Nom LIKE '%" + nom + "%' AND Tipus = '" + tipus + "'");

            return lEnemics;
    }

    //Retorna tots els tipus distints
    public static IEnumerable<Enemics> getAllTipus()
    {
        conectar();

        IEnumerable<Enemics> lEnemics = con.Query<Enemics>("SELECT DISTINCT Tipus from Enemics");

        return lEnemics;
        
    }


    //Retorna tots els nivells de la base de dades
    public static IEnumerable<Nivells> getAllNivells()
    {
        conectar();

        IEnumerable<Nivells> lNivells = con.Query<Nivells>("Select * from Nivells");
        return lNivells;
    }

    //Retorna tots els enemics d'un nivell
    public static IEnumerable<Enemics> getAllEnemiesByNivell(int id)
    {
        conectar();

        IEnumerable<Enemics> lEnemics = con.Query<Enemics>("Select * from Enemics e JOIN Nivells_Enemics n ON n.IdEnemics = e.Id WHERE n.IdNivells = " + id);
        return lEnemics;
    }

}
