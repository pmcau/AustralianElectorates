using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
}