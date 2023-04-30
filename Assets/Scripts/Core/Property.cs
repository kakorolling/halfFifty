using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Common))]
public abstract class Property : MonoBehaviour
{
    public Common common;
    void Awake()
    {
        common = GetComponent<Common>();
    }
    public abstract string GetData();
    public abstract void SetData(string json);
}
