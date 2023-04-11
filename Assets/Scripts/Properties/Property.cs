using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Property : MonoBehaviour
{
    public abstract string GetData();
    public abstract void SetData(string json);
}
