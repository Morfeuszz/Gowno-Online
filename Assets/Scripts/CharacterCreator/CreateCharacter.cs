using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CreateCharacter : MonoBehaviour
{
    public InputField characterName;
    public DataHolder DataController;
    public TextFadeOut fadeOutScript;
    public authClient connection;

    
    void Start(){
       DataController = GameObject.Find ("DATA").GetComponent<DataHolder>();
    }
    public void Create(){
        string url = "http://18.192.38.56/php/createCharacter.php";
        WWWForm form = new WWWForm();
        form.AddField("Name", characterName.text);
        form.AddField("ID", DataController.data.ID  );
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www));  
    }
    IEnumerator WaitForRequest(WWW www)
     {
         yield return www;
         if (www.error == null)
         {
             Debug.Log(www.text);           
            switch (www.text.Substring(0,1))
            {
                case "0":
                    SceneManager.LoadScene("menuNew");
                    break; 
                case "1":
                fadeOutScript.FadeOut("Please enter name.");
                    break;
                case "2":
                fadeOutScript.FadeOut("Name Taken.");
                    break;
                default: 
                    break;
            }

         } else {
            Debug.Log("WWW Error: "+ www.error);
         }    
     }    
}
