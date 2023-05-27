using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredienttest : MonoBehaviour
{
    public void ConfirmButtonClick()
    {
        
        InventoryManager.AddItem(new Item("나무 도막", 1, "Wood"));
        InventoryManager.AddItem(new Item("사과", 1, "apple"));
        InventoryManager.AddItem(new Item("바나나", 1, "banana1"));
        InventoryManager.AddItem(new Item("바나나2", 1, "banana2"));
        
        


    }
}
