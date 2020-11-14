using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public GameObject attackPrefab;
    public Transform orb;
    public float radius;

    
    private Transform pivot;

    void Start()
    {
        pivot = orb.transform;
        transform.parent = pivot;
        transform.position += Vector3.up * radius;
    }


    void Update()
    {

        Vector3 orbVector = Camera.main.WorldToScreenPoint(orb.position);
        orbVector = Input.mousePosition - orbVector;
        float angle = Mathf.Atan2(orbVector.y, orbVector.x) * Mathf.Rad2Deg;
 
        pivot.position = orb.position;
        pivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Input.GetMouseButtonDown(0)){
            GameObject currentAttack = Instantiate(attackPrefab, transform.position,transform.rotation);
            Destroy(currentAttack,0.33f);
        }




    }


}
