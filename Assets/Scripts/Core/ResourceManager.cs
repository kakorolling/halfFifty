using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance = null;
    public Dictionary<int, RuleTile> terrainDic;
    public Dictionary<string, Sprite> spriteDic;

    void Awake()
    {
        //singleton
        if (instance != null) Destroy(gameObject);
        instance = this;
        DontDestroyOnLoad(gameObject);

        //load terrainDic
        terrainDic = new Dictionary<int, RuleTile>();
        RuleTile[] tiles = Resources.LoadAll<RuleTile>("Tile");
        foreach (RuleTile tile in tiles)
        {
            terrainDic.Add(Int32.Parse(tile.name), tile);
        }

        //load spriteDic
        spriteDic = new Dictionary<string, Sprite>();
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprite");
        foreach (Sprite sprite in sprites)
        {
            spriteDic.Add(sprite.name, sprite);
        }
    }
}