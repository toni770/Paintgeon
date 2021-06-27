using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

/*
 * SCRIPT QUE CONTROLA EL MENU DE PAUSA
 */
public class Pausa : MonoBehaviour {

    GameObject pausa;
    public GameObject opcions = null;
    public GameObject messageBox;

    CharacterMovement move;

    public bool menuPausa = true;

    bool salir = false;

	// Use this for initialization
	void Start () {

        pausa = transform.Find("Pausa").gameObject;

        move = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("B_Button") && menuPausa && move.move)
        {
            Cursor.visible = !pausa.activeSelf;
            pausa.SetActive(!pausa.activeSelf);
            Time.timeScale = Convert.ToInt32(!pausa.activeSelf);
        }
	}

    //Renaudar
    public void Renaudar()
    {
        pausa.SetActive(false);
        Time.timeScale = 1;
    }

    //Opcions
    public void Opciones()
    {
        pausa.transform.GetChild(0).gameObject.SetActive(false);
        opcions.SetActive(true);
        menuPausa = false;

    }

    //Tornar al poble
    public void Pueblo()
    {
        menuPausa = false;
        messageBox.SetActive(true);
    }

    //Sortir del joc
    public void Salir()
    {
        menuPausa = false;
        salir = true;
        messageBox.SetActive(true);
    }

    //Si pitxes si
    public void si()
    {
        menuPausa = true;
        if (salir)
        {
            Application.Quit();
        }
        else
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(2);
        }
    }

    //Si pitxes no
    public void no()
    {
        menuPausa = true;
        messageBox.SetActive(false);
    }
}
