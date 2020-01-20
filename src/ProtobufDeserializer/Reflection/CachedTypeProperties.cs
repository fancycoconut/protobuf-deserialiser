using System;
using System.Collections.Generic;
using System.Reflection;

namespace ProtobufDeserializer.Reflection
{
    public class CachedTypeProperties : ITypeProperties
    {
        private readonly ITypeProperties typeProperties;
        private readonly Dictionary<Type, PropertyInfo[]> propertiesCache;

        private readonly Dictionary<Type, LinkedList<PropertyInfo>> propertiesListCache;

        public CachedTypeProperties(ITypeProperties typeProperties)
        {
            this.typeProperties = typeProperties;
            propertiesCache = new Dictionary<Type, PropertyInfo[]>();

            propertiesListCache = new Dictionary<Type, LinkedList<PropertyInfo>>();
        }

        public LinkedList<PropertyInfo> GetList(Type type)
        {
            if (propertiesListCache.TryGetValue(type, out var props)) return props;

            // We cache the props to avoid reflecting it every time... Which makes it blazingly fast...
            props = typeProperties.GetList(type);
            propertiesListCache.Add(type, props);

            return props;
        }

        public PropertyInfo[] Get(Type type)
        {
            if (propertiesCache.TryGetValue(type, out var props)) return props;

            // We cache the props to avoid reflecting it every time... Which makes it blazingly fast...
            props = typeProperties.Get(type);
            propertiesCache.Add(type, props);

            return props;
        }
    }
}
