using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dash : MonoBehaviour
{
    private Rigidbody rb;
    public float dashSpeed = 5;
    private float orginalDashSpeed;
    private float dashTimer;
    public float dashLength = 1;
    public float dashCooldown;
    private float dashCooldownTimer;
    public float ghostDelay;
    private float ghostDelaySeconds;
    public float destroyTime;
    Vector3 movement;
    public GameObject ghost;
    private SpriteRenderer playerSprite;
    public bool isPlayer = true;
    private bool dodasha = false;
    void Start()
    {
    rb = GetComponent<Rigidbody>();
    //playerSprite = GetComponent<SpriteRenderer>();
    ghostDelaySeconds = ghostDelay;   
    orginalDashSpeed = dashSpeed; 
    }

    
    void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical"));


        if(dashTimer > 0){
            if(dashTimer - Time.deltaTime <= 0){
                rb.velocity = new Vector3(0f,0f,0f);
                dashTimer = 0;
                PlayerControler.json.isDashing = false;
                dodasha = false;
            } else {
                dashTimer -= Time.deltaTime; 
            }
            if(ghostDelaySeconds > 0){
                ghostDelaySeconds -= Time.deltaTime;
            } else {
                GameObject currentGhost = Instantiate(ghost, transform.position,transform.rotation);
                //currentGhost.GetComponent<SpriteRenderer>().sprite = playerSprite.sprite;
                Destroy(currentGhost,destroyTime);
                ghostDelaySeconds = ghostDelay;
            }
        }
        if(dashCooldownTimer > 0){
            dashCooldownTimer -= Time.deltaTime;
        }
    }
    public void doDash(){
        dodasha = true;
    }
    void FixedUpdate() {
        if(dashCooldownTimer <= 0 && dodasha == true || isPlayer == false && dodasha == true){
            transform.position += movement * dashSpeed * Time.deltaTime;
            dashTimer = dashLength;
            dashCooldownTimer = dashCooldown;
            if(isPlayer == true){
                PlayerControler.json.isDashing = true;
            }
        }   
    }


}
