using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 출처 https://ansohxxn.github.io/unity%20lesson%203/ch6-1/
// slot.cs 에서 인벤토리에서 가져오는 것을 참고하여 추가 제작해야함
public class Slot : MonoBehaviour
{
    [SerializeField] private RectTransform baseRect; // Inventory_Base 의 영역
    [SerializeField] private RectTransform quickSlotBaseRect;
    // 퀵슬롯의 영역. 퀵슬롯 영역의 슬롯들을 묶어 관리하는 'Content' 오브젝트가 할당 됨.
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
