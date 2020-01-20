using System;
using System.Collections.Generic;
using System.Reflection;

namespace ProtobufDeserializer.Reflection
{
    public interface ITypeProperties
    {
        LinkedList<PropertyInfo> GetList(Type type);

        PropertyInfo[] Get(Type type);
    }
}
