using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * SCRIPTS PER GESTIONAR ELS INPUTS DEL JOC(MANDO/TECLAT)
 */
public static class InputManager
{
    //AXIS
    public static float MainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_MainHorizontal");
        r += Input.GetAxis("K_MainHorizontal");
        return Mathf.Clamp(r, -1.0f, 1 - 0f);
    }

    public static float MainVertical()
    {
        float r = 0.0f;
        r += Input.GetAxis("J_MainVertical");
        r += Input.GetAxis("K_MainVertical");
        return Mathf.Clamp(r, -1.0f, 1 - 0f);
    }

    //BUTTONS
    public static bool AButton()
    {
        return Input.GetButtonDown("A_Button");
    }

    public static bool BButton()
    {
        return Input.GetButtonDown("B_Button");
    }

    public static bool XButton()
    {
        return Input.GetButtonDown("X_Button");
    }

    public static bool YButton()
    {
        return Input.GetButtonDown("Y_Button");
    }
}
