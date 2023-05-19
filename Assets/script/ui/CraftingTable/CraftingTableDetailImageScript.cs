using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftingTableDetailImageScript : MonoBehaviour
{
    
    public GameObject DetailNameTextObject;
    public GameObject DetailExpressionTextObject1;
    public GameObject DetailExpressionTextObject2;

    public void SetText(string DetailNameText, string[] DetailExpressionText)
    {
        
        DetailNameTextObject.GetComponent<Text>().text = DetailNameText;
        DetailExpressionTextObject1.GetComponent<Text>().text = DetailExpressionText[0];
        DetailExpressionTextObject2.GetComponent<Text>().text = DetailExpressionText[1];
    }

}
