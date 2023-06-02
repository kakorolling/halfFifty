using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredienttest : MonoBehaviour
{
   
    public void ConfirmButtonClick()
    {
        
        InventoryManager.AddItem(new Item("나무 도막", 1, "Wood", "재료"));
        InventoryManager.AddItem(new Item("사과", 1, "apple", "음식"));
        InventoryManager.AddItem(new Item("바나나", 1, "banana1", "음식"));
        InventoryManager.AddItem(new Item("바나나2", 1, "banana2", "음식"));
        InventoryManager.AddItem(new Item("나무 도끼", 1, "WoodAxe", "도구"));
        InventoryManager.AddItem(new Item("나무 낚싯대", 1, "FishingRod", "도구"));

     
    }
}
