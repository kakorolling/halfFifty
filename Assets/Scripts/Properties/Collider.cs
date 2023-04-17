using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Collider : Property
{
    public Vector2Int[] points;

    public override string GetData()
    {
        Data data = new Data();
        data.points = points;
        string json = JsonUtility.ToJson(data);
        return json;
    }
    public override void SetData(string json)
    {
        Data data = JsonUtility.FromJson<Data>(json);
        points = data.points;
    }
    public struct Data
    {
        public Vector2Int[] points;
    }
}



