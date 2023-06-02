using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using Newtonsoft.Json.Serialization;

[JsonConverter(typeof(PropertyConverter))]
public abstract class Property
{
    public abstract string type { get; set; }
    [JsonProperty] internal Guid _objId { get; set; }
    Obj _obj;
    public Obj obj
    {
        get
        {
            if (_obj == null) _obj = GameManager.instance.GetObj(_objId);
            return _obj;
        }
        set
        {
            _objId = value.id;
            _obj = value;
        }
    }
    public GameObject go { get => obj.go; }
    public abstract void OnLoadGo();
}

public class PropertyConverter : JsonConverter
{
    static JsonSerializerSettings jss = new JsonSerializerSettings()
    {
        ContractResolver = new PropertySpecifiedConcreteClassConverter()
    };
    public override bool CanConvert(Type objectType) => (objectType == typeof(Property));
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

public class PropertySpecifiedConcreteClassConverter : DefaultContractResolver
{
    protected override JsonConverter ResolveContractConverter(Type objectType)
    {
        if (typeof(Property).IsAssignableFrom(objectType) && !objectType.IsAbstract)
            return null;
        return base.ResolveContractConverter(objectType);
    }
}