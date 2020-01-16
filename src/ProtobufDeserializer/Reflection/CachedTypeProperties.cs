using System;
using System.Collections.Generic;
using System.Reflection;

namespace ProtobufDeserializer.Reflection
{
    public class CachedTypeProperties : ITypeProperties
    {
        private readonly ITypeProperties typeProperties;
        private readonly Dictionary<Type, IEnumerable<PropertyInfo>> propertiesCache;

        public CachedTypeProperties(ITypeProperties typeProperties)
        {
            this.typeProperties = typeProperties;
            propertiesCache = new Dictionary<Type, IEnumerable<PropertyInfo>>();
        }

        public Queue<PropertyInfo> GetQueue(Type type)
        {
            var props = Get(type);
            return new Queue<PropertyInfo>(props);
        }

        public IEnumerable<PropertyInfo> Get(Type type)
        {
            if (propertiesCache.TryGetValue(type, out var props)) return props;

            // We cache the props to avoid reflecting it every time... Which makes it blazingly fast...
            props = typeProperties.Get(type);
            propertiesCache.Add(type, props);

            return props;
        }
    }
}
