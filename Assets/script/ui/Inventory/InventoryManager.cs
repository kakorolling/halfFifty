using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager
{
    // 인벤토리의 아이템과 수량을 저장하는 딕셔너리
    public Dictionary<string, string> inventoryItems; 

    public InventoryManager()
    {
        inventoryItems = new Dictionary<string, string>();
    }

    public void AddItem(string itemName, string amount)
    {
        if (inventoryItems.ContainsKey(itemName))
        {
            inventoryItems[itemName] += amount;
        }
        else
        {
            inventoryItems.Add(itemName, amount);
        }

        Debug.Log(amount + "개의 " + itemName + "이(가) 인벤토리에 추가되었습니다.");
        //Debug.Log(inventoryItems[itemName]);
    }

    
}
