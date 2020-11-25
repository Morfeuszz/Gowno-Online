using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    [System.Serializable]
    public class UserData {
        public int ID;
        public string Username;
        public string characterName;
        public int characterID;
        public int Level;
        public int EXP;
        public Vector3 position;
        public string serverAdress;
    }

    public class MenuData {
        public int activeScreen = 0;
    }

    public MenuData menu = new MenuData();


    void Start(){
        DontDestroyOnLoad(this.gameObject);
    }

    public UserData data = new UserData();
    
    public  void Login(string message)
    {
        print(message);
        data = JsonUtility.FromJson<UserData>(message);
    }


}
