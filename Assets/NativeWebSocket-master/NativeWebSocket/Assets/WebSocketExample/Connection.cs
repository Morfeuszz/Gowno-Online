using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using NativeWebSocket;
using System.Globalization;

public class Connection : MonoBehaviour
{
    System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
    WebSocket websocket;
    
    public PlayersManager playersManager;
    public Chat chat;
    public ping pingScript;
    public DataHolder DataController;
    private bool connected = false;

    public bool DebugModeNoConnection = false;

    public class Action
    {
        public string action;
        public string Username;
        public int ID;
    }
    public Action actionName = new Action();


    async void Start()
    {
        if(DebugModeNoConnection) this.enabled = false;
        DataController = GameObject.Find ("DATA").GetComponent<DataHolder>();
        websocket = new WebSocket(DataController.data.serverAdress);
        
  
        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>
        {
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            print(message);
            actionName = JsonUtility.FromJson<Action>(message);


            switch (actionName.action)
            {
                case "updatePositions":
                    playersManager.updatePositions(message);
                    break;
                case "requestToken":
                    SendAuthToken("{\"authToken\" : \"" + DataController.data.authToken + "\", \"ID\" : \"" + DataController.data.ID + "\"}");
                    break;
                case "tokenSuccess":
                    connected = true;
                    break;
                case "chatMessage":
                    chat.reciveChatMessage(message);
                    break;
                case "pong":
                    pingScript.Pong();
                    break;
                default: 
                    break;
            }

        };

        await websocket.Connect();

    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        websocket.DispatchMessageQueue();
#endif
    }

    async public void SendWebSocketMessage(string json)
    {
        if (websocket.State == WebSocketState.Open && connected == true)
        {
            await websocket.SendText(json);
        }
    }

    async public void SendAuthToken(string token)
    {
        await websocket.SendText(token);
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }
}
