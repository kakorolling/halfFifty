using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//테스트용 코드
using UnityEngine.SceneManagement;

public class CraftingTableConfirmManager : MonoBehaviour
{
    // 제작 버튼 로직에 사용
    public string itemName;
    public Image itemImage;

    public Image itemIngredientImage1;
    public Image itemIngredientImage2;
    public Image itemIngredientImage3;
    public Image itemIngredientImage4;
    public string itemIngredientAmount1;
    public string itemIngredientAmount2;
    public string itemIngredientAmount3;
    public string itemIngredientAmount4;


    // 제작 취소 버튼 로직에 사용
    GameObject Scroll;
    string DetailExpression1;
    string itemType;
    public GameObject DetailImage;
    public GameObject ConfirmImage;
    public GameObject ConfirmImageText;


    //제작하기 기능
    public void ConfirmButtonClick()
    {
        itemName = GameObject.Find("DetailName").GetComponent<Text>().text;
        itemImage = GameObject.Find("DetailIconImage").GetComponent<Image>();

        DetailExpression1 = GameObject.Find("DetailExpression1").GetComponent<Text>().text;
        itemType = DetailExpression1.Substring(DetailExpression1.Length - 2);

        itemIngredientImage1 = GameObject.Find("DetailIngredientImage1").GetComponent<Image>();
        itemIngredientImage2 = GameObject.Find("DetailIngredientImage2").GetComponent<Image>();
        itemIngredientImage3 = GameObject.Find("DetailIngredientImage3").GetComponent<Image>();
        itemIngredientImage4 = GameObject.Find("DetailIngredientImage4").GetComponent<Image>();

        itemIngredientAmount1 = GameObject.Find("DetailIngredientAmount1").GetComponent<Text>().text;
        itemIngredientAmount2 = GameObject.Find("DetailIngredientAmount2").GetComponent<Text>().text;
        itemIngredientAmount3 = GameObject.Find("DetailIngredientAmount3").GetComponent<Text>().text;
        itemIngredientAmount4 = GameObject.Find("DetailIngredientAmount4").GetComponent<Text>().text;

        Debug.Log(itemIngredientImage2.sprite.name);
        

        // 재료 아이템의 종류가 1개일때
        if(itemIngredientImage2.sprite.name == "Transparent")
        {
            if(InventoryManager.CheckItem(new Item("", int.Parse(itemIngredientAmount1), itemIngredientImage1.sprite.name, "")))
            {
                InventoryManager.DeleteItemByImage(new Item("", int.Parse(itemIngredientAmount1), itemIngredientImage1.sprite.name, ""));
                InventoryManager.AddItem(new Item("itemName", 1, itemImage.sprite.name, itemType));
                ConfirmImageText.GetComponent<Text>().text = "제작되었습니다.";
                //Debug.Log(itemType);
            }
            else
            {
                ConfirmImageText.GetComponent<Text>().text = "재료 아이템이 부족합니다.";
            }
        }

        // 재료 아이템의 종류가 2개일때
        else if(itemIngredientImage3.sprite.name == "Transparent")
        {
            if(InventoryManager.CheckItem(new Item("", int.Parse(itemIngredientAmount1), itemIngredientImage1.sprite.name, "")) &&
                InventoryManager.CheckItem(new Item("", int.Parse(itemIngredientAmount2), itemIngredientImage2.sprite.name, "")))
            {
                InventoryManager.DeleteItemByImage(new Item("", int.Parse(itemIngredientAmount1), itemIngredientImage1.sprite.name, ""));
                InventoryManager.DeleteItemByImage(new Item("", int.Parse(itemIngredientAmount2), itemIngredientImage2.sprite.name, ""));
                InventoryManager.AddItem(new Item(itemName, 1, itemImage.sprite.name, itemType));
                ConfirmImageText.GetComponent<Text>().text = "제작되었습니다.";
            }
            else
            {
                ConfirmImageText.GetComponent<Text>().text = "재료 아이템이 부족합니다.";
            }
        }

        // 재료 아이템의 종류가 3개일때
        else if(itemIngredientImage4.sprite.name == "Transparent")
        {
            if(InventoryManager.CheckItem(new Item("", int.Parse(itemIngredientAmount1), itemIngredientImage1.sprite.name, "")) &&
                InventoryManager.CheckItem(new Item("", int.Parse(itemIngredientAmount2), itemIngredientImage2.sprite.name, "")) &&
                InventoryManager.CheckItem(new Item("", int.Parse(itemIngredientAmount3), itemIngredientImage3.sprite.name, "")))
            {
                InventoryManager.DeleteItemByImage(new Item("", int.Parse(itemIngredientAmount1), itemIngredientImage1.sprite.name, ""));
                InventoryManager.DeleteItemByImage(new Item("", int.Parse(itemIngredientAmount2), itemIngredientImage2.sprite.name, ""));
                InventoryManager.DeleteItemByImage(new Item("", int.Parse(itemIngredientAmount2), itemIngredientImage3.sprite.name, ""));
                InventoryManager.AddItem(new Item(itemName, 1, itemImage.sprite.name, itemType));
                ConfirmImageText.GetComponent<Text>().text = "제작되었습니다.";
            }
            else
            {
                ConfirmImageText.GetComponent<Text>().text = "재료 아이템이 부족합니다.";
            }
        }

        // 재료 아이템의 종류가 4개일때
        else if(itemIngredientAmount4 != "Transparent")
        {
            if(InventoryManager.CheckItem(new Item("", int.Parse(itemIngredientAmount1), itemIngredientImage1.sprite.name, "")) &&
                InventoryManager.CheckItem(new Item("", int.Parse(itemIngredientAmount2), itemIngredientImage2.sprite.name, "")) &&
                InventoryManager.CheckItem(new Item("", int.Parse(itemIngredientAmount3), itemIngredientImage3.sprite.name, "")) &&
                InventoryManager.CheckItem(new Item("", int.Parse(itemIngredientAmount4), itemIngredientImage4.sprite.name, "")))
            {
                InventoryManager.DeleteItemByImage(new Item("", int.Parse(itemIngredientAmount1), itemIngredientImage1.sprite.name, ""));
                InventoryManager.DeleteItemByImage(new Item("", int.Parse(itemIngredientAmount2), itemIngredientImage2.sprite.name, ""));
                InventoryManager.DeleteItemByImage(new Item("", int.Parse(itemIngredientAmount3), itemIngredientImage3.sprite.name, ""));
                InventoryManager.DeleteItemByImage(new Item("", int.Parse(itemIngredientAmount4), itemIngredientImage4.sprite.name, ""));
                InventoryManager.AddItem(new Item(itemName, 1, itemImage.sprite.name, itemType));
                ConfirmImageText.GetComponent<Text>().text = "제작되었습니다.";
            }
            else
            {
                ConfirmImageText.GetComponent<Text>().text = "재료 아이템이 부족합니다.";
            }
        }

        ConfirmImage.SetActive(true);
        SceneManager.LoadScene("Scene4");
    
    }
   


    //제작 취소 기능
    public void CancelButtonClick()
    {
        //세부사항 창 찾음
        DetailImage = GameObject.Find("DetailImage");
        DetailExpression1 = GameObject.Find("DetailExpression1").GetComponent<Text>().text;
        itemType = DetailExpression1.Substring(DetailExpression1.Length - 2);
        
        //세부사항의 문자열을 이용해 해당 아이템이 어느 스크롤뷰에 있는지 찾음
        if(itemType == "도구")
        {
            Scroll = GameObject.Find("ToolScroll");
            
        }
        else if(itemType == "음식")
        {
            Scroll = GameObject.Find("FoodScroll");
        }

        //세부사항 창 닫음
        DetailImage.SetActive(false);

        //해당 스크롤뷰 켜짐
        Scroll.SetActive(true);
    }
}
