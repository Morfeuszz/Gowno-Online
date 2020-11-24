using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public class UserData {
        public int ID;
        public string Username;
        public int Level;
        public int EXP;
        public float posX;
        public float posY;
        public string serverAdress;
    }


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
