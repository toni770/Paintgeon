using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CameraAnimation : MonoBehaviour {

    CameraFollow cam;
    Animator anim;
    PlayerMovement mov;

    public float duracio = 3;

	// Use this for initialization
	void Start () {

        cam = GetComponent<CameraFollow>();
        anim = GetComponent<Animator>();

        mov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        mov.move = false;
        cam.enabled = false;
        StartCoroutine(activar());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator activar()
    {
        yield return new WaitForSeconds(duracio);
        cam.enabled = true;
        mov.move = true;
    }

}
