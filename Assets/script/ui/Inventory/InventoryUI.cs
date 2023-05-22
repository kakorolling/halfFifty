using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    bool activelInventory = false;
    InventoryManager inventoryManager = new InventoryManager();
    
    private void Start(){
        inventoryPanel.SetActive(activelInventory);
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.E))
        {
            activelInventory = !activelInventory;
            inventoryPanel.SetActive(activelInventory);
            inventoryManager.ShowItem();

        }
    
  
    }
}