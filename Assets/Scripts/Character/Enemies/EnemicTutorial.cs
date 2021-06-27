using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemicTutorial : EnemyMovement {

	// Use this for initialization
	public override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        isTriggered = false;
		
	}

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
}
