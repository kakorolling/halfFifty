using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Graphic : Property
{
    struct Data
    {
        public string spriteId;
    }
    Data data;
    SpriteRenderer sR;

    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
        sR.sprite = ResourceManager.instance.spriteDic[data.spriteId];
    }

    public override string GetData() { return JsonUtility.ToJson(data); }
    public override void SetData(string json) { data = JsonUtility.FromJson<Data>(json); }
}
