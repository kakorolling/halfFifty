using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoyuPanel;
    bool activelInventory = false;
    
    private void Start(){
        inventoyuPanel.SetActive(activelInventory);
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
                activelInventory = !activelInventory;
                inventoyuPanel.SetActive(activelInventory);
        }
    
  
    }
}