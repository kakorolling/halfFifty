using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class Graphic : Property
{
    static string _type = typeof(Graphic).ToString();
    [JsonProperty] public override string type { get => _type; set { } }
    [JsonProperty] string _spriteId { get; set; }
    public static Graphic Create(string spriteId)
    {
        Graphic graphic = new Graphic();
        graphic._spriteId = spriteId;
        return graphic;
    }
    public string spriteId
    {
        get => _spriteId;
    }

    SpriteRenderer sR;
    Animation animManager;

    void addAnimation()
    {

    }

    public override void OnLoadGo()
    {
        sR = go.AddComponent<SpriteRenderer>();
        sR.sprite = ResourceManager.instance.spriteDic[spriteId];
    }
}