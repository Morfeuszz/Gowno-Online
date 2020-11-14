using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGrid : MonoBehaviour
{
    public GameObject Player;
    public Connection connection;
    public DataHolder DataController;
    //public int mapSize = 1000;
    public int gridSize = 50;
    
    public class MapGrid {
    public string action = "mapGrid";
    public bool add = true;
    public int x;
    public int z;
    public int xOld;
    public int zOld;
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
        grid.x = (int)Player.transform.position.x / gridSize;
        grid.z = (int)Player.transform.position.z / gridSize;
        string temp = JsonUtility.ToJson(grid);
        connection.SendWebSocketMessage(temp);
        grid.xOld = grid.x;
        grid.zOld = grid.z;
        grid.add = false;
    }

    void Update()
    { 
        if(grid.add == true){
            return;
        }
        grid.x = (int)Player.transform.position.x / gridSize;
        grid.z = (int)Player.transform.position.z / gridSize;
        if(grid.x != grid.xOld || grid.z != grid.zOld){
            string temp = JsonUtility.ToJson(grid);
            connection.SendWebSocketMessage(temp);
            grid.xOld = grid.x;
            grid.zOld = grid.z;
        }

    }
}
