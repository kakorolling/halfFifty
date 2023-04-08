using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream:Assets/Scripts/Properties/DurabilityData.cs
using System;

[Serializable]
public class DurabilityData
=======
using System.Runtime.Serialization;
public class Durability : MonoBehaviour, ISerial
>>>>>>> Stashed changes:Assets/Scripts/Properties/Durability.cs
{
    public int maxValue;
    public int value;
}
