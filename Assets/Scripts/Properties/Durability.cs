using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

public class Durability : MonoBehaviour, ISerializable
{
    public int maxValue;
    public int value;

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        //
    }
}
