using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDragAndDrop : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Image image;
    public Text text;
    public int slotnumber;
    public int otherItemslotnumber;

    private Transform originalParent;
    private Vector3 originalPosition;
    private bool isDragging = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 기존의 부모와 위치 저장
        originalParent = transform.parent;
        originalPosition = transform.position;

        if(!(image.sprite.name == "Transparent"))
        {   
            foreach(Item ItemInInventory in InventoryManager.inventoryItems)
            {
                if(ItemInInventory.GetImageName() == image.sprite.name)
                {
                    slotnumber = ItemInInventory.GetSlotNumber();
                }
            }
        }
        else if(image.sprite.name == "Transparent")
        {
            return;
        }

        // 드래그 중인지 플래그 설정
        isDragging = true;

        // 드래그 중인 아이템을 가장 위로 올림
        //transform.SetAsLastSibling();

        // 드래그 시 다른 UI 이벤트를 막기 위해 레이캐스트 타겟 비활성화
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 드래그 중인 경우에만 위치 업데이트
        if (isDragging)
        {
            transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 드롭된 오브젝트 가져오기
        GameObject droppedObject = eventData.pointerCurrentRaycast.gameObject;
        
        if((droppedObject.name != "SlotImage") && (droppedObject.name != "QuickSlotImage"))
        {
            transform.position = originalPosition;
            transform.SetParent(originalParent);
            image.raycastTarget = true;
        }
        
        // 드롭된 오브젝트에 ItemDragAndDrop 컴포넌트가 있는지 확인
        ItemDragAndDrop otherItem = droppedObject.GetComponent<ItemDragAndDrop>();

        if(!(otherItem.image.sprite.name == "Transparent"))
        {   
            foreach(Item ItemInInventory in InventoryManager.inventoryItems)
            {
                if(ItemInInventory.GetImageName() == otherItem.image.sprite.name)
                {
                    otherItemslotnumber = ItemInInventory.GetSlotNumber();
                }
            }
        }
        

        // 다른 아이템과 교환
        if (otherItem != null && otherItem != this)
        {
            // 이미지와 텍스트 교환
            Sprite tempImage = image.sprite;
            string tempText = text.text;
            int tempSlotnumber = slotnumber;

            image.sprite = otherItem.image.sprite;
            text.text = otherItem.text.text;

            
            if(otherItem.image.sprite.name != "Transparent")
            {
                foreach(Item ItemInInventory in InventoryManager.inventoryItems)
                {
                    if(ItemInInventory.GetImageName() == tempImage.name)
                    {
                        ItemInInventory.SetSlotNumber(otherItemslotnumber);
                    }
                }

                foreach(Item ItemInInventory in InventoryManager.inventoryItems)
                {
                    if(ItemInInventory.GetImageName() == otherItem.image.sprite.name)
                    {
                        ItemInInventory.SetSlotNumber(slotnumber);
                    }
                }

                foreach(Item ItemInInventory in InventoryManager.inventoryItems)
                {
                    
                    //Debug.Log(ItemInInventory.GetSlotNumber());
                    
                }
            }
            else if(otherItem.image.sprite.name == "Transparent")
            {
                Transform parent = droppedObject.transform.parent;
                Debug.Log(parent.gameObject.name);

                string parentSlotnumber = parent.gameObject.name.Substring(4);
                otherItemslotnumber = int.Parse(parentSlotnumber);
                
                foreach(Item ItemInInventory in InventoryManager.inventoryItems)
                {
                    if(ItemInInventory.GetImageName() == tempImage.name)
                    {
                        ItemInInventory.SetSlotNumber(otherItemslotnumber);
                    }
                }
            }
            
            otherItem.image.sprite = tempImage;
            otherItem.text.text = tempText;
        }


        // 드래그가 끝나면 원래 위치로 되돌리기
        transform.position = originalPosition;
        transform.SetParent(originalParent);

        // 드래그가 끝나면 레이캐스트 타겟 다시 활성화
        image.raycastTarget = true;

        // 드래그 중인 플래그 해제
        isDragging = false;

        //InventoryManager.ShowItem();
    }
}
