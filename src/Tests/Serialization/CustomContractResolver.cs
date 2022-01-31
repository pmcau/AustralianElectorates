﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

public class CustomContractResolver :
    DefaultContractResolver
{
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
        var jsonProperties = base.CreateProperties(type, memberSerialization);
        var first = jsonProperties.FirstOrDefault(x => x.PropertyName == "properties");
        if (first == null)
        {
            return jsonProperties;
        }

        List<JsonProperty> properties = new() { first };
        properties.AddRange(jsonProperties.Where(x => x.PropertyName != "properties"));
        return properties;
    }

    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var property = base.CreateProperty(member, memberSerialization);
        property.ShouldSerialize = o =>
        {
            if (o is ICollection collection)
            {
                return collection.Count > 0;
            }

            return true;
        };
        return property;
    }
}