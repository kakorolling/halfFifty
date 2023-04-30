using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Durability : Property //데이터가 들어갈 실제 본체
{
    struct Data //데이터 덩어리 -> Durability 클래스에 끼워넣어야함
    {
        public int maxValue;
        public int value;
    }

    Data data;
    public int value //현재 내구도
    {
        get { return data.value; }
        set
        {
            if (data.value == value) return;
            OnValueChanged(data.value, value);
            data.value = value;
        }
    }
    public int maxValue //최대 내구도
    {
        get { return data.maxValue; }
        set
        {
            if (data.maxValue == value) return;
            OnMaxValueChanged(data.maxValue, value);
            data.maxValue = value;
        }
    }

    public override string GetData() { return JsonUtility.ToJson(data); }
    public override void SetData(string json) { data = JsonUtility.FromJson<Data>(json); }

    public Action<int, int> onValueChanged;
    public Action<int, int> onMaxValueChanged;
    void OnValueChanged(int prev, int next)
    {
        if (onValueChanged != null) onValueChanged.Invoke(prev, next);
    }
    void OnMaxValueChanged(int prev, int next)
    {
        if (onMaxValueChanged != null) onMaxValueChanged.Invoke(prev, next);
    }
}