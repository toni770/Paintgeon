using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * SCRIPT QUE CARREGARA LA INFORMACIO DEL MONSTRE CLICKAT
 */
public class InfoMonstre2 : MonoBehaviour {


    public string nom, img;
    //Funcio per pasar les dades al boto
    public void setDades(Enemics e)
    {
        this.nom = e.Nom;
        this.img = e.Img;
    }

	// Use this for initialization
	void Start() {

        transform.Find("nom").GetComponent<Text>().text = nom;
        transform.Find("perfil").GetComponent<Image>().sprite = Resources.Load<Sprite>(img + "_icon");
    }

}
