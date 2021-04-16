using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class thirdPersonController : MonoBehaviour
{

    public class PositionInfo {
    public string action = "position";
    public Vector3 position;
    public Vector3 velocity;
    public Vector2 grid;
    public float rotation;
    public int ID;
    }

    public static PositionInfo json = new PositionInfo();
    private  PositionInfo jsonOld = new PositionInfo();
    public Connection connection;
    public float speed = 2f;
    public float normalSpeed = 2f;
    public float sprintSpeed = 5f;

    public Vector3 movement;
    public Transform camera;
    public float cameraDamp = 2;
    Quaternion target;
    public DataHolder DataController;
    public bool canMove = true;
    private Rigidbody rb;

 


    void Start(){
        DataController = GameObject.Find ("DATA").GetComponent<DataHolder>();
        rb = gameObject.GetComponent<Rigidbody>();
        json.ID = DataController.data.ID;
    }

    void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"),0.0f,Input.GetAxis("Vertical"));
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){
            target = Quaternion.Euler (transform.localEulerAngles.x, camera.localEulerAngles.y, transform.localEulerAngles.z);
        }
    }


    void FixedUpdate () {
        if(Input.GetKey(KeyCode.LeftShift)){
            if(speed < sprintSpeed){
                speed += Time.deltaTime * 5;
            } else {
                speed = sprintSpeed;
            }
        } else {
            if(speed > normalSpeed){
                speed -= Time.deltaTime * 5;
            } else {
                speed = normalSpeed;
            } 
        }
        Vector3 tempVect = new Vector3(movement.x, 0, movement.z);
        tempVect = Quaternion.Euler(0, transform.localEulerAngles.y, 0) * tempVect; 
        tempVect = tempVect.normalized * speed;
        rb.velocity = new Vector3(tempVect.x,rb.velocity.y,tempVect.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * cameraDamp);
        json.velocity = rb.velocity;
        json.position = transform.position;
        json.rotation = transform.localEulerAngles.y;
        if(json.position != jsonOld.position){
            string temp = JsonUtility.ToJson(json);
            connection.SendWebSocketMessage(temp);
            jsonOld = JsonUtility.FromJson<PositionInfo>(temp);                          

        }
    }

    
}
