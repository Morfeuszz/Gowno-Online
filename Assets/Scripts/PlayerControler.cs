using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class PlayerControler : MonoBehaviour
{
    public Connection connection;
    public class PlayerInfo {
        public string action = "playerInfo";
        public string Username;
        public float positionX;
        public float positionY;
        public float orientX;
        public float orientY;
        public float orientMagni; 
        public bool isDashing = false;
        public int ID;
    }
    private Animator anim;
     public static PlayerInfo json = new PlayerInfo();
    float oldX;
    float oldY;
    float oldMagni;
    public Text usernameText;
    public bool canMove = true;
    public dash dashScript;
    
    Vector3 movement;
    public float speed = 1.5f;


     void Start () {
         oldX = transform.position.x;
         oldY = transform.position.y;
         anim = GetComponent<Animator>();
         
     }

 
     void Update() {
         if (usernameText.text != json.Username){
            usernameText.text = json.Username;
         }
         if(canMove){
            movement = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0.0f);
            if(Input.GetKeyDown(KeyCode.Space)){
                dashScript.doDash();
            }
         } else {
             movement = new Vector3(0f,0f,0f);
         }
        if (movement != Vector3.zero)
           {
                anim.SetFloat("Horizontal",movement.x);
                anim.SetFloat("Vertical",movement.y);
                json.orientX = movement.x;
                json.orientY = movement.y;
            }

        anim.SetFloat("Magnitude",movement.magnitude);
        json.orientMagni = movement.magnitude;
    }
     void FixedUpdate () {

        Vector3 tempVect = new Vector3(movement.x, movement.y, 0);
        tempVect = tempVect.normalized * speed * Time.deltaTime;
        transform.position += tempVect;
        if(transform.position.x != oldX | transform.position.y != oldY | movement.magnitude != oldMagni){
            json.positionX = transform.position.x;
            json.positionY = transform.position.y;
            connection.SendWebSocketMessage(JsonUtility.ToJson(json));
        }
            oldX = transform.position.x;
            oldY = transform.position.y;
            oldMagni = movement.magnitude;
      
        
     }    
 }
 
