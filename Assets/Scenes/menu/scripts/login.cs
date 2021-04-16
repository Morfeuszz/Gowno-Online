using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class login : MonoBehaviour
{
    public DataHolder DataController;
    public TextFadeOut fadeOutScript;
    public authClient connection;
    public  InputField password , username;
    public GameObject menuScreen, registerScreen;
    private CanvasFadeOut loginScreenCanvas, menuScreenCanvas, registerScreenCanvas;

    public class userCredentials {
        public string action = "login";
        public string password;
        public string username;
    }
    public static userCredentials json = new userCredentials();


    void Start()
    {
        DataController = GameObject.Find ("DATA").GetComponent<DataHolder>();
        loginScreenCanvas = GetComponent<CanvasFadeOut>();
        menuScreenCanvas = menuScreen.GetComponent<CanvasFadeOut>();
        registerScreenCanvas = registerScreen.GetComponent<CanvasFadeOut>();
    }
    public void SubmitButton()
    {
        if(password.text != "" && username.text != "" ){
            json.password = password.text;
            json.username = username.text;
            connection.SendWebSocketMessage(JsonUtility.ToJson(json));
        } else {
            fadeOutScript.FadeOut("please fill out all required fields");
        }
    }

    public void loginRespons(authClient.Action status){
        switch (status.status)
            {
                case "success":
                    menuScreenCanvas.FadeIn();
                    loginScreenCanvas.FadeOut();
                    DataController.data.ID = status.ID;
                    DataController.data.authToken = status.authToken;
                    DataController.data.familyName = status.familyName;
                    DataController.menu.logged = true;
                    break;
                case "failed":
                    fadeOutScript.FadeOut("wrong username/passowrd");
                    break;
                default: 
                    break;
            }

    }

    public void BackButton()
    {
        menuScreenCanvas.FadeIn();
        loginScreenCanvas.FadeOut();
    }
    public void RegisterButton()
    {
        registerScreenCanvas.FadeIn();
        loginScreenCanvas.FadeOut();
    }
}
