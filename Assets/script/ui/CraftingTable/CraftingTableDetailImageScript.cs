using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftingTableDetailImageScript : MonoBehaviour
{
    
    // 아이템 이름과 설명 변수
    public GameObject DetailNameTextObject;
    public GameObject DetailExpressionTextObject1;
    public GameObject DetailExpressionTextObject2;

    // 아이템의 아이콘과 재료 아이템의 아이콘 변수
    public GameObject DetailIconImageObject;
    public GameObject DetailIngredientIconImageObject1;
    public GameObject DetailIngredientIconImageObject2;
    public GameObject DetailIngredientIconImageObject3;
    public GameObject DetailIngredientIconImageObject4;

    //아이템 수량 변수
    public GameObject DetailIngredientAmountObject1;
    public GameObject DetailIngredientAmountObject2;
    public GameObject DetailIngredientAmountObject3;
    public GameObject DetailIngredientAmountObject4;


    // 아이템 이름, 세부 설명 설정 메소드
    public void SetText(string DetailNameText, string[] DetailExpressionText)
    {
        
        DetailNameTextObject.GetComponent<Text>().text = DetailNameText;
        DetailExpressionTextObject1.GetComponent<Text>().text = DetailExpressionText[0];
        DetailExpressionTextObject2.GetComponent<Text>().text = DetailExpressionText[1];
    }

    // 아이템 이미지, 재료 아이템 이미지 설정 메소드
    public void SetImage(Image DetailIconImage, Image DetailIngredientIconImage1,
                         Image DetailIngredientIconImage2, Image DetailIngredientIconImage3, Image DetailIngredientIconImage4)
    {
        Image detailIconImageComponent = DetailIconImageObject.GetComponent<Image>();
        Image detailIngredientIconImageComponent1 = DetailIngredientIconImageObject1.GetComponent<Image>();
        Image detailIngredientIconImageComponent2 = DetailIngredientIconImageObject2.GetComponent<Image>();
        Image detailIngredientIconImageComponent3 = DetailIngredientIconImageObject3.GetComponent<Image>();
        Image detailIngredientIconImageComponent4 = DetailIngredientIconImageObject4.GetComponent<Image>();

        detailIconImageComponent.sprite = DetailIconImage.sprite;
        detailIngredientIconImageComponent1.sprite = DetailIngredientIconImage1.sprite;
        detailIngredientIconImageComponent2.sprite = DetailIngredientIconImage2.sprite;
        detailIngredientIconImageComponent3.sprite = DetailIngredientIconImage3.sprite;
        detailIngredientIconImageComponent4.sprite = DetailIngredientIconImage4.sprite;
    }

    // 재료 아이템 수량 설정 메소드
    public void SetAmount(string DetailIngredientAmount1, string DetailIngredientAmount2, string DetailIngredientAmount3, string DetailIngredientAmount4)
    {
        DetailIngredientAmountObject1.GetComponent<Text>().text = DetailIngredientAmount1;
        DetailIngredientAmountObject2.GetComponent<Text>().text = DetailIngredientAmount2;
        DetailIngredientAmountObject3.GetComponent<Text>().text = DetailIngredientAmount3;
        DetailIngredientAmountObject4.GetComponent<Text>().text = DetailIngredientAmount4;

    }
    

}
