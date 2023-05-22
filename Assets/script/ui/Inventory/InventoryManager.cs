using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager
{
    public GameObject[] slots;

    // 인벤토리에 보일 아이템의 배열
    public static List<Item> inventoryItems = new List<Item>();
    private const int INVENTORY_SIZE = 48;
    
    public static void AddItem(Item item)
    {
        // 인벤토리의 최대 사이즈보다 작을때만 Add 실행
        if(!(inventoryItems.Count < INVENTORY_SIZE))
        {
            return;
        }

        // 같은 아이템이 Add 되면 수량을 늘리는 기능
        // 도구와 같은 아이템은 안 합쳐지는 기능 추가 필요
        foreach(Item ItemInInventory in inventoryItems)
        {
            if(ItemInInventory.GetName() == item.GetName())
            {
                ItemInInventory.SetAmount(ItemInInventory.GetAmount() + item.GetAmount());
                return;
            }
        }
        
        inventoryItems.Add(item);
    
        //인벤토리 사이즈 초과시 아이템 더 획득 안되게하는 기능 추가 필요
    }

    /*
    public void DeleteItem()
    {

    }
    */

    public void ShowItem()
    {
        // Slot 오브젝트를 찾아서 slots 배열에 할당
        slots = GameObject.FindGameObjectsWithTag("Slot");
        
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if(inventoryItems[i] == null)
            {
                continue;
            }

            string itemName = inventoryItems[i].GetName();
            int itemAmount = inventoryItems[i].GetAmount();
            string itemImage = inventoryItems[i].GetImageName();

            Image slotImage = slots[i].transform.GetChild(0).GetComponent<Image>();
            slotImage.sprite = Resources.Load<Sprite>("UIImage/CommonUIImage/" + itemImage);

            Text slotText =  slots[i].transform.GetChild(1).GetComponent<Text>();
            slotText.text = itemAmount.ToString();
            Debug.Log(slotText.text);

            
        }
    }

    
   
}
