using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using Newtonsoft.Json.Serialization;

[RequireComponent(typeof(Common))]
public abstract class Property : MonoBehaviour
{
    public abstract void SetData(PropertyData propertyData);
    public Common common;
    void Awake()
    {
        common = GetComponent<Common>();
    }
}

[JsonConverter(typeof(PropertyDataConverter))]
public abstract class PropertyData
{
    public string type { get; set; }
}

public class PropertyDataConverter : JsonConverter
{
    static JsonSerializerSettings jss = new JsonSerializerSettings()
    {
        ContractResolver = new PropertyDataSpecifiedConcreteClassConverter()
    };
    public override bool CanConvert(Type objectType) => (objectType == typeof(PropertyData));
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject jObject = JObject.Load(reader);
        Type type = Type.GetType(jObject["type"].ToString());
        return JsonConvert.DeserializeObject(jObject.ToString(), type, jss);
    }
    public override bool CanWrite
    {
        get { return false; }
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}

public class PropertyDataSpecifiedConcreteClassConverter : DefaultContractResolver
{
    protected override JsonConverter ResolveContractConverter(Type objectType)
    {
        if (typeof(PropertyData).IsAssignableFrom(objectType) && !objectType.IsAbstract)
            return null;
        return base.ResolveContractConverter(objectType);
    }
}