using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using UnityEngine.UI;
using TMPro;


public class PlayersManager : MonoBehaviour
{

    static System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.GetCultureInfo("en-US");

    public GameObject playerPrefab;
    public GameObject localPlayerPrefab;
    static GameObject Track;
    static GameObject PlayerPrefab;
    static GameObject playerFinder;
    public GameObject spawn;
    static Animator anim;
    static TMP_Text UsernameText;
    private weaponManager weapons;
    private string currentAnim;
    private int oldAttackID = 0;

  
    
    static thirdPersonController.PlayerInfo playerData = new thirdPersonController.PlayerInfo();



    private void Start() {
        PlayerPrefab = playerPrefab;
        //Instantiate(localPlayerPrefab, spawn.transform.position, Quaternion.identity);
    }
    public void newPlayer(int ID){
        Track = Instantiate(PlayerPrefab, spawn.transform.position, Quaternion.identity);
        Track.name = "Player" + ID;

    }

    public void disconnect(int ID){
        playerFinder = GameObject.Find("Player" + ID);
        Destroy(playerFinder);
    }

    public void playerInfo(string message){
        playerData = JsonUtility.FromJson<thirdPersonController.PlayerInfo>(message);
        playerFinder = GameObject.Find("Player" + playerData.ID);
        anim = GameObject.Find("Player" + playerData.ID+"/Model").GetComponent<Animator>();
        PlayerInfoHolder PIH = playerFinder.GetComponent<PlayerInfoHolder>();
        PIH.UpdateInfo(playerData);


        UsernameText = GameObject.Find("Player" + playerData.ID + "/Name").GetComponent<TMP_Text>();
        UsernameText.text = playerData.Username;
        weapons = playerFinder.GetComponent<weaponManager>();
        weapons.attack = playerData.attack;
        weapons.Sheath(playerData.Armed);
        if(playerData.attackID != oldAttackID){
            anim.Play("Slash", 2, 0f);
            oldAttackID = playerData.attackID;
        }

        
        

        /*
        if(playerData.isDashing == true){
            playerFinder.GetComponent<dash>().doDash();
        }
    */
        
    } 
}
