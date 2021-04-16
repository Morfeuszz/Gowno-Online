using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public mouseLock Cursor;
    public configController DataController;
    private inventoryController inventory;

    void Start()
    {
        DataController = GameObject.Find ("DATA").GetComponent<configController>();
        Cursor = GameObject.Find ("DATA").GetComponent<mouseLock>();
        inventory = GameObject.Find ("Inventory").GetComponent<inventoryController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(DataController.config.keybinds.UI.inventory))
        {
            inventory.SwitchVisibility();
            Cursor.locked = true;
        }
    }

    public void BackPanel()
    {
        Cursor.locked = false;
    }
}
