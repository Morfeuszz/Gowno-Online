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
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener((data) => { OnDragDelegate((PointerEventData)data); });
        trigger.triggers.Add(entry);
 }
     public void OnDragDelegate(PointerEventData data)
    {
        Debug.Log("sds");
        x+= data.delta.x * Time.deltaTime;
        y+= data.delta.y * Time.deltaTime;

    }


    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
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
