using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoHolder : MonoBehaviour
{
    public Animator anim;
    public Rigidbody rb;

    public string currentAnim;
    private string lastAnim;


    
    public void UpdateInfo(thirdPersonController.PositionInfo playerData){
        rb.velocity = playerData.velocity;
        transform.position = playerData.position;
        transform.rotation = Quaternion.Euler(0,playerData.rotation,0);
    }
}
