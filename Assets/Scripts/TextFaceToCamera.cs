using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFaceToCamera : MonoBehaviour
{
public Transform nameText;


void FixedUpdate()
{
this.nameText.forward = Camera.main.transform.forward;
}
}
