using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Durability : Property //데이터가 들어갈 실제 본체
{
    public int maxValue;
    public int value;

    public override string GetData()
    {
        Data data = new Data();
        data.maxValue = maxValue;
        data.value = value;
        string json = JsonUtility.ToJson(data);
        return json;
    }
    public override void SetData(string json)
    {
        Data data = JsonUtility.FromJson<Data>(json);
        maxValue = data.maxValue;
        value = data.value;
    }
    public struct Data //데이터 덩어리 -> Durability 클래스에 끼워넣어야함
    {
        public int maxValue;
        public int value;
    }
}