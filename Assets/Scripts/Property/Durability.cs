using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class Durability : Property //데이터가 들어갈 실제 본체
{
    static string _type = typeof(Durability).ToString();
    [JsonProperty] public override string type { get => _type; set { } }
    [JsonProperty] int _value { get; set; }
    [JsonProperty] int _maxValue { get; set; }
    public int value //현재 내구도
    {
        get => _value;
        set
        {
            if (this.value == value) return;
            onValueChanged?.Invoke(this.value, value);
            _value = value;
        }
    }
    public int maxValue //최대 내구도
    {
        get => _maxValue;
        set
        {
            if (maxValue == value) return;
            onMaxValueChanged?.Invoke(maxValue, value);
            _maxValue = value;
        }
    }

    public override void OnLoadGo() { }

    public Action<int, int> onValueChanged;
    public Action<int, int> onMaxValueChanged;
}