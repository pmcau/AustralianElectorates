using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ObjectApproval;

public class CustomContractResolver : DefaultContractResolver
{
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
        var jsonProperties = base.CreateProperties(type, memberSerialization);
        var first = jsonProperties.FirstOrDefault(x => x.PropertyName == "properties");
        if (first == null)
        {
            return jsonProperties;
        }

        var properties = new List<JsonProperty> {first};
        properties.AddRange(jsonProperties.Where(x => x.PropertyName != "properties"));
        return properties;
    }

    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var property = base.CreateProperty(member, memberSerialization);
        property.SkipEmptyCollections(member);
        return property;
    }
}