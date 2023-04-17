using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Common : Property
{
    public int id;
    public Vector2Int center;
    public Vector2Int[] meshPoints;

    public override string GetData()
    {
        Data data = new Data();
        data.id = id;
        data.center = center;
        data.meshPoints = meshPoints;
        string json = JsonUtility.ToJson(data);
        return json;
    }
    public override void SetData(string json)
    {
        Data data = JsonUtility.FromJson<Data>(json);
        id = data.id;
        center = data.center;
        meshPoints = data.meshPoints;
    }
    public struct Data
    {
        public int id;
        public Vector2Int center;
        public Vector2Int[] meshPoints;
    }
}



