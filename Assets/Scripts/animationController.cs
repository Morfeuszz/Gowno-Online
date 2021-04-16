using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour
{
    public Animator anim;
    public Rigidbody rb;
    private Vector3 velocity;
    public GameObject player;



    //TEMP values should be taken from invidual player info

    public float nrmlspeed = 2f;
    


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity;
        Vector3 localVelocity = player.transform.InverseTransformDirection(velocity);
        Vector3 localVelocityNormalized = localVelocity.normalized;
        anim.SetFloat("Magnitude", velocity.magnitude);
        anim.SetFloat("Horizontal",localVelocityNormalized.x *(Mathf.Abs(localVelocity.x) / nrmlspeed));
        anim.SetFloat("Vertical",localVelocityNormalized.z *(Mathf.Abs(localVelocity.z) / nrmlspeed));
    
    }
    
    
}
