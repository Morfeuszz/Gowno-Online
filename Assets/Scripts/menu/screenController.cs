using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenController : MonoBehaviour
{   
    public List<GameObject> screenList = new List<GameObject>();
    private int active;
    private DataHolder dataController;

    void changeScreen(int screenNr){
        screenList[active].SetActive(false);
        screenList[screenNr].SetActive(true);
        active = screenNr;
    }

    void Start()
    {
        dataController = GameObject.Find ("DATA").GetComponent<DataHolder>();
        active = dataController.menu.activeScreen;
        screenList[0].SetActive(false);
        screenList[active].SetActive(true);                                                                                                                                                                                   
    }

    void Update(){
        if(dataController.menu.activeScreen != active){
            changeScreen(dataController.menu.activeScreen);
        }
    }
}
