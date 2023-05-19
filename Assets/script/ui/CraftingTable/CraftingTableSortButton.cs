using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingTableSortButton : MonoBehaviour
{
    public GameObject Scroll;
    
    public void ButtonClick()
    {
        GameObject[] Scrolls = GameObject.FindGameObjectsWithTag("Scroll");
        foreach (GameObject Scroll in Scrolls)
        {
            Scroll.SetActive(false);
        }
        
        Scroll.SetActive(true);
       
        
    }
}
