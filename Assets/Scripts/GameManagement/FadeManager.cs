using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * SCRIPT ENCARREGAR D'APLICAR UN EFECTE DE FADE
 */
public class FadeManager : MonoBehaviour {

    public Image imatge;    //Imatge que fara l'efecte de fade. En aquest cas sera tot negre.

    public GameObject loadingText;

    bool loadScene = false;

    bool estaCanviant;      //Comprova s'esta fent l'efecte de fade.
    float alpha;            //Intensitat de la imatge
    bool apareixer;         //Comprova si la imatge ha d'apareixer o desapareixer
    float duracio;          //Duracio de l'efecte

    void Awake()
    {
        //No destrueix l'objecte on esta l'script al canviar d'escena
        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (estaCanviant)
        {
            //Si ha d'apareixer, suma el valor de alpha. Si ha de desapareixer, resta.
            if (apareixer)
            {
                alpha += Time.deltaTime * (1 / duracio);
            }
            else
            {
                alpha -= Time.deltaTime * (1 / duracio);
            }

            //Aplicar alpha
            imatge.color = new Color(imatge.color.r, imatge.color.g, imatge.color.b, alpha);

            if (alpha > 1 || alpha < 0)
            {
                estaCanviant = false;
                //Es posa darrere de tots els canvas per que es pugui interectuar amb ells.
                if (!apareixer)
                {
                    imatge.canvas.sortingOrder = -100;
                }
            }
        }


    }

    public void Fade(bool apareixer, float duracio)
    {
        //Es posa devant de tots els canvas
        imatge.canvas.sortingOrder = 10000;


        this.apareixer = apareixer;
        this.duracio = duracio;

        estaCanviant = true;

        //Si ha d'apareixer, l'alpha comença per 0, si no, comença per 1
        if (this.apareixer)
        {
            alpha = 0;
        }
        else
        {
            alpha = 1;
        }
    }

    public IEnumerator LoadNewScene(int scene)
    {
        

        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        yield return new WaitForSeconds(0.9f);

       /* if (!loadingText.activeSelf)
        {
            loadingText.SetActive(true);
        }*/
        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            yield return null;
        }

    }

    //Funcions ecarregades de aplicar l'efecte de Fade sempre que es acarregui una escena
    void OnEnable()
    {
        //Li diem a la nostra funcio que escolti per un cavi d'escena nomes carregar-se l'script
        SceneManager.sceneLoaded += EscenaCarregada;
    }

    void OnDisable()
    {
        //Li diem a la nostra funcio que deixi d'escoltar
        SceneManager.sceneLoaded -= EscenaCarregada;
    }

    private void EscenaCarregada(Scene scene, LoadSceneMode mode)
    {
        //loadingText.SetActive(false);
        Fade(false, 1.25F);

        if(scene.buildIndex > 0)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }

        if (GameObject.Find("CanvasPrincipal")!= null)
        {
            GameObject.Find("CanvasPrincipal").transform.Find("coin_image").GetChild(0).GetComponent<Text>().text = SaveData.monedes.ToString();
        }
    }
}
