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
    private Rigidbody rb;

    [System.Serializable]
    public class positionData {
        public Vector3 position;
        public Vector3 velocity;
        public float rotation;
        public int ID;
    }
    [System.Serializable]
    public class positionsString {
        public string[] positions;

    }
    public positionsString positionsStringList;
    public positionData positionsReady;

    public DataHolder DataController;

    public List<int> spawnedIDs = new List<int>();
    




    private void Start() {
        PlayerPrefab = playerPrefab;
        //Instantiate(localPlayerPrefab, spawn.transform.position, Quaternion.identity);
        DataController = GameObject.Find ("DATA").GetComponent<DataHolder>();
    }
    public void spawnPlayer(int ID){
        Track = Instantiate(PlayerPrefab, spawn.transform.position, Quaternion.identity);
        Track.name = "Player" + ID;
        spawnedIDs.Add(ID);
    }

    public void disconnect(int ID){
        playerFinder = GameObject.Find("Player" + ID);
        Destroy(playerFinder);
    }

    /*public void playerInfo(string message){
        playerData = JsonUtility.FromJson<thirdPersonController.PositionInfo>(message);
        playerFinder = GameObject.Find("Player" + playerData.ID);
        anim = GameObject.Find("Player" + playerData.ID+"/Model").GetComponent<Animator>();
        PlayerInfoHolder PIH = playerFinder.GetComponent<PlayerInfoHolder>();
        PIH.UpdateInfo(playerData);

    }
    */
    public void updatePositions(string message){
        positionsStringList = JsonUtility.FromJson<positionsString>(message);
        foreach (string pos in positionsStringList.positions)
        {
            positionsReady = JsonUtility.FromJson<positionData>(pos);
            if(positionsReady.ID != DataController.data.ID){
                if(!spawnedIDs.Contains(positionsReady.ID)){
                    spawnPlayer(positionsReady.ID);
                }
                playerFinder = GameObject.Find("Player" + positionsReady.ID);
                playerFinder.transform.position = positionsReady.position;
                playerFinder.transform.rotation = Quaternion.Euler(0,positionsReady.rotation,0);
                rb = playerFinder.GetComponent<Rigidbody>();
                rb.velocity = positionsReady.velocity;
            }
        }



    } 
}
