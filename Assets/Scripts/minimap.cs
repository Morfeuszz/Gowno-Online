using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimap : MonoBehaviour
{
    public Transform player;
    public Transform camera;

    void Start()
    {
        
    }


    void Update()
    {
        transform.position = new Vector3(player.transform.position.x,300f,player.transform.position.z);
        transform.eulerAngles = new Vector3(90, camera.transform.eulerAngles.y, 0);
    }
}
