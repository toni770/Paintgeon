using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public Animator moure, interectuar, atacar, pausa, llum;

    public GameObject intectRay;

    public button_Tutorial btn;

    public Pausa ps;

    AudioSource so;

    bool once = false;

    // Use this for initialization
    void Start() {
        ps.menuPausa = false;

        so = GetComponent<AudioSource>();
        StartCoroutine(start());
    }

    // Update is called once per frame
    void Update() {
        RaycastHit2D hit = Physics2D.Raycast(intectRay.transform.position, new Vector2(intectRay.transform.position.x, intectRay.transform.position.y-100));

        if (hit.collider != null)
        {
            interectuar.SetTrigger("appear");
        }

        if (btn.activat)
        {
            atacar.SetTrigger("appear");
        }

        if(btn.activat && SaveData.monedes > 0)
        {
            if (!once)
            {
                so.Play();
                ps.menuPausa = true;
                pausa.SetTrigger("appear");
                llum.SetTrigger("completed");
                once = true;
            }

        }
    }

    IEnumerator start()
    {
        yield return new WaitForSeconds(1.7f);
        moure.SetTrigger("appear");
    }
}
