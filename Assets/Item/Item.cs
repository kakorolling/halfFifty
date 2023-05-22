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

    //이외에도 내구도와 같은 프로퍼티를 여기에 추가로 생성가능

    //생성자 (아이템을 new로 생성할때 new Item(itemName, 1, itemImage.sprite.name) 이런식으로 생성 가능)
    public Item(string Name, int amount, string imageName)
    {
        this.Name = Name;
        this.amount = amount;
        this.imageName = imageName;
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
}
