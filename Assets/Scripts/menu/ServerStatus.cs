using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ServerStatus : MonoBehaviour
{   
  [System.Serializable]
    public class ServerInfo{
        public string name;
        public string ip;
        public int port;
    }

    [System.Serializable]
    public class ServerListObject{
        public List<ServerInfo> serversList = new List<ServerInfo>();
    }
    [System.Serializable]
    public class ServerWidget{
        public Text name;
        public Text status;
        public Text statusIcon;
        public Button button;
    }
    List<ServerWidget> widgetList = new List<ServerWidget>();
    public ServerListObject serverDB;
    WebSocket websocket;
    public List<WebSocket> websocketList = new List<WebSocket>();
    public GameObject widgetObject, statusPanel; 
    private DataHolder DataController;
    public GameObject menuScreen, logo, characterSelect;
    private CanvasFadeOut  serverScreenCanvas, menuScreenCanvas, logoCanvas, characterSelectCanvas;

    string url = "http://35.158.140.147/serversold.json";
    void Start()
    {   
        serverScreenCanvas = GetComponent<CanvasFadeOut>();
        logoCanvas = logo.GetComponent<CanvasFadeOut>();
        menuScreenCanvas = menuScreen.GetComponent<CanvasFadeOut>();
        characterSelectCanvas = characterSelect.GetComponent<CanvasFadeOut>();
        DataController = GameObject.Find ("DATA").GetComponent<DataHolder>();
        WWW www = new WWW(url);
        StartCoroutine(GetServerStatus(www));
    }


    IEnumerator GetServerStatus(WWW www)
    {
        yield return www;
        serverDB = JsonUtility.FromJson<ServerListObject>(www.text);
        foreach(ServerInfo server in serverDB.serversList){
            string serverAdress = "ws://" + server.ip + ":" + server.port;
            CheckStatus(serverAdress,server);
        }
    }
    
    async void CheckStatus(string serverAdress, ServerInfo server){
        ServerWidget newWidget = new ServerWidget();
        GameObject widget = Instantiate(widgetObject, statusPanel.transform);
        widget.name = server.name +"Status";
        newWidget.name = GameObject.Find(server.name +"Status/Server_Name").GetComponent<Text>();
        newWidget.status = GameObject.Find(server.name +"Status/Status").GetComponent<Text>();
        newWidget.statusIcon = GameObject.Find(server.name +"Status/Status_icon").GetComponent<Text>();
        newWidget.button = GameObject.Find(server.name +"Status/Button").GetComponent<Button>();
        newWidget.name.text = server.name.ToLower();

        websocket = new WebSocket(serverAdress);
        websocketList.Add(websocket); 

        websocket.OnOpen += () => {
            newWidget.status.text = "online";
            newWidget.button.interactable = true;
            newWidget.button.onClick.AddListener(delegate {Connect(serverAdress); });
            newWidget.status.color = new Color(0.18f,0.58f,0.18f,1f);
            newWidget.statusIcon.color = new Color(0.18f,0.58f,0.18f,1f);
            widgetList.Add(newWidget);
            websocket.Close();
            
        };
        websocket.OnError += (e) => {
            newWidget.status.text = "offline";
            newWidget.status.color = new Color(0.76f,0.28f,0.06f,1f);
            newWidget.statusIcon.color = new Color(0.76f,0.28f,0.06f,1f);
            widgetList.Add(newWidget);
            websocket.Close();
        };
        await websocket.Connect();
    }
    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }

    public void Connect(string serverAdress){
        DataController.data.serverAdress = serverAdress;
        serverScreenCanvas.FadeOut();
        characterSelectCanvas.FadeIn();
    }

    public void backButton(){
        menuScreenCanvas.FadeIn();
        logoCanvas.FadeIn();
        serverScreenCanvas.FadeOut();
    }

}
