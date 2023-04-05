using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotController : MonoBehaviour
{
    [SerializeField] private QuickSlotController[] quickSlots; //퀵슬롯 8개
    [SerializeField] private Transform tfParent; //퀵슬롯의 부모 오브젝트

    private int selectedSlot; //선택된 퀵슬롯의 인덱스(0~7)
    [SerializeField] private GameObject SelectedQuickSlotImage; //선택된 퀵슬롯 이미지

    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
