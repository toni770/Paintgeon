using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * SCRIPT QUE CARREGARA LA INFORMACIO DEL MONSTRE CLICKAT
 */
public class InfoMonstre : MonoBehaviour {

    GameObject info;             //GameObject on es mostraran les dades

    public int id;
    public string nom, descripcio, img, tipus;
    //Funcio per pasar les dades al boto
    public void setDades(Enemics e)
    {
        this.id = e.Id;
        this.nom = e.Nom;
        this.descripcio = e.Descripcio;
        this.img = e.Img;
        this.tipus = e.Tipus;
    }

	// Use this for initialization
	void Start() {
        info = GameObject.Find("InfoEnemic");

        //Carrega les dades al boto
        GetComponent<Button>().onClick.AddListener(setInfo);
        transform.Find("Nom").GetComponent<Text>().text = nom;
        transform.Find("Text_tipus").GetComponent<Text>().text = tipus;
        transform.Find("Id").GetComponent<Text>().text = id.ToString();
        transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(img + "_icon");
    }
	
    //Funcio que posara la informacio del monstre a la pagina
    void setInfo()
    {
        //Carrega les dades a la pagina
        info.transform.Find("ImatgeEnemic").GetComponent<Image>().sprite = Resources.Load<Sprite>(img);
        info.transform.Find("Nom").GetComponent<Text>().text = nom;
        info.transform.Find("Descripcio").GetComponent<Text>().text = descripcio;
        info.transform.Find("Tipus").GetComponent<Text>().text = tipus;
    }

}
