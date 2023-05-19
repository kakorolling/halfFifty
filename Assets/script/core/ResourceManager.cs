using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance = null;
    public Dictionary<string, Sprite> spriteDic;

    void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
        DontDestroyOnLoad(gameObject);

        spriteDic = new Dictionary<string, Sprite>();
        Sprite[] sprites = Resources.LoadAll<Sprite>("sprite");
        foreach (Sprite sprite in sprites)
        {
            Debug.Log(sprite.name);
            spriteDic.Add(sprite.name, sprite);
        }
    }
}
