using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SCRIPT QUE CONTROLA LA PROFUNDITAT DE LES PARTS PER QUE NO ES SOBREPOSIN
 */
public class DepthParts : MonoBehaviour {


    private SpriteRenderer sr;  //Component que controla la profunditat.
    SpriteRenderer srProta;
    int srInicial;

    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        srProta = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        srInicial = sr.sortingOrder;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Depenent de a la profunditat del personatge principal, canvia la profunditat d'aquest objecte
        sr.sortingOrder = srProta.sortingOrder + srInicial;
    }
}
