using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;
using UnityEngine.SceneManagement;

public class authClient : MonoBehaviour
{
    public CanvasFadeOut loadingCanvas,errorCanvas,loginCanvas, characterSelectCanvas;
    WebSocket websocket;
    public DataHolder DataController;
    public GameObject dataPrefab,empty;
    public login loginScript;
    public register registerScript;
    public characterSelect characterSelectScript;
    private bool masterConnnection = false;

    public class Action{
        public string action;
        public int ID;
        public string familyName;
        public string authToken;
        public string status;
    }
    public Action actionName = new Action();

    [System.Serializable]
    public class Server{
        public string address;
    }
    Server serverAddress = new Server();
    string url = "http://35.158.140.147/server.json";
    void Awake()
    {   
        if (GameObject.FindGameObjectsWithTag("Connection").Length >= 2 && masterConnnection == false){
            Destroy(gameObject);
            
        }

        if (GameObject.Find("DATA") == null){
            GameObject Data = Instantiate(dataPrefab);
            Data.name = "DATA";
        }
        DataController = GameObject.Find ("DATA").GetComponent<DataHolder>();
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        masterConnnection = true;
        if(scene.name != "menuNew" && scene.name != "CharacterCreator" && scene.name != "LoadingScreen"){
            Destroy(gameObject);
        }

    }
    public void init(){
        loadingCanvas.FadeIn();
        WWW www = new WWW(url);
        StartCoroutine(GetServerAddress(www));
    }

    IEnumerator GetServerAddress(WWW www)
    {
        yield return www;
        serverAddress = JsonUtility.FromJson<Server>(www.text);
        Connect();
    }

    async void Connect(){
        websocket = new WebSocket(serverAddress.address);
        print(websocket);
        websocket.OnOpen += () => {
            loadingCanvas.FadeOut();
            if(DataController.menu.characterCreator == false){
                loginCanvas.FadeIn();
            } else {
                characterSelectScript.GetCharacters();
                characterSelectCanvas.FadeIn();
                DataController.menu.characterCreator = false;
            }

        };
        websocket.OnError += (e) =>
        {
            print(e);
            loadingCanvas.FadeOut();
            errorCanvas.FadeIn();
        };
        

        websocket.OnMessage += (bytes) =>
        {
            var message = System.Text.Encoding.UTF8.GetString(bytes); 
            actionName = JsonUtility.FromJson<Action>(message);
            switch (actionName.action)
            {
                case "login":
                    loginScript.loginRespons(actionName);
                    break;
                case "register":
                    registerScript.registerRespons(actionName);
                    break;
                case "getCharacters":
                    characterSelectScript.loadCharacters(message);
                    break;
                default: 
                    break;
            }

        };
        await websocket.Connect();
    }
        
    public void retry(){
        loadingCanvas.FadeIn();
        errorCanvas.FadeOut();
        Connect();
    }

    void Update()
    {
        if (websocket != null)
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            websocket.DispatchMessageQueue();
#endif
        }

    }
        
    async public void SendWebSocketMessage(string json)
    {
        if (websocket.State == WebSocketState.Open)
        {

            await websocket.SendText(json);
        }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }
}
