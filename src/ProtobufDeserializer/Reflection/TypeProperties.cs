using System;
using System.Collections.Generic;
using System.Reflection;

namespace ProtobufDeserializer.Reflection
{
    public class TypeProperties : ITypeProperties
    {
        public Queue<PropertyInfo> GetQueue(Type type)
        {
            return new Queue<PropertyInfo>(type.GetProperties());
        }

        public IEnumerable<PropertyInfo> Get(Type type)
        {
            return type.GetProperties();
        }
    }
}
