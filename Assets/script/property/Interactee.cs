using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Interactee : Property
{
    struct Data
    {
        //상호작용에대한 행동의 데이터가 있어야함
    }
    Data data;

    public void Interact()
    {

        OnInteracted();
    }

    public override string GetData() { return JsonUtility.ToJson(data); }
    public override void SetData(string json) { data = JsonUtility.FromJson<Data>(json); }

    public Action onInteracted;
    void OnInteracted() { onInteracted.Invoke(); }
}
