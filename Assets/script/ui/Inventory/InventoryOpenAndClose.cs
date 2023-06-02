using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 할당된 키로 인벤토리 열고 닫기
public class InventoryOpenAndClose : MonoBehaviour
{
    public GameObject inventoryPanel;
    bool activelInventory = false;
    
    private void Start(){
        inventoryPanel.SetActive(activelInventory);
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.E))
        {
            activelInventory = !activelInventory;
            inventoryPanel.SetActive(activelInventory);

            InventoryManager.ShowItem();
        }
        
    }
}