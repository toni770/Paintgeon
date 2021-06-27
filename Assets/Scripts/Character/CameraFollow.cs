using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SCRIPT QUE FA QUE LA CAMARA SEGUEIXI Al JUGADOR RESPECTANT ELS LIMITS
 * */
public class CameraFollow : MonoBehaviour {
    
    private Transform personatge;   //Personatge principal

    [Header("Limits del seguiment de la camera")]
    public float maxX;
    public float maxY;
    public float minX;
    public float minY;

    // Use this for initialization
    void Start () {
        personatge = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {

        //Mou la camera a la posicio del personatge sense pasar-se dels limits. 
        //Mathf.Clamp fa que el primer valor estigui sempre entre el segon i el tercer.Per exemple si es pasa de llarg, el numero sempre sera el maxim. 
        transform.position = new Vector3(Mathf.Clamp(personatge.position.x,minX,maxX),Mathf.Clamp(personatge.position.y, minY, maxY), transform.position.z);
	}

    public void canviarLimits(float maxX, float minX, float maxY, float minY)
    {
        this.maxX = maxX;
        this.minX = minX;
        this.maxY = maxY;
        this.minY = minY;
    }
}
