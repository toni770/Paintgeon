using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemic1 : EnemyMovement
{

    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        isTriggered = false;

        //Si tant l'enemic com el jugador estan vius i es pot moure.
        if (vida.estaViu() && move)
        {
            atacarJugador();
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
}
