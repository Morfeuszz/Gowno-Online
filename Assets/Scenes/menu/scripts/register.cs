using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class register : MonoBehaviour
{
    public DataHolder DataController;
    public TextFadeOut fadeOutScript;
    public authClient connection;
    public  InputField password, confirmPassword, username, mail;
    public GameObject menuScreen, loginScreen;
    private CanvasFadeOut loginScreenCanvas, menuScreenCanvas, registerScreenCanvas;

    public class userCredentials {
        public string action = "register";
        public string password;
        public string username;
        public string mail;
    }

    public static userCredentials json = new userCredentials();

    void Start()
    {
        DataController = GameObject.Find ("DATA").GetComponent<DataHolder>();
        registerScreenCanvas = GetComponent<CanvasFadeOut>();
        menuScreenCanvas = menuScreen.GetComponent<CanvasFadeOut>();
        loginScreenCanvas = loginScreen.GetComponent<CanvasFadeOut>();
    }
    public void SubmitButton()
    {
        if(password.text != "" && username.text != "" && mail.text != ""){
            if(password.text.Length >= 6){
                if(password.text == confirmPassword.text){
                    json.password = password.text;
                    json.username = username.text;
                    json.mail = mail.text;
                    connection.SendWebSocketMessage(JsonUtility.ToJson(json));
                } else {
                    fadeOutScript.FadeOut("passwords don't match");
                }
            } else{
                fadeOutScript.FadeOut("password has to be atlest 6 characters long");
            }
        } else {
            fadeOutScript.FadeOut("please fill out all required fields");
        }
    }

    
    public void registerRespons(authClient.Action status){
        switch (status.status)
            {
                case "success":
                    menuScreenCanvas.FadeIn();
                    registerScreenCanvas.FadeOut();
                    DataController.data.ID = status.ID;
                    DataController.data.authToken = status.authToken;
                    DataController.data.familyName = status.familyName;
                    DataController.menu.logged = true;
                    break;
                case "taken":
                    fadeOutScript.FadeOut("username taken");
                    break;
                default: 
                    break;
            }

    }

    public void BackButton()
    {
        menuScreenCanvas.FadeIn();
        registerScreenCanvas.FadeOut();
    }
    public void LoginButton()
    {
        registerScreenCanvas.FadeOut();
        loginScreenCanvas.FadeIn();
    }
}
