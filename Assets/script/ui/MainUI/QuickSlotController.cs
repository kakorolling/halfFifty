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

    
    void Start()
    {
        quickSlots = tfParent.GetComponentsInChildren<Slot>();
        //selectedSlot = 0;

        Usingitem = new Item("", 0, "", "");
    }

    // Update is called once per frame
    void Update()
    {
        TryInputNumber();
    }

    /*
    private void ChangeSlot(int _num)
    {
        SelectedSlot(_num);
    }

    private void SelectedSlot(int _num)
    {
        selectedSlot = _num; //선택된 슬롯
        
    }
    */

    private void TryInputNumber()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            //ChangeSlot(0);
            //Debug.Log("1 동작 확인");

            SelectedQuickSlot = GameObject.Find("QuickSlot1");
            SelectedQuickSlotItemImage = SelectedQuickSlot.transform.GetChild(0).GetComponent<Image>();
            
            if(!(SelectedQuickSlotItemImage.sprite.name == "Transparent"))
            {
                foreach(Item ItemInInventory in InventoryManager.inventoryItems)
                {
                    if(ItemInInventory.GetImageName() == SelectedQuickSlotItemImage.sprite.name)
                    {
                        Usingitem = ItemInInventory;
                        Debug.Log(Usingitem.GetItemType());
                    }
                }
            }
            else if(SelectedQuickSlotItemImage.sprite.name == "Transparent")
            {
                Usingitem = new Item("", 0, "", "");
            }
        }

        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            //ChangeSlot(1);
            //Debug.Log("2 동작 확인");

            SelectedQuickSlot = GameObject.Find("QuickSlot2");
            SelectedQuickSlotItemImage = SelectedQuickSlot.transform.GetChild(0).GetComponent<Image>();
            
            if(!(SelectedQuickSlotItemImage.sprite.name == "Transparent"))
            {
                foreach(Item ItemInInventory in InventoryManager.inventoryItems)
                {
                    if(ItemInInventory.GetImageName() == SelectedQuickSlotItemImage.sprite.name)
                    {
                        Usingitem = ItemInInventory;
                        Debug.Log(Usingitem.GetItemType());
                    }
                }
            }
            else if(SelectedQuickSlotItemImage.sprite.name == "Transparent")
            {
                Usingitem = new Item("", 0, "", "");
            }

        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            //ChangeSlot(2);
            //Debug.Log("3 동작 확인");

            SelectedQuickSlot = GameObject.Find("QuickSlot3");
            SelectedQuickSlotItemImage = SelectedQuickSlot.transform.GetChild(0).GetComponent<Image>();
            
            if(!(SelectedQuickSlotItemImage.sprite.name == "Transparent"))
            {
                foreach(Item ItemInInventory in InventoryManager.inventoryItems)
                {
                    if(ItemInInventory.GetImageName() == SelectedQuickSlotItemImage.sprite.name)
                    {
                        Usingitem = ItemInInventory;
                        Debug.Log(Usingitem.GetItemType());
                    }
                }
            }
            else if(SelectedQuickSlotItemImage.sprite.name == "Transparent")
            {
                Usingitem = new Item("", 0, "", "");
            }

        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            //ChangeSlot(3);
            //Debug.Log("4 동작 확인");

            SelectedQuickSlot = GameObject.Find("QuickSlot4");
            SelectedQuickSlotItemImage = SelectedQuickSlot.transform.GetChild(0).GetComponent<Image>();
            
            if(!(SelectedQuickSlotItemImage.sprite.name == "Transparent"))
            {
                foreach(Item ItemInInventory in InventoryManager.inventoryItems)
                {
                    if(ItemInInventory.GetImageName() == SelectedQuickSlotItemImage.sprite.name)
                    {
                        Usingitem = ItemInInventory;
                        Debug.Log(Usingitem.GetItemType());
                    }
                }
            }
            else if(SelectedQuickSlotItemImage.sprite.name == "Transparent")
            {
                Usingitem = new Item("", 0, "", "");
            }
            
        }
        else if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            //ChangeSlot(4);
            //Debug.Log("5 동작 확인");

            SelectedQuickSlot = GameObject.Find("QuickSlot5");
            SelectedQuickSlotItemImage = SelectedQuickSlot.transform.GetChild(0).GetComponent<Image>();
            
            if(!(SelectedQuickSlotItemImage.sprite.name == "Transparent"))
            {
                foreach(Item ItemInInventory in InventoryManager.inventoryItems)
                {
                    if(ItemInInventory.GetImageName() == SelectedQuickSlotItemImage.sprite.name)
                    {
                        Usingitem = ItemInInventory;
                        Debug.Log(Usingitem.GetItemType());
                    }
                }
            }
            else if(SelectedQuickSlotItemImage.sprite.name == "Transparent")
            {
                Usingitem = new Item("", 0, "", "");
            }

        }
        else if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            //ChangeSlot(5);
            //Debug.Log("6 동작 확인");

            SelectedQuickSlot = GameObject.Find("QuickSlot6");
            SelectedQuickSlotItemImage = SelectedQuickSlot.transform.GetChild(0).GetComponent<Image>();
            
            if(!(SelectedQuickSlotItemImage.sprite.name == "Transparent"))
            {
                foreach(Item ItemInInventory in InventoryManager.inventoryItems)
                {
                    if(ItemInInventory.GetImageName() == SelectedQuickSlotItemImage.sprite.name)
                    {
                        Usingitem = ItemInInventory;
                        Debug.Log(Usingitem.GetItemType());
                    }
                }
            }
            else if(SelectedQuickSlotItemImage.sprite.name == "Transparent")
            {
                Usingitem = new Item("", 0, "", "");
            }

        }
        else if(Input.GetKeyDown(KeyCode.Alpha7))
        {
            //ChangeSlot(6);
            //Debug.Log("7 동작 확인");

            SelectedQuickSlot = GameObject.Find("QuickSlot7");
            SelectedQuickSlotItemImage = SelectedQuickSlot.transform.GetChild(0).GetComponent<Image>();
            
            if(!(SelectedQuickSlotItemImage.sprite.name == "Transparent"))
            {
                foreach(Item ItemInInventory in InventoryManager.inventoryItems)
                {
                    if(ItemInInventory.GetImageName() == SelectedQuickSlotItemImage.sprite.name)
                    {
                        Usingitem = ItemInInventory;
                        Debug.Log(Usingitem.GetItemType());
                    }
                }
            }
            else if(SelectedQuickSlotItemImage.sprite.name == "Transparent")
            {
                Usingitem = new Item("", 0, "", "");
            }
        }
    }

    /*
    private void checkUsingItem()
    {

    }
    */



}
