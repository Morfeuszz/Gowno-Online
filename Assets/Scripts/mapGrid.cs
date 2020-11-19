using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGrid : MonoBehaviour
{
    public GameObject Player;
    public thirdPersonController playerController;
    public Connection connection;
    public DataHolder DataController;
    //public int mapSize = 1000;
    public int gridSize = 50;
    
    public class MapGrid {
    public string action = "mapGrid";
    public bool add = true;
    public Vector2 cords;
    public Vector2 cordsOld;
    public int ID;
    }

    public MapGrid grid = new MapGrid();

    void Start(){
        Invoke("First", 1);
    }


    void First(){
        DataController = GameObject.Find ("DATA").GetComponent<DataHolder>();
        grid.ID = DataController.data.ID;
        grid.add = true;
        grid.cords = new Vector2((int)Player.transform.position.x / gridSize,(int)Player.transform.position.z / gridSize);
        string temp = JsonUtility.ToJson(grid);
        connection.SendWebSocketMessage(temp);
        grid.cordsOld = grid.cords;
        thirdPersonController.json.grid = grid.cords;
        grid.add = false;
    }

    void Update()
    { 
        if(grid.add == true){
            return;
        }
        grid.cords = new Vector2((int)Player.transform.position.x / gridSize,(int)Player.transform.position.z / gridSize);
        if(grid.cords != grid.cordsOld){
            string temp = JsonUtility.ToJson(grid);
            connection.SendWebSocketMessage(temp);
            grid.cordsOld = grid.cords;;
        }

    }
}
