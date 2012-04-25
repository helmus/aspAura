using System.Linq;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

/// <summary>
/// This will remove entityKeys from serializations
/// </summary>
/// <remarks></remarks>
public class ExcludeEntityKeyContractResolver : DefaultContractResolver
{
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
        IList<JsonProperty> properties = base.CreateProperties(type,memberSerialization);
        return properties.Where(p => p.PropertyType != typeof(System.Data.EntityKey)).ToList();
    }
}