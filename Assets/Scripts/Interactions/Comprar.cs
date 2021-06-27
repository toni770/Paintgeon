using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Comprar : MonoBehaviour
{
    public GameObject msgBox;

    public Text mon;

    public GameObject avis;

    FadeManager fade;

    Obres ob;

    PlayerMovement mov;

    AudioSource so;

    GameObject destruir, crear;
    // Use this for initialization
    void Start()
    {
        fade = GameObject.Find("GameManager").GetComponent<FadeManager>();

        mov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        so = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (msgBox.activeSelf && Input.GetButtonDown("B_Button"))
        {
            Cursor.visible = false;
            msgBox.SetActive(false);
            mov.move = true;
        }
    }

    public void comprar(GameObject destruir, GameObject crear)
    {

        this.destruir = destruir;
        this.crear = crear;

        ob = destruir.transform.GetChild(0).GetComponent<Obres>();

        msgBox.SetActive(true);
        msgBox.transform.GetChild(0).GetComponent<Text>().text = "Quieres construir esta zona? Cuesta " + ob.preu + " moneda/s.";
        
    }

    public void si()
    {

        if(ob.preu <= SaveData.monedes)
        {
            StartCoroutine(compra());
            mov.move = true;
            Cursor.visible = false;
        }
        else
        {
            avis.SetActive(true);
        }
    }

    IEnumerator compra()
    {
        msgBox.SetActive(false);
        fade.Fade(true, 2);

        yield return new WaitForSeconds(3);
        so.Play();
        yield return new WaitForSeconds(2);
        

        SaveData.monedes -= ob.preu;
        mon.text = SaveData.monedes.ToString();

        SaveData.npc[ob.id - 1] = true;

        Destroy(destruir);

        crear.SetActive(true);

        fade.Fade(false, 2);
    }


    public void no()
    {
        msgBox.SetActive(false);
        mov.move = true;
        Cursor.visible = false;
    }

    public void ok()
    {
        avis.SetActive(false);
    }
}
