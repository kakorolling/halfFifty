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
        {"나무 도끼", new string[]{"종류: 도구", "벌목 할 수 있는 나무 도끼입니다. 벌목시 나무 도막을 2개 획득합니다."}},
        {"나무 낫", new string[]{"종류: 도구", "농작물을 수확 할 수 있는 나무 낫입니다."}},
        {"나무 곡괭이", new string[]{"종류: 도구", "광물을 캘 수 있는 나무 곡괭이입니다."}},
        {"나무 갈퀴", new string[]{"종류: 도구", "땅의 상태를 농사가 가능한 상태로 바꿀 수 있는 나무 갈퀴입니다."}},
        {"나무 낚싯대", new string[]{"종류: 도구", "낚시를 할 수 있는 나무 낚싯대입니다."}},
        {"철 칼", new string[]{"종류: 도구", "상대를 공격 할 수 있는 철 칼입니다."}},
        {"천", new string[]{"종류: 재료", "의상을 제작 할 수 있는 천입니다."}},
        {"슈 의상", new string[]{"종류: 의상", "슈의 모습을 한 캐릭터 의상입니다."}},
        
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
        DetailScript.SetAmount(clickedObject.transform.GetChild(6).GetComponent<Text>().text, clickedObject.transform.GetChild(7).GetComponent<Text>().text,
                                clickedObject.transform.GetChild(8).GetComponent<Text>().text, clickedObject.transform.GetChild(9).GetComponent<Text>().text);
    }

    public void OnClickImage()
    {
        DetailImage.SetActive(true);   
    }
}