using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using UnityEngine.UI;

public class PlayersManager : MonoBehaviour
{

    static System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.GetCultureInfo("en-US");

    public GameObject playerPrefab;
    static GameObject Track;
    static GameObject PlayerPrefab;
    static GameObject playerFinder;
    public GameObject spawn;
    static Animator anim;
    static Text UsernameText;
    private string currentAnim;

  
    static thirdPersonController.PlayerInfo playerData = new thirdPersonController.PlayerInfo();




    private void Start() {
        PlayerPrefab = playerPrefab;
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
        playerFinder.transform.position = new Vector3 (playerData.positionX,playerFinder.transform.position.y,playerData.positionZ);
        playerFinder.transform.rotation = Quaternion.Euler(0,playerData.rotation,0);
        anim = GameObject.Find("Player" + playerData.ID + "/Model").GetComponent<Animator>();
        anim.SetFloat("Horizontal",playerData.animHorizontal);
        anim.SetFloat("Vertical",playerData.animVertical);
        anim.SetFloat("Magnitude",playerData.animMagnitude);
        anim.SetBool("Running",playerData.Running);
        
        
        
    /*
        UsernameText = GameObject.Find("Player" + playerData.ID + "/Canvas/Username").GetComponent<Text>();
        UsernameText.text = playerData.Username;
        if(playerData.isDashing == true){
            playerFinder.GetComponent<dash>().doDash();
        }
    */
        
    } 
}
