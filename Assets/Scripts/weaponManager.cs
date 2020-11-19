using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponManager : MonoBehaviour
{
    public GameObject Holster;
    public GameObject Weapon;
    public Animator anim;
    public bool Sheathed = true;
    public  bool attack;
    public string Status;


    public void Sheath(bool isSheathed)
    {
        if(isSheathed != Sheathed) return;
        StartCoroutine(waiter());
    }

    IEnumerator waiter(){
        if(Sheathed == false){
            Sheathed = true;
            anim.Play("Sheath", 1, 0f);
            yield return new WaitForSecondsRealtime(1.1f);
        }else if(Sheathed == true && attack == false){
            Sheathed = false;
            anim.Play("Unsheath", 1, 0f);
            yield return new WaitForSecondsRealtime(0.1f);
        }else if(Sheathed == true && attack == true){
            Sheathed = false;
            anim.Play("UnsheathAttack", 2, 0.4f);
        } 
       
        Holster.SetActive(!Holster.active);
        Weapon.SetActive(!Weapon.active);
        

    }
}
