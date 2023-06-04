using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredienttest : MonoBehaviour
{
   
    public void ConfirmButtonClick()
    {
        
        InventoryManager.AddItem(new Item("천", 4, "Cloth", "재료"));
        InventoryManager.AddItem(new Item("랍스터", 1, "sea_river_crayfish", "생선"));
        

     
    }
}
