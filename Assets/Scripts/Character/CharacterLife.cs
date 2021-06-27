using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SCRIPT QUE CONTROLA TOTS ELS 
 */
public class CharacterLife : MonoBehaviour {

    public int vidaMaxima = 100;            //Vida maxima del personatge
    public float vidaPersonatge = 100;      //Vida del personatge
    public GameObject barraVida;                   //Barra de vida 

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

    //Metode per comprovar que esta viu
    public bool estaViu()
    {
        return vidaPersonatge > 0;
    }

    //Metode per treure vida
    public void treureVida(float quantitat, int velocitat)
    {
        if (estaViu())
        {
            vidaPersonatge = Mathf.Clamp(vidaPersonatge -= quantitat,0,vidaMaxima);

            StartCoroutine(ActulitzarBarra(vidaPersonatge / vidaMaxima,velocitat));
        }
    }

    //Metode per augmentar vida
    public void augmentarVida(float quantitat, int velocitat)
    {
        vidaPersonatge = Mathf.Clamp(vidaPersonatge += quantitat, 0, vidaMaxima);

        StartCoroutine(ActulitzarBarra(vidaPersonatge / vidaMaxima,velocitat));
    }

    //Metode per actualitzar la barra de vida
    IEnumerator ActulitzarBarra(float vida, int velocitat)
    {
        //Actualitza la barra vida frame a frame, aixi baixa seqüencialment
        //Resta
        if (barraVida.transform.localScale.x > vida)
        {
            //Mentres la barra de vida sigui mes gran que la vida, disminuir fins que sigui igual.
            while (barraVida.transform.localScale.x > vida)
            {
                barraVida.transform.localScale = new Vector3(Mathf.Clamp(barraVida.transform.localScale.x - 0.001f*velocitat,0,1), barraVida.transform.localScale.y, barraVida.transform.localScale.z);
                yield return null;
            }
        }

        //Suma
        else if (barraVida.transform.localScale.x < vida)
        {
            while (barraVida.transform.localScale.x < vida)
            {
                barraVida.transform.localScale = new Vector3(Mathf.Clamp(barraVida.transform.localScale.x + 0.001f*velocitat,0,1), barraVida.transform.localScale.y, barraVida.transform.localScale.z);
                yield return null;
            }
        }

    }
}
