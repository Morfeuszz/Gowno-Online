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
    }


void Start(){
DontDestroyOnLoad(this.gameObject);
}

    public UserData data = new UserData();
    // Update is called once per frame
    public  void Login(string message)
    {
        data = JsonUtility.FromJson<UserData>(message);
    }
}
