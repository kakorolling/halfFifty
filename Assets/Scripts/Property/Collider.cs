using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Dynamic;
using Newtonsoft.Json;
public class Collider : Property
{
    ColliderData data;
    public override void SetData(PropertyData propertyData) { data = (ColliderData)propertyData; }

    public Vector2Int[] pointArr
    {
        get => data.pointArr;
    }

    public Action onCollisionEnter;
    public Action onCollisionStay;
    public Action onCollisionExit;
}

public class ColliderData : PropertyData
{
    public Vector2Int[] pointArr { get; set; }
}



