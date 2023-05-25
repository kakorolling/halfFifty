using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 참고 https://ansohxxn.github.io/unity%20lesson%203/ch6-1/
public class QuickSlotController : MonoBehaviour
{
    [SerializeField] private Slot[] quickSlots; //퀵슬롯 8개
    [SerializeField] private Transform tfParent; //퀵슬롯의 부모 오브젝트

    private int selectedSlot; //선택된 퀵슬롯의 인덱스(0~7)
    [SerializeField] private GameObject SelectedQuickSlotImage; //선택된 퀵슬롯 이미지

    
    void Start()
    {
        quickSlots = tfParent.GetComponentsInChildren<Slot>();
        selectedSlot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        TryInputNumber();
    }
    private void ChangeSlot(int _num)
    {
        SelectedSlot(_num);
    }
    private void SelectedSlot(int _num)
    {
        selectedSlot = _num; //선택된 슬롯
        
    }

    private void TryInputNumber()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeSlot(0);
            Debug.Log("1 동작 확인");
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeSlot(1);
            Debug.Log("2 동작 확인");
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeSlot(2);
            Debug.Log("3 동작 확인");
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeSlot(3);
            Debug.Log("4 동작 확인");
        }
        else if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeSlot(4);
            Debug.Log("5 동작 확인");
        }
        else if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangeSlot(5);
            Debug.Log("6 동작 확인");
        }
        else if(Input.GetKeyDown(KeyCode.Alpha7))
        {
            ChangeSlot(6);
            Debug.Log("7 동작 확인");
        }
    }
}
