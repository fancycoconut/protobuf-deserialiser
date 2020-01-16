using System;
using System.Reflection;

namespace ProtobufDeserializer.Reflection
{
    public interface IMethodInfo
    {
        MethodInfo Get(string name, Type targetType);
    }
}
