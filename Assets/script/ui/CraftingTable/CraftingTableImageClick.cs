using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingTableImageClick : MonoBehaviour, IPointerClickHandler
{
    public GameObject DetailImage;
    public GameObject Scroll;
    GameObject clickedObject;
    Dictionary<string, string[]> dic = new Dictionary<string, string[]>()
    {
        {"나무 삽", new string[]{"종류: 도구", "땅을 팔 수 있는 나무 삽입니다."}},
        {"나무 칼", new string[]{"종류: 도구", "공격 할 수 있는 나무 칼입니다."}},
        {"나무 도끼", new string[]{"종류: 도구", "벌목 할 수 있는 나무 도끼입니다."}},
        {"사과 파이", new string[]{"종류: 음식", "먹으면 체력을 회복하는 사과 파이입니다."}},
        
    };

    public void OnPointerClick(PointerEventData eventData)
    {
        this.clickedObject = eventData.pointerPress;
        Debug.Log("Clicked object: " + clickedObject.name);
        Debug.Log("Clicked object: " + clickedObject.transform.GetChild(1).GetComponent<Text>().text);
        
        CraftingTableDetailImageScript DetailScript = DetailImage.GetComponent<CraftingTableDetailImageScript>();
        DetailScript.SetText(clickedObject.transform.GetChild(1).GetComponent<Text>().text, dic[clickedObject.transform.GetChild(1).GetComponent<Text>().text]);
        DetailScript.SetImage(clickedObject.transform.GetChild(0).GetComponent<Image>(), clickedObject.transform.GetChild(2).GetComponent<Image>(),
                            clickedObject.transform.GetChild(3).GetComponent<Image>(), clickedObject.transform.GetChild(4).GetComponent<Image>(),
                            clickedObject.transform.GetChild(5).GetComponent<Image>());
    }

    public void OnClickImage()
    {
        DetailImage.SetActive(true);   
    }
}