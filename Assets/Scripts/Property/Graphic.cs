using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Dynamic;
using Newtonsoft.Json;

[RequireComponent(typeof(SpriteRenderer))]
public class Graphic : Property
{
    GraphicData data;
    Animation animManager;
    public override void SetData(PropertyData propertyData) { data = (GraphicData)propertyData; }

    public string spriteId
    {
        get => data.spriteId;
    }

    SpriteRenderer sR;

    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
        sR.sprite = ResourceManager.instance.spriteDic[spriteId];
    }

    void addAnimation()
    {

    }


}
public class GraphicData : PropertyData
{
    public string spriteId { get; set; }
}