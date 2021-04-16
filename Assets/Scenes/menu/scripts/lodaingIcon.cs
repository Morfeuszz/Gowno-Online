using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lodaingIcon : MonoBehaviour
{
    public bool active = false;
    public int degrees = 5;

    void Update()
    {
        if(active){
            transform.eulerAngles += Vector3.forward * degrees;
        }

    }
}
