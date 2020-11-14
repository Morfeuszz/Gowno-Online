using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class thirdPersonController : MonoBehaviour
{

    public class PlayerInfo {
    public string action = "playerInfo";
    public string Username;
    public float positionX;
    public float positionZ;
    public int gridX;
    public int gridZ;
    public float rotation;
    public string animationName;
    public float animVertical;
    public float animHorizontal;
    public float animMagnitude;
    public bool Running;
    public int ID;
    }

    public static PlayerInfo json = new PlayerInfo();
    private  PlayerInfo jsonOld = new PlayerInfo();

    public mapGrid gridScript;
    public Connection connection;
    public float speed = 1.5f;
    private float actualSpeed;
    Vector3 movement;
    public Transform camera;
    public float cameraDamp = 2;
    Quaternion target;
    private Animator anim;
    AnimatorClipInfo[] CurrentClipInfo;
    public bool doDash;
    public float dashTime;
    private float dashTimer;
    public float dashPower;

 


    void Start(){
        anim =  GameObject.Find("/Player/Model").GetComponent<Animator>();
        actualSpeed = speed;
    }

    void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"),0.0f,Input.GetAxis("Vertical"));
        anim.SetFloat("Magnitude",movement.magnitude);
        json.animMagnitude = movement.magnitude;
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){
            target = Quaternion.Euler (transform.localEulerAngles.x, camera.localEulerAngles.y, transform.localEulerAngles.z);
            anim.SetFloat("Horizontal",movement.x);
            anim.SetFloat("Vertical",movement.z);
            json.animHorizontal = movement.x;
            json.animVertical = movement.z;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            anim.SetBool("Running",true);
            actualSpeed = speed * 2f;
            json.Running = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)){
            anim.SetBool("Running",false);
            actualSpeed = speed;
            json.Running = false;
        }
        CurrentClipInfo = this.anim.GetCurrentAnimatorClipInfo(0);
        json.animationName = CurrentClipInfo[0].clip.name;
        if(Input.GetKeyDown(KeyCode.Space)){
            doDash = true;
            dashTimer = dashTime;
        }
        if(dashTimer - Time.deltaTime <= 0 && doDash == true){
            doDash = false;
            dashTimer = 0;
        } else if (doDash == true){
            dashTimer -= Time.deltaTime;
        }
        json.gridX = gridScript.grid.x;
        json.gridZ = gridScript.grid.z;

    }


    void FixedUpdate () {
        Vector3 tempVect = new Vector3(movement.x, 0, movement.z);
        tempVect = Quaternion.Euler(0, transform.localEulerAngles.y, 0) * tempVect; 
        tempVect = tempVect.normalized * actualSpeed * Time.deltaTime;
        transform.position += tempVect;
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * cameraDamp);
        json.positionX = transform.position.x;
        json.positionZ = transform.position.z;
        json.rotation = transform.localEulerAngles.y;
        if(json.positionX != jsonOld.positionX || json.positionZ != jsonOld.positionZ || json.animationName != jsonOld.animationName){
            string temp = JsonUtility.ToJson(json);
            connection.SendWebSocketMessage(temp);
            jsonOld = JsonUtility.FromJson<PlayerInfo>(temp);                          

        }
        if(doDash == true){
            transform.position += tempVect * dashPower;
        }
    }

    
}
