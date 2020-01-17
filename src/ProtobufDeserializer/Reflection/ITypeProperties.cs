using System;
using System.Collections.Generic;
using System.Reflection;

namespace ProtobufDeserializer.Reflection
{
    public interface ITypeProperties
    {
        Queue<PropertyInfo> GetQueue(Type type);
        LinkedList<PropertyInfo> GetList(Type type);

        PropertyInfo[] Get(Type type);
    }
}
