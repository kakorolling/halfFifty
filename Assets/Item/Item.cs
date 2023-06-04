using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    //아이템의 이름
    private string Name;

    //아이템의 수량
    private int amount;

    //아이템의 이미지의 이름(이미지를 보이게 할때 필요)
    private string imageName;

    //아이템이 인벤토리의 몇번째 슬롯에 있는지 나타내는 숫자
    //private int slotnumber;

    //아이템의 종류
    private string itemType;

    //아이템이 인벤토리에 위치하는 슬롯 번호
    private int itemSlotNumber;

    //이외에도 내구도와 같은 프로퍼티를 여기에 추가로 생성가능

    //생성자 (아이템을 new로 생성할때 new Item(itemName, 1, itemImage.sprite.name) 이런식으로 생성 가능)
    public Item(string Name, int amount, string imageName, string itemType)
    {
        this.Name = Name;
        this.amount = amount;
        this.imageName = imageName;
        this.itemType = itemType;
    }

    //아이템의 이름 가져올때 사용
    public string GetName()
    {
        return Name;
    }
    //아이템의 이름 지정할때 사용
    public void SetName(string newName)
    {
        Name = newName;
    }

    //아이템의 수량 가져올때 사용
    public int GetAmount()
    {
        return amount;
    }
    //아이템의 수량 지정할때 사용
    public void SetAmount(int newAmount)
    {
        amount = newAmount;
    }

    //아이템의 이미지의 이름 가져올때 사용
    public string GetImageName()
    {
        return imageName;
    }
    //아이템의 이미지의 이름 지정할때 사용
    public void SetImageName(string newImageName)
    {
        imageName = newImageName;
    }

    //아이템의 종류 가져올때 사용
    public string GetItemType()
    {
        return itemType;
    }
    //아이템의 종류 지정할때 사용
    public void SetItemType(string newitemType)
    {
        itemType = newitemType;
    }

    public int GetSlotNumber()
    {
        return itemSlotNumber;
    }
    //아이템의 종류 지정할때 사용
    public void SetSlotNumber(int newitemSlotNumber)
    {
        itemSlotNumber = newitemSlotNumber;
    }
}
