using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

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
    public CharacterInfo[] data;
    }
    public  SQLResult charactersDB;
    public GameObject widgetObject, statusPanel, newCharacterButton, serverSelect, blackScreen; 
    private CanvasFadeOut characterSelectCanvas, serverSelectCanvas, blackScreenCanvas;
    private DataHolder DataController;
    public authClient auth;
    private mouseLock MouseLock;

    void Start(){
        DataController = GameObject.Find ("DATA").GetComponent<DataHolder>();
        MouseLock = GameObject.Find ("DATA").GetComponent<mouseLock>();
        characterSelectCanvas = GetComponent<CanvasFadeOut>();
        serverSelectCanvas = serverSelect.GetComponent<CanvasFadeOut>();
    }


    public void GetCharacters () {
        Array.Clear(charactersDB.data, 0 ,charactersDB.data.Length);
        foreach (Transform child in statusPanel.transform)
        {
            Destroy(child.gameObject);
        }
        var mess = "{\"action\" : \"getCharacters\", \"ID\" : \"" + DataController.data.ID + "\"}";
       auth.SendWebSocketMessage(mess);
    }


    public void loadCharacters(string message){
        Debug.Log(message);
        charactersDB = JsonUtility.FromJson<SQLResult>(message);

        foreach(CharacterInfo character in charactersDB.data){
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
        DataController.menu.characterCreator = true;
        SceneManager.LoadScene("CharacterCreator");
    }

    public void connect(CharacterInfo character){
        DataController.data.characterName = character.Name;
        DataController.data.characterID = character.ID;
        DataController.data.Level  = character.Level;
        DataController.data.EXP = character.Exp;
        string[] tempPostion = character.Position.Split(',');
        DataController.data.position = new Vector3(float.Parse(tempPostion[0]),float.Parse(tempPostion[1]),float.Parse(tempPostion[2]));
        auth.SendWebSocketMessage("{\"action\" : \"loadCharacterData\", \"ID\" : \"" + DataController.data.ID + "\", \"charID\" : \"" + character.ID + "\"}");
        MouseLock.canChange = true;
        //MouseLock.ChangeLock();
        StartCoroutine(LoadScene());

    }

    string fixJson(string value)
    {
        value = "{\"result\":" + value + "}";
        return value;
    }

    public void backButton(){
        serverSelectCanvas.FadeIn();
        characterSelectCanvas.FadeOut();
    }


    IEnumerator LoadScene ()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("testchamber");

        while ( !op.isDone )
        {
            float progress = Mathf.Clamp01(op.progress / .9f);
            Debug.Log(op.progress);

            yield return null;
        }
    }
    
}
