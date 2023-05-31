using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Dynamic;
using Newtonsoft.Json;

public class Durability : Property //데이터가 들어갈 실제 본체
{
    DurabilityData data;
    public override void SetData(PropertyData propertyData) { data = (DurabilityData)propertyData; }

    public int value //현재 내구도
    {
        get => data.value;
        set
        {
            if (this.value == value) return;
            onValueChanged?.Invoke(this.value, value);
            data.value = value;
        }
    }
    public int maxValue //최대 내구도
    {
        get => data.maxValue;
        set
        {
            if (maxValue == value) return;
            onMaxValueChanged?.Invoke(maxValue, value);
            data.maxValue = value;
        }
    }

    public Action<int, int> onValueChanged;
    public Action<int, int> onMaxValueChanged;
}
public class DurabilityData : PropertyData
{
    public int value { get; set; }
    public int maxValue { get; set; }
}