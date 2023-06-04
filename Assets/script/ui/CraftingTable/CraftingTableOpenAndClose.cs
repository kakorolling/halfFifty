using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 할당된 키로 인벤토리 열고 닫기
public class CraftingTableOpenAndClose : MonoBehaviour
{
    public GameObject CraftingTable;
    public GameObject CraftingTableDetailImage;
    static bool activelCraftingTable = false;
    
    private void Start()
    {
        CraftingTable.SetActive(activelCraftingTable);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O) && activelCraftingTable == false)
        {
            activelCraftingTable = !activelCraftingTable;
            CraftingTable.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.O) && activelCraftingTable == true)
        {
            activelCraftingTable = !activelCraftingTable;
            CraftingTable.SetActive(false);
            CraftingTableDetailImage.SetActive(false);
        }
        
    }

    public void ExitButtonClick()
    {
        activelCraftingTable = false;
        CraftingTable.SetActive(false);
        CraftingTableDetailImage.SetActive(false);
    }

}