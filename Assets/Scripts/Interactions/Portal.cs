using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

    public int id = 0;

    FadeManager fd;
	// Use this for initialization
	void Start () {
        fd = GameObject.Find("GameManager").GetComponent<FadeManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            SaveData.nivells[id] = true;
            StartCoroutine(loadScene());
        }
    }

    IEnumerator loadScene()
    {
        fd.Fade(true, 3);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(2);

    }
}
