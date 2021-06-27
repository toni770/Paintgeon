using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CLASSE ENEMICS PER GUARDAR LES DADES DELS ENEMICS DE LA BASE DE DADES
 */
public class Enemics{

    
    int id;
    string nom;
    string descripcio;
    string img;
    string tipus;

    //Getters and setters
    public int Id { get { return this.id; } set { this.id = value; } }
    public string Nom { get { return this.nom; } set { this.nom = value; } }
    public string Descripcio { get { return this.descripcio; } set { this.descripcio = value; } }
    public string Img { get { return this.img; } set { this.img = value; } }
    public string Tipus { get { return this.tipus; } set { this.tipus = value; } }

}
