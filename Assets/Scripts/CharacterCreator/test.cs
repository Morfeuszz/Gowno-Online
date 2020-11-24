using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class test : MonoBehaviour
{
  public float x = 1.0f;
  public float y = 1.0f;
  public float z = 1.0f;
    private Transform[] children = null;


 void Start () 
 {
     children = new Transform[ transform.childCount ];
     int i = 0;  
     foreach( Transform T in transform )
         children[i++] = T;
 }
public void dupa(){
    x+=0.1f;     
}
 void Update () 
 {
     if( x != transform.localScale.x || y != transform.localScale.y || z != transform.localScale.z )
     {
         transform.DetachChildren();                     // Detach
         transform.localScale = new Vector3( x, y, z );  // Scale        
         foreach( Transform T in children )              // Re-Attach
             T.parent = transform;
     }
 }
}
