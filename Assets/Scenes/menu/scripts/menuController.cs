using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuController : MonoBehaviour
{
    public GameObject serverScreen, logo;
    private CanvasFadeOut serverScreenCanvas, menuScreenCanvas, logoCanvas;
    public characterSelect characterSelectScript;
    void Start()
    {
        serverScreenCanvas = serverScreen.GetComponent<CanvasFadeOut>();
        logoCanvas = logo.GetComponent<CanvasFadeOut>();
        menuScreenCanvas = GetComponent<CanvasFadeOut>();
    }
    public void StartButton()
    {
        logoCanvas.FadeOut();
        menuScreenCanvas.FadeOut();
        serverScreenCanvas.FadeIn();
        characterSelectScript.GetCharacters();
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
