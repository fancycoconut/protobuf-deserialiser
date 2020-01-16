using System;
using System.Collections.Generic;
using System.Reflection;

namespace ProtobufDeserializer.Reflection
{
    public class CachedTypeProperties : ITypeProperties
    {
        private readonly ITypeProperties typeProperties;
        private readonly Dictionary<Type, Queue<PropertyInfo>> propertiesCache;

        public CachedTypeProperties(ITypeProperties typeProperties)
        {
            this.typeProperties = typeProperties;
            propertiesCache = new Dictionary<Type, Queue<PropertyInfo>>();
        }

        public Queue<PropertyInfo> GetQueue(Type type)
        {
            if (propertiesCache.TryGetValue(type, out var props)) return props;

            // We cache the queue to avoid generating it every time... Which makes it blazingly fast...
            props = typeProperties.GetQueue(type);
            propertiesCache.Add(type, props);

            return props;
        }

        public IEnumerable<PropertyInfo> Get(Type type)
        {
            // TODO Cache this...
            return typeProperties.Get(type);
        }
    }
}
