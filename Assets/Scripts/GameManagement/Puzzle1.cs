using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : MonoBehaviour {

    public GameObject obstacle;
    public button_premut button;
    public GameObject raycast;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(raycast.transform.position, new Vector2(raycast.transform.position.x, raycast.transform.position.y - 100));

        Debug.DrawLine(raycast.transform.position, new Vector2(raycast.transform.position.x, raycast.transform.position.y - 100));

        if (hit.collider != null)
        {
            Destroy(button);

            if (obstacle.activeSelf)
            {
                obstacle.SetActive(false);
            }
        }
    }
}
