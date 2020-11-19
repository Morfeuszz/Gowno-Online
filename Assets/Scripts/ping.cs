using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ping : MonoBehaviour
{
    public float timer;
    public bool isPinging;
    public string pingNow;
    public Connection connection;
    public Text pingText;

    void Start(){
        InvokeRepeating("Ping", 1f, 5f); 
    }
    void FixedUpdate()
    {
        if(isPinging == true){
            timer += Time.fixedDeltaTime;
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
        pingText.text = pingNow;
    }

    
}
