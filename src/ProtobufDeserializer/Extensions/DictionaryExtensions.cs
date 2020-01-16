using System.Collections.Generic;

namespace ProtobufDeserializer.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddIfNotExists<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key)) return;
            dictionary.Add(key, value);
        }
    }
}
