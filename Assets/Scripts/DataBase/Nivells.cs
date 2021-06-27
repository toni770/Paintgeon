using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivells{

    int id;
    string nom;
    string descripcio;
    string imatge;

    public int Id { get { return this.id; } set { this.id = value; } }
    public string Nom { get { return this.nom; } set { this.nom = value; } }
    public string Descripcio { get { return this.descripcio; } set { this.descripcio = value; } }
    public string Imatge { get { return this.imatge; } set { this.imatge = value; } }
}
