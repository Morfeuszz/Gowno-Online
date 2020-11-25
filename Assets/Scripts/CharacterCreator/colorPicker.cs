using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorPicker : MonoBehaviour
{
    public FlexibleColorPicker fcb;
    public Material material;
    void Start()
    {
        
    }

    void Update()
    {
        if(material.color != fcb.color){
            material.color = fcb.color;
        }
    }
}
