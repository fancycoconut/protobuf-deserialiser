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

        public LinkedList<PropertyInfo> GetList(Type type)
        {
            return new LinkedList<PropertyInfo>(type.GetProperties());
        }

        public PropertyInfo[] Get(Type type)
        {
            return type.GetProperties();
        }
    }
}
