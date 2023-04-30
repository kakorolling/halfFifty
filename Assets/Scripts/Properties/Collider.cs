using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Collider : Property
{
    struct Data
    {
        public Vector2Int[] points;
    }
    Data data;
    public Vector2Int[] points;

    public override string GetData() { return JsonUtility.ToJson(data); }
    public override void SetData(string json) { data = JsonUtility.FromJson<Data>(json); }

    public Action onCollisionEnter;
    public Action onCollisionStay;
    public Action onCollisionExit;
    void OnCollisionEnter()
    {
        if (onCollisionEnter != null) onCollisionEnter.Invoke();
    }
    void OnCollisionStay()
    {
        if (onCollisionStay != null) onCollisionStay.Invoke();
    }
    void OnCollisionExit()
    {
        if (onCollisionExit != null) onCollisionExit.Invoke();
    }
}



