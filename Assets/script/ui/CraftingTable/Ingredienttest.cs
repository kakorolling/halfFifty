using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredienttest : MonoBehaviour
{
   
    public void ConfirmButtonClick()
    {
        
        InventoryManager.AddItem(new Item("천", 4, "Cloth", "재료"));
        InventoryManager.AddItem(new Item("랍스터", 1, "sea_river_crayfish", "생선"));
        InventoryManager.AddItem(new Item("나무 도막", 1, "Wood", "재료"));
        InventoryManager.AddItem(new Item("나무 도끼", 1, "WoodAxe", "도구"));
        InventoryManager.AddItem(new Item("나무 낚싯대", 1, "FishingRod", "도구"));
        InventoryManager.AddItem(new Item("천", 1, "Cloth", "재료"));
        InventoryManager.AddItem(new Item("실", 1, "Thread", "재료"));
        

     
    }
}
