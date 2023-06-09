using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 참고 https://ansohxxn.github.io/unity%20lesson%203/ch6-1/
public class QuickSlotController : MonoBehaviour
{
    [SerializeField] private Slot[] quickSlots; //퀵슬롯 8개
    [SerializeField] private Transform tfParent; //퀵슬롯의 부모 오브젝트

    //선택된 퀵슬롯의 인덱스(0~7)
    //private int selectedSlot; 

    //선택된 퀵슬롯
    private GameObject SelectedQuickSlot;

    //선택된 퀵슬롯에 들어있는 아이템의 이미지
    private Image SelectedQuickSlotItemImage; 
    
    // 퀵슬롯을 눌러 사용중인 아이템
    public Item Usingitem;

    // 퀵슬롯을 눌러 입고 있는 의상의 이름
    public string wearingClothes;

    [SerializeField] private GameObject QuickSlotContainer;
    
    void Start()
    {
        //quickSlots = tfParent.GetComponentsInChildren<Slot>();
        //selectedSlot = 0;

        Usingitem = new Item("", 0, "", "");
        wearingClothes = "농부 의상";
    }

    // Update is called once per frame
    void Update()
    {
        TryInputNumber();
    }


    private void TryInputNumber()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            //ChangeSlot(0);
            //Debug.Log("1 동작 확인");

            SelectedQuickSlot = GameObject.Find("Slot0");
            SelectedQuickSlotItemImage = SelectedQuickSlot.transform.GetChild(0).GetComponent<Image>();
            
            if(!(SelectedQuickSlotItemImage.sprite.name == "Transparent"))
            {
                foreach(Item ItemInInventory in InventoryManager.inventoryItems)
                {
                    if((ItemInInventory.GetImageName() == SelectedQuickSlotItemImage.sprite.name) && (ItemInInventory.GetItemType() != "의상"))
                    {
                        Usingitem = ItemInInventory;
                    }
                    else if((ItemInInventory.GetImageName() == SelectedQuickSlotItemImage.sprite.name) && (ItemInInventory.GetItemType() == "의상"))
                    {
                        wearingClothes = ItemInInventory.GetName();
                        Debug.Log(wearingClothes);
                    }
                }
            }
            else if(SelectedQuickSlotItemImage.sprite.name == "Transparent")
            {
                Usingitem = new Item("", 0, "", "");
            }

            QuickSlotContainer.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIImage/Inventory/QuickSlots1");
        }

        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            //ChangeSlot(1);
            //Debug.Log("2 동작 확인");

            SelectedQuickSlot = GameObject.Find("Slot1");
            SelectedQuickSlotItemImage = SelectedQuickSlot.transform.GetChild(0).GetComponent<Image>();
            
            if(!(SelectedQuickSlotItemImage.sprite.name == "Transparent"))
            {
                foreach(Item ItemInInventory in InventoryManager.inventoryItems)
                {
                    if((ItemInInventory.GetImageName() == SelectedQuickSlotItemImage.sprite.name) && (ItemInInventory.GetItemType() != "의상"))
                    {
                        Usingitem = ItemInInventory;
                    }
                    else if((ItemInInventory.GetImageName() == SelectedQuickSlotItemImage.sprite.name) && (ItemInInventory.GetItemType() == "의상"))
                    {
                        wearingClothes = ItemInInventory.GetName();
                    }
                }
            }
            else if(SelectedQuickSlotItemImage.sprite.name == "Transparent")
            {
                Usingitem = new Item("", 0, "", "");
            }

            QuickSlotContainer.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIImage/Inventory/QuickSlots2");

        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            //ChangeSlot(2);
            //Debug.Log("3 동작 확인");

            SelectedQuickSlot = GameObject.Find("Slot2");
            SelectedQuickSlotItemImage = SelectedQuickSlot.transform.GetChild(0).GetComponent<Image>();
            
            if(!(SelectedQuickSlotItemImage.sprite.name == "Transparent"))
            {
                foreach(Item ItemInInventory in InventoryManager.inventoryItems)
                {
                    if((ItemInInventory.GetImageName() == SelectedQuickSlotItemImage.sprite.name) && (ItemInInventory.GetItemType() != "의상"))
                    {
                        Usingitem = ItemInInventory;
                    }
                    else if((ItemInInventory.GetImageName() == SelectedQuickSlotItemImage.sprite.name) && (ItemInInventory.GetItemType() == "의상"))
                    {
                        wearingClothes = ItemInInventory.GetName();
                    }
                }
            }
            else if(SelectedQuickSlotItemImage.sprite.name == "Transparent")
            {
                Usingitem = new Item("", 0, "", "");
            }

            QuickSlotContainer.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIImage/Inventory/QuickSlots3");

        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            //ChangeSlot(3);
            //Debug.Log("4 동작 확인");

            SelectedQuickSlot = GameObject.Find("Slot3");
            SelectedQuickSlotItemImage = SelectedQuickSlot.transform.GetChild(0).GetComponent<Image>();
            
            if(!(SelectedQuickSlotItemImage.sprite.name == "Transparent"))
            {
                foreach(Item ItemInInventory in InventoryManager.inventoryItems)
                {
                    if((ItemInInventory.GetImageName() == SelectedQuickSlotItemImage.sprite.name) && (ItemInInventory.GetItemType() != "의상"))
                    {
                        Usingitem = ItemInInventory;
                    }
                    else if((ItemInInventory.GetImageName() == SelectedQuickSlotItemImage.sprite.name) && (ItemInInventory.GetItemType() == "의상"))
                    {
                        wearingClothes = ItemInInventory.GetName();
                    }
                }
            }
            else if(SelectedQuickSlotItemImage.sprite.name == "Transparent")
            {
                Usingitem = new Item("", 0, "", "");
            }
            
            QuickSlotContainer.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIImage/Inventory/QuickSlots4");

        }
        else if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            //ChangeSlot(4);
            //Debug.Log("5 동작 확인");

            SelectedQuickSlot = GameObject.Find("Slot4");
            SelectedQuickSlotItemImage = SelectedQuickSlot.transform.GetChild(0).GetComponent<Image>();
            
            if(!(SelectedQuickSlotItemImage.sprite.name == "Transparent"))
            {
                foreach(Item ItemInInventory in InventoryManager.inventoryItems)
                {
                    if((ItemInInventory.GetImageName() == SelectedQuickSlotItemImage.sprite.name) && (ItemInInventory.GetItemType() != "의상"))
                    {
                        Usingitem = ItemInInventory;
                    }
                    else if((ItemInInventory.GetImageName() == SelectedQuickSlotItemImage.sprite.name) && (ItemInInventory.GetItemType() == "의상"))
                    {
                        wearingClothes = ItemInInventory.GetName();
                    }
                }
            }
            else if(SelectedQuickSlotItemImage.sprite.name == "Transparent")
            {
                Usingitem = new Item("", 0, "", "");
            }

            QuickSlotContainer.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIImage/Inventory/QuickSlots5");

        }
        else if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            //ChangeSlot(5);
            //Debug.Log("6 동작 확인");

            SelectedQuickSlot = GameObject.Find("Slot5");
            SelectedQuickSlotItemImage = SelectedQuickSlot.transform.GetChild(0).GetComponent<Image>();
            
            if(!(SelectedQuickSlotItemImage.sprite.name == "Transparent"))
            {
                foreach(Item ItemInInventory in InventoryManager.inventoryItems)
                {
                    if((ItemInInventory.GetImageName() == SelectedQuickSlotItemImage.sprite.name) && (ItemInInventory.GetItemType() != "의상"))
                    {
                        Usingitem = ItemInInventory;
                    }
                    else if((ItemInInventory.GetImageName() == SelectedQuickSlotItemImage.sprite.name) && (ItemInInventory.GetItemType() == "의상"))
                    {
                        wearingClothes = ItemInInventory.GetName();
                    }
                }
            }
            else if(SelectedQuickSlotItemImage.sprite.name == "Transparent")
            {
                Usingitem = new Item("", 0, "", "");
            }

            QuickSlotContainer.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIImage/Inventory/QuickSlots6");

        }
        else if(Input.GetKeyDown(KeyCode.Alpha7))
        {
            //ChangeSlot(6);
            //Debug.Log("7 동작 확인");

            SelectedQuickSlot = GameObject.Find("Slot6");
            SelectedQuickSlotItemImage = SelectedQuickSlot.transform.GetChild(0).GetComponent<Image>();
            
            if(!(SelectedQuickSlotItemImage.sprite.name == "Transparent"))
            {
                foreach(Item ItemInInventory in InventoryManager.inventoryItems)
                {
                    if((ItemInInventory.GetImageName() == SelectedQuickSlotItemImage.sprite.name) && (ItemInInventory.GetItemType() != "의상"))
                    {
                        Usingitem = ItemInInventory;
                    }
                    else if((ItemInInventory.GetImageName() == SelectedQuickSlotItemImage.sprite.name) && (ItemInInventory.GetItemType() == "의상"))
                    {
                        wearingClothes = ItemInInventory.GetName();
                    }
                }
            }
            else if(SelectedQuickSlotItemImage.sprite.name == "Transparent")
            {
                Usingitem = new Item("", 0, "", "");
            }

            QuickSlotContainer.GetComponent<Image>().sprite = Resources.Load<Sprite>("UIImage/Inventory/QuickSlots7");
        }
    }

    /*
    private void checkUsingItem()
    {

    }
    */



}
