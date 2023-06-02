using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager
{
    // 인벤토리 슬롯들의 배열
   

    // 인벤토리에 보일 아이템의 배열
    public static List<Item> inventoryItems = new List<Item>();
    private const int INVENTORY_SIZE = 56;
    
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
                ShowItem();
                return;
            }
        }
       
        // 아이템이 빈 슬롯에 찾아 들어가는 로직 Made By 용준
        for(int i = 0; i < INVENTORY_SIZE; i++)
        {
            bool flag = true;

            for(int j = 0; j < inventoryItems.Count; j++)
            {
                
                if(i == inventoryItems[j].GetSlotNumber())
                {
                    flag = false;
                    break;
                }
                
            }
            
            if(flag)
            {
                item.SetSlotNumber(i);
                break;
            }
        }

        // inventoryItems 배열에 아이템 추가
        inventoryItems.Add(item);
        ShowItem();
        //Debug.Log(item.GetItemType());
    
        //인벤토리 사이즈 초과시 아이템 더 획득 안되게하는 기능 추가 필요
    }

    // 인벤토리에 아이템이 수량만큼 있는지 확인하는 메소드
    public static bool CheckItem(Item item)
    {
        foreach(Item ItemInInventory in inventoryItems)
        {
            if( (ItemInInventory.GetName() == item.GetName() && ItemInInventory.GetAmount() >= item.GetAmount()) ||
                (ItemInInventory.GetImageName() == item.GetImageName() && ItemInInventory.GetAmount() >= item.GetAmount()) )
            {
                return true;
            }
        }
        return false;
    }

    // 아이템의 이름 정보로 아이템을 삭제하는 메소드
    public static void DeleteItem(Item item)
    {
        foreach(Item ItemInInventory in inventoryItems)
        {
            if(ItemInInventory.GetName() == item.GetName() && ItemInInventory.GetAmount() >= item.GetAmount())
            {
                ItemInInventory.SetAmount(ItemInInventory.GetAmount() - item.GetAmount());

                if(ItemInInventory.GetAmount() == 0)
                {
                    inventoryItems.Remove(item);
                }
            }
        }
        ShowItem();
    }


    // 아이템의 이미지 정보로 아이템을 삭제하는 메소드
    public static void DeleteItemByImage(Item item)
    {
        foreach(Item ItemInInventory in inventoryItems)
        {
            if(ItemInInventory.GetImageName() == item.GetImageName() && ItemInInventory.GetAmount() >= item.GetAmount())
            {
                ItemInInventory.SetAmount(ItemInInventory.GetAmount() - item.GetAmount());

                if(ItemInInventory.GetAmount() == 0)
                {
                    inventoryItems.Remove(item);
                }
            }
        }
        ShowItem();
    }


    

    public static void ShowItem()
    {
        // Slot 오브젝트를 찾아서 slots 배열에 할당
        GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");
        
        //인벤토리가 안켜져있을때 에러방지
        if(slots.Length == 0)
        {
            return;
        }
        
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            //Debug.Log(i);
            //Debug.Log(inventoryItems.Count);
            if((inventoryItems[i] == null) || (inventoryItems[i].GetAmount() == 0))
            {
                continue;
            }

            // 변수에 inventoryItems배열에 있는 아이템들의 정보를 할당
            string itemName = inventoryItems[i].GetName();
            int itemAmount = inventoryItems[i].GetAmount();
            string itemImage = inventoryItems[i].GetImageName();
            int itemSlotNumber = inventoryItems[i].GetSlotNumber();
            

            // 할당된 정보로 슬롯의 이미지 설정
            Image slotImage = slots[itemSlotNumber].transform.GetChild(0).GetComponent<Image>();
            slotImage.sprite = Resources.Load<Sprite>("UIImage/CommonUIImage/" + itemImage);

            // 할당된 정보로 슬롯의 갯수 텍스트 설정
            Text slotText =  slots[itemSlotNumber].transform.GetChild(1).GetComponent<Text>();
            slotText.text = itemAmount.ToString();
            //Debug.Log(slotText.text);

            
        }
    }

    
   
}
