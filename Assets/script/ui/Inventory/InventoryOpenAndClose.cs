using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 할당된 키로 인벤토리 열고 닫기
public class InventoryOpenAndClose : MonoBehaviour
{
    public GameObject inventoryPanel;
    static bool activelInventory = false;
    
    private void Start()
    {
        inventoryPanel.SetActive(activelInventory);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && activelInventory == false)
        {
            activelInventory = !activelInventory;
            inventoryPanel.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.E) && activelInventory == true)
        {
            activelInventory = !activelInventory;
            inventoryPanel.SetActive(false);
        }
        
    }

    public void ExitButtonClick()
    {
        activelInventory = false;
        inventoryPanel.SetActive(false);
    }
}