using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryController : MonoBehaviour
{
    private string keybind;
    private DataHolder DataController;
    public GameObject content;
    public GameObject slotPrefab;

    [System.Serializable]
    public class InventorySlot{
        public int itemID = 0;
        public GameObject slotObject;

        public InventorySlot (GameObject _slotObject){
            slotObject = _slotObject;
        }
    }


    public List<InventorySlot> inventory = new List<InventorySlot>();

    void Start(){
        DataController = GameObject.Find ("DATA").GetComponent<DataHolder>();
        for(var i = 0;i<DataController.data.inventory.slots;i++){
            inventory.Add(new InventorySlot(Instantiate(slotPrefab, new Vector3(0,0,0), Quaternion.identity, content.transform)));
        }
    }

    void Update()
    {
        if(inventory.Count != DataController.data.inventory.slots){
            if(inventory.Count < DataController.data.inventory.slots){
                inventory.Add(new InventorySlot(Instantiate(slotPrefab, new Vector3(0,0,0), Quaternion.identity, content.transform)));
            } else {
                for(var i = inventory.Count-1; i >= 0 ; i--){
                    if(inventory[i].itemID == 0){
                        Destroy(inventory[i].slotObject);
                        inventory.RemoveAt(i);
                        break;
                    }
                }
            }
            

        }
    }

    public void SwitchVisibility(){
        gameObject.active = !gameObject.activeSelf;
    }
}
