using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using ProtobufDeserializer.Helpers;

namespace ProtobufDeserializer
{
    public class Deserializer
    {
        private readonly byte[] descriptorData;
        private readonly Dictionary<string, IField> messageSchema;
        private readonly Dictionary<Type, Queue<PropertyInfo>> propertiesCache;

        public Deserializer(byte[] descriptor)
        {
            descriptorData = descriptor;

            messageSchema = GetMessageSchema();
            propertiesCache = new Dictionary<Type, Queue<PropertyInfo>>();
        }

        /// <summary>
        /// Deserializes a given stream of protobuf based on the provided descriptor
        /// This can be a potentially expensive process especially for nested messages
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> DeserializeToMap(byte[] data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserializes a given stream of protobuf based on the provided descriptor and message name
        /// </summary>
        /// <param name="data"></param>
        /// <param name="messageName"></param>
        /// <returns></returns>
        public Dictionary<string, object> DeserializeToMap(byte[] data, string messageName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserializes a given stream of protobuf based on the provided descriptor into a concrete typed object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public T Deserialize<T>(byte[] data)
        {
            return (T) Deserialize(data, typeof(T));
        }

        /// <summary>
        /// Deserializes a given stream of protobuf based on the provided descriptor into a concrete typed object
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Deserialize(byte[] data, Type type)
        {
            var instance = Activator.CreateInstance(type);
            using (var input = new CodedInputStream(data))
            {
                ReadFields(input, instance, type);
            }

            return instance;
        }

        private void ReadFields(CodedInputStream input, object targetInstance, Type targetInstanceType)
        {
            var propsQueue = GetProperties(targetInstanceType);
            while (propsQueue.Count > 0)
            {
                var prop = propsQueue.Dequeue();
                var tag = input.PeekTag();
                if (tag == 0) break;

                var field = GetFieldReader(tag, prop.Name);
                if (field == null)
                {
                    propsQueue.Enqueue(prop);
                    continue;
                }


                if (prop.PropertyType.IsPrimitive
                    || prop.PropertyType.IsEnum
                    || prop.PropertyType == typeof(string)
                    // TODO Test for decimal since float currently works
                    || prop.PropertyType == typeof(decimal))
                {
                    var propValue = field?.ReadValue(input);
                    prop.SetValue(targetInstance, propValue);
                }
                else
                {
                    // Lists and arrays
                    if (prop.PropertyType.GetInterface(nameof(IEnumerable)) != null)
                    {
                        var list = field?.ReadValue(input);
                        if (prop.PropertyType.IsArray && list != null)
                        {
                            var toArray = list.GetType().GetMethod("ToArray");
                            prop.SetValue(targetInstance, toArray?.Invoke(list, null));
                        }
                        else
                        {
                            prop.SetValue(targetInstance, list);
                        }

                        continue;
                    }

                    // Nested Messages
                    // Before we actually parse fields for a nested message, we need to read off/shave off some extra bytes first
                    field?.ReadValue(input);

                    var instance = Activator.CreateInstance(prop.PropertyType);
                    ReadFields(input, instance, prop.PropertyType);
                    prop.SetValue(targetInstance, instance);
                }
            }

        }

        private Queue<PropertyInfo> GetProperties(Type type)
        {
            if (propertiesCache.TryGetValue(type, out var props)) return props;

            // We cache the queue to avoid generating it everytime... Which makes it blazingly fast...
            props = new Queue<PropertyInfo>(type.GetProperties());
            propertiesCache.Add(type, props);

            return props;
        }

        private IField GetFieldReader(uint tag, string fieldName)
        {
            var key = $"{tag}_{fieldName}";
            if (messageSchema.TryGetValue(key, out var field)) return field;

            // Do we care about these other scenarios
            key = $"{tag}_{fieldName.ToLower()}";
            var lowerCasedFieldExists = messageSchema.TryGetValue(key, out field);
            if (lowerCasedFieldExists) return field;

            key = $"{tag}_{fieldName.ToUpper()}";
            var upperCasedFieldExists = messageSchema.TryGetValue(key, out field);
            return !upperCasedFieldExists ? null : field;
        }

        private Dictionary<string, IField> GetMessageSchema()
        {
            var fileDescriptorSet = FileDescriptorSet.Parser.ParseFrom(descriptorData);
            var descriptor = fileDescriptorSet.File[0];

            return ParseMessage(descriptor.MessageType);
        }

        // Breakthrough!! Soo I think because there is a consistent way of generating a tag therefore this handles duplicated fields as well :)
        private static Dictionary<string, IField> ParseMessage(IEnumerable<DescriptorProto> messages)
        {
            var schema = new Dictionary<string, IField>();

            foreach (var message in messages)
            {
                foreach (var field in message.Field)
                {
                    var tag = ProtobufHelper.ComputeFieldTag(field);
                    AddItemToDictionary($"{tag}_{field.Name}", FieldFactory.Create(message.Name, field), schema);
                }

                foreach (var nestedMessage in message.NestedType)
                {
                    foreach (var field in nestedMessage.Field)
                    {
                        var tag = ProtobufHelper.ComputeFieldTag(field);
                        AddItemToDictionary($"{tag}_{field.Name}", FieldFactory.Create(nestedMessage.Name, field), schema);
                    }
                }
            }

            return schema;
        }

        private static void AddItemToDictionary<TKey, TValue>(TKey key, TValue item, IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary.ContainsKey(key)) return;
            dictionary.Add(key, item);
        }
    }
}
