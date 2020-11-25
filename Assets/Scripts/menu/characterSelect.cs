﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class characterSelect : MonoBehaviour
{
 [System.Serializable]
    public class CharacterInfo{
        public string Name;
        public int Level;
        public int ID;
        public int Exp;
        public string Position;

        
    }
    [System.Serializable]
    public class CharacterWidget{
        public Text Name;
        public Text Level;
        public Button button;
    }

    [System.Serializable]
    public class SQLResult
    {
    public CharacterInfo[] result;
    }
    public  SQLResult charactersDB;
    public GameObject widgetObject, statusPanel, newCharacterButton; 
    private DataHolder DataController;
    private mouseLock MouseLock;

    void Start(){
        DataController = GameObject.Find ("DATA").GetComponent<DataHolder>();
        MouseLock = GameObject.Find ("DATA").GetComponent<mouseLock>();
        GetCharacters();
    }

    public void GetCharacters () {

        string url = "http://18.192.38.56/php/getCharacters.php";
        //string url = "http://127.0.0.1/getCharacters.php";

        WWWForm form = new WWWForm();
        form.AddField("ID", DataController.data.ID);
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www));
    }
     IEnumerator WaitForRequest(WWW www)
     {
         yield return www;
         if (www.error == null)
         {
            string temp = fixJson(www.text);
            Debug.Log(temp);
            charactersDB = JsonUtility.FromJson<SQLResult>(temp);
            loadCharacters();
    
         } else {
            Debug.Log("WWW Error: "+ www.error);
         }    
     }    

    void loadCharacters(){
        foreach(CharacterInfo character in charactersDB.result){
            CharacterWidget newWidget = new CharacterWidget();
            GameObject widget = Instantiate(widgetObject, statusPanel.transform);
            widget.name = character.Name +"Character";
            newWidget.Name = GameObject.Find(character.Name +"Character/Name").GetComponent<Text>();
            newWidget.Level = GameObject.Find(character.Name +"Character/Level").GetComponent<Text>();
            newWidget.button = GameObject.Find(character.Name +"Character/Button").GetComponent<Button>();
            newWidget.button.onClick.AddListener(delegate {connect(character); });
            newWidget.Name.text = character.Name;
            newWidget.Level.text = "Level: " + character.Level;
        }
            GameObject newCharButton = Instantiate(newCharacterButton, statusPanel.transform);
            Button butt = GameObject.Find("ButtonNewChar").GetComponent<Button>();
            butt.onClick.AddListener(delegate {loadCreator(); });
    }

    public void loadCreator(){
        SceneManager.LoadScene("CharacterCreator");
    }

    public void connect(CharacterInfo character){
        DataController.data.characterName = character.Name;
        DataController.data.characterID = character.ID;
        DataController.data.Level  = character.Level;
        DataController.data.EXP = character.Exp;
        string[] tempPostion = character.Position.Split(',');
        DataController.data.position = new Vector3(float.Parse(tempPostion[0]),float.Parse(tempPostion[1]),float.Parse(tempPostion[2]));
        SceneManager.LoadScene(1);
        MouseLock.canChange = true;
        MouseLock.ChangeLock();
    }

    string fixJson(string value)
    {
        value = "{\"result\":" + value + "}";
        return value;
    }


}
