using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 using UnityEngine.Events;
 using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{


    public  InputField usernameLogin;
    public InputField passwordLogin;
    public  InputField usernameRegister;
    public InputField passwordRegister;
    public InputField confirmPassword;
    public TextFadeOut fadeOutScript;
    public DataHolder DataController;

    
     public void Login () {
 
         string url = "http://127.0.0.1/login.php";
 
         WWWForm form = new WWWForm();
         form.AddField("username", usernameLogin.text);
         form.AddField("password", passwordLogin.text);
         WWW www = new WWW(url, form);
         StartCoroutine(WaitForRequest(www));
     }

     public void Register () {
 
         string url = "http://127.0.0.1/register.php";
 
         WWWForm form = new WWWForm();
         form.AddField("username", usernameRegister.text);
         form.AddField("password", passwordRegister.text);
         form.AddField("confirm_password", confirmPassword.text);
         WWW www = new WWW(url, form);
         StartCoroutine(WaitForRequest(www));
     }
     IEnumerator WaitForRequest(WWW www)
     {
         yield return www;
 
         // check for errors
         if (www.error == null)
         {
             Debug.Log(www.text);
             switch (www.text.Substring(0,1))
            {
                case "0":
                DataController.Login(www.text.Substring(2,www.text.Length-6));
                SceneManager.LoadScene(1);
                    break;
                case "9":
                DataController.Login(www.text.Substring(2,www.text.Length-3));
                SceneManager.LoadScene(1);
                    break;  
                case "1":
                fadeOutScript.FadeOut("Please enter username.");
                    break;
                case "2":
                fadeOutScript.FadeOut("Please enter password.");
                    break;
                case "3":
                fadeOutScript.FadeOut("Wrong username or password.");
                    break;
                case "4":
                fadeOutScript.FadeOut("This username is already taken.");
                    break;
                case "5":
                fadeOutScript.FadeOut("Password must have atleast 6 characters.");
                    break;
                case "6":
                fadeOutScript.FadeOut("Please confirm password.");
                    break;
                case "7":
                fadeOutScript.FadeOut("Password did not match.");
                    break;
                
                default: 
                
                    break;
            }
         } else {
             Debug.Log("WWW Error: "+ www.error);
         }    
     }    



}
