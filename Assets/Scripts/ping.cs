using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ping : MonoBehaviour
{
    public float timer;
    public bool isPinging;
    public string pingNow;
    public Connection connection;

    void Start(){
        InvokeRepeating("Ping", 1f, 5f); 
    }
    void FixedUpdate()
    {
        if(isPinging == true){
            timer += Time.deltaTime;
        }
    }

    public void Ping(){
        
        if(isPinging == false || timer > 10f){
            timer = 0f;
            isPinging = true;
            connection.SendWebSocketMessage("{\"action\": \"ping\"}");
        }
    }

    public void Pong(){
        Debug.Log(timer);
        isPinging = false;
        pingNow = "ping: " + timer*1000;
    }



        void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), pingNow);
    }
    
}
