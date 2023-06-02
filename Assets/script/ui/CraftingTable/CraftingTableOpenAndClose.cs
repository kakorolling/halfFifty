using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 할당된 키로 인벤토리 열고 닫기
public class CraftingTableOpenAndClose : MonoBehaviour
{
    public GameObject CraftingTable;
    bool activelCraftingTable = false;
    
    private void Start(){
        CraftingTable.SetActive(activelCraftingTable);
        Debug.Log("good");
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("good");
            activelCraftingTable = !activelCraftingTable;
            CraftingTable.SetActive(activelCraftingTable);
        }
    }
}