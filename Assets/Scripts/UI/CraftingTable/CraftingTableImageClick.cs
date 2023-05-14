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
        {"나무 삽", new string[]{"땅을 팔 수 있는 나무 삽입니다.", "두번째 텍스트 입니다."}},
        {"나무 칼", new string[]{"공격할 수 있는 나무 칼입니다.", "두번째 텍스트 입니다."}},
        
    };

    public void OnPointerClick(PointerEventData eventData)
    {
        this.clickedObject = eventData.pointerPress;
        Debug.Log("Clicked object: " + clickedObject.name);
        CraftingTableDetailImageScript DetailScript = DetailImage.GetComponent<CraftingTableDetailImageScript>();
        DetailScript.SetText(clickedObject.transform.GetChild(1).GetComponent<Text>().text, dic[clickedObject.transform.GetChild(1).GetComponent<Text>().text]);
    }

    public void OnClickImage()
    {
        GameObject[] Scrolls = GameObject.FindGameObjectsWithTag("Scroll");
        foreach (GameObject Scroll in Scrolls)
        {
            Scroll.SetActive(false);
        }

        DetailImage.SetActive(true);

        
    }
}