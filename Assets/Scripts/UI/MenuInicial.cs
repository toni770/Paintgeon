using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SCRIPT QUE CONTROLA LES ACCIONS DEL MENU PRINIPAL(CANVIAR DE MENU I APLICAR EFECTES)
 */
public class MenuInicial : MonoBehaviour {

    public GameObject inicial;      //Canvas que mostra l'inici del joc
    public GameObject triarPartida; //Canvas que mostra les partides disponibles
    bool canviat = false;           //Comprova si ha canviat de canvas
    FadeManager fadeM;               //Script encarregat de aplicar l'efecte de fade a la pantalla

	// Use this for initialization
	void Start () {
        fadeM = GameObject.Find("GameManager").GetComponent<FadeManager>();
    }
	
	// Update is called once per frame
	void Update () {


        if (Input.anyKeyDown && !canviat)
        {

            StartCoroutine(canviarMenu());
            canviat = true;
        }
	}

    IEnumerator canviarMenu()
    {
        //Aplica l'efecte de fade i treu el canvas inicial
        fadeM.Fade(true, 1.25f);
        yield return new WaitForSeconds(1.25f);
        inicial.SetActive(false);

        //Activa el canvas de les partides i aplica un altre efecte de fade
        triarPartida.SetActive(true);
        fadeM.Fade(false, 1.25f);
    }
}
