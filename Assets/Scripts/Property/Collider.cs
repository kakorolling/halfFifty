using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class Collider : Property
{
    static string _type = typeof(Collider).ToString();
    [JsonProperty] public override string type { get => _type; set { } }
    [JsonProperty] Vector2Int[] _pointArr { get; set; }
    public Vector2Int[] pointArr
    {
        get => _pointArr;
    }

    public override void OnLoadGo() { }

    public Action onCollisionEnter;
    public Action onCollisionStay;
    public Action onCollisionExit;
}