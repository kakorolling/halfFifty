using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemManager : MonoBehaviour
{
    public abstract void DestroyAferTime(); //시간이 지난 후 파괴한다
    public abstract void UseItem(); // 아이템 사용
}
