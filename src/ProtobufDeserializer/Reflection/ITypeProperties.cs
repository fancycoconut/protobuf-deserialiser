using System;
using System.Collections.Generic;
using System.Reflection;

namespace ProtobufDeserializer.Reflection
{
    public interface ITypeProperties
    {
        Queue<PropertyInfo> GetQueue(Type type);
        IEnumerable<PropertyInfo> Get(Type type);
    }
}
