using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoHolder : MonoBehaviour
{
    public Animator anim;
    public Rigidbody rb;

    public string currentAnim;
    private string lastAnim;


    
    public void UpdateInfo(thirdPersonController.PlayerInfo playerData){
        rb.velocity = playerData.velocity;
        transform.position = playerData.position;
        transform.rotation = Quaternion.Euler(0,playerData.rotation,0);
        anim.SetFloat("Horizontal",playerData.animHorizontal);
        anim.SetFloat("Vertical",playerData.animVertical);
        anim.SetFloat("Magnitude",playerData.animMagnitude);
        anim.SetBool("Running",playerData.Running);
        anim.SetBool("Jumping",playerData.Jumping);
    }
}
