using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class configController : MonoBehaviour
{
    [System.Serializable]
    public class Config {
         public Keybinds keybinds;

    }
    [System.Serializable]
    public class Keybinds {
        public UIKeyBinds UI;
    }
    [System.Serializable]
    public class UIKeyBinds {
        public KeyCode inventory = KeyCode.I;
        public KeyCode menu = KeyCode.Escape;
    }

    public Config config;
    

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (System.IO.File.Exists("config.json"))
        {   
            string loadedConfig = System.IO.File.ReadAllText("./config.json");
            config = JsonUtility.FromJson<Config>(loadedConfig);
            print("config loaded");
        } else {
            System.IO.File.WriteAllText("./config.json", "test");
        }
        
    }

    void UpdateConfig()
    {
        
       System.IO.File.WriteAllText("./config.json",JsonUtility.ToJson(config)); 
    }

}
