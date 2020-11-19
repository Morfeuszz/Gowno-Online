using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class thirdPersonController : MonoBehaviour
{

    public class PlayerInfo {
    public string action = "playerInfo";
    public string Username;
    public Vector3 position;
    public Vector3 velocity;
    public Vector2 grid;
    public float rotation;
    public string animationName;

    //public int actionID



    public int attackID = 0;

    public float animVertical;
    public float animHorizontal;
    public float animMagnitude;
    public bool Running;
    public bool Jumping;
    public bool Armed;
    public bool attack;


    public int ID;
    }

    public static PlayerInfo json = new PlayerInfo();
    private  PlayerInfo jsonOld = new PlayerInfo();
    private bool isArmed;
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
    public TMP_Text UsernameText;
    public DataHolder DataController;
    public weaponManager weapons;
    public bool canMove = true;
    private Rigidbody rb;

 


    void Start(){
        DataController = GameObject.Find ("DATA").GetComponent<DataHolder>();
        anim =  GameObject.Find("/Player/Model").GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        json.ID = DataController.data.ID;
        json.Username = DataController.data.Username;
        UsernameText.text = json.Username;
        actualSpeed = speed;
    }

    void Update()
    {
        if(!canMove) {
            movement = new Vector3(0f,0f,0f);
            json.animHorizontal = 0;
            json.animVertical = 0;
            json.animMagnitude = 0;
            anim.SetFloat("Horizontal",0);
            anim.SetFloat("Vertical",0);
            anim.SetFloat("Magnitude",0);
            return;

        }
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
        if(Input.GetKeyDown(KeyCode.Space)){
            anim.SetBool("Jumping",true);
            json.Jumping = true;
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            anim.SetBool("Jumping",false);
            json.Jumping = false;
        }
        if (Input.GetMouseButtonDown(0)){
            if(json.Armed == false){
                json.Armed = true;
                weapons.attack = true;
                json.attack = true;
                weapons.Sheath(json.Armed);
                weapons.attack = false;
            } else {
                anim.Play("Slash", 2, 0f);
                json.attackID++;
            }
        }
        if(Input.GetKeyUp(KeyCode.Tab)){
            json.Armed = !json.Armed;
            weapons.Sheath(json.Armed);
        }


        CurrentClipInfo = this.anim.GetCurrentAnimatorClipInfo(0);
        json.animationName = CurrentClipInfo[0].clip.name;

    }


    void FixedUpdate () {
        Vector3 tempVect = new Vector3(movement.x, 0, movement.z);
        tempVect = Quaternion.Euler(0, transform.localEulerAngles.y, 0) * tempVect; 
        tempVect = tempVect.normalized * actualSpeed;
        rb.velocity = new Vector3(tempVect.x,rb.velocity.y,tempVect.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * cameraDamp);
        json.velocity = rb.velocity;
        json.position = transform.position;
        json.rotation = transform.localEulerAngles.y;
        if(json.position != jsonOld.position || json.animationName != jsonOld.animationName || json.Armed != jsonOld.Armed || json.attackID != jsonOld.attackID){
            string temp = JsonUtility.ToJson(json);
            connection.SendWebSocketMessage(temp);
            json.attack = false;
            jsonOld = JsonUtility.FromJson<PlayerInfo>(temp);                          

        }
        if(doDash == true){
            transform.position += tempVect * dashPower;
        }
    }

    
}
