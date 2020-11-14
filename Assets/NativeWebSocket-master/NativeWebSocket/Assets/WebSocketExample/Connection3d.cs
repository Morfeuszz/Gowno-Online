using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using NativeWebSocket;
using System.Globalization;

public class Connection3d : MonoBehaviour
{
    System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
    WebSocket websocket;

    public DataHolder DataController;
    public PlayersManager playersManager;
    public ping pingScript;


    public class Action
    {
        public string action;
        public string Username;
        public int ID;
    }
    public Action actionName = new Action();


    async void Start()
    {
        websocket = new WebSocket("ws://127.0.0.1:6789");
  
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
         DataController = GameObject.Find ("DATA").GetComponent<DataHolder>();
         

         PlayerControler.json.ID = DataController.data.ID;
        PlayerControler.json.Username = DataController.data.Username;
        websocket.OnMessage += (bytes) =>
        {
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            actionName = JsonUtility.FromJson<Action>(message);
            print(message);

            switch (actionName.action)
            {
                case "newPlayer":
                    playersManager.newPlayer(actionName.ID);
                    break;
                case "disconnect":
                    playersManager.disconnect(actionName.ID);
                    break;
                case "playerInfo":
                    playersManager.playerInfo(message);
                    break;
                case "requestID":
                    actionName.ID = DataController.data.ID;
                    actionName.action = "giveID";
                    SendWebSocketMessage(JsonUtility.ToJson(actionName));
                    break;
                case "chatMessage":
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
        if (websocket.State == WebSocketState.Open)
        {
            
            await websocket.SendText(json);
            pingScript.Ping();
        }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }
}
