using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Id : Property
{
    public int value;
    public override string GetData()
    {
        IdData data = new IdData();
        data.value = value;
        string json = JsonUtility.ToJson(data);
        return json;
    }

    public override void SetData(string json)
    {
        IdData data = JsonUtility.FromJson<IdData>(json);
        value = data.value;
    }
}

public class IdData
{
    public int value;
}