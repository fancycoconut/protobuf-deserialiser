using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace ProtobufDeserializer.V2
{
    public class Deserializer
    {
        private readonly byte[] descriptorData;
        private readonly Dictionary<string, IField> messageSchema;
        private readonly Dictionary<Type, PropertyInfo[]> propertiesCache;

        public Deserializer(byte[] descriptor)
        {
            descriptorData = descriptor;

            messageSchema = GetMessageSchema();
            propertiesCache = new Dictionary<Type, PropertyInfo[]>();
        }

        public Dictionary<string, object> Deserialize(byte[] data)
        {
            throw new NotImplementedException("Not supported anymore...");
        }

        public T Deserialize<T>(byte[] data)
        {
            return (T) Deserialize(data, typeof(T));
        }

        /// <summary>
        /// For deserialising protobuf you would actually need to know the order of your message and fields because they can always be jumbled up.
        /// Therefore I would work with the assumption that I would deserialise a protobuf message based on the actual provided structure of the provided object
        /// If the field ordering of the provided object is wrong then hard luck because it is close to impossible to know...
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
            var props = GetProperties(targetInstanceType);
            foreach (var prop in props)
            {
                if (prop.PropertyType.IsPrimitive
                    || prop.PropertyType.IsEnum
                    || prop.PropertyType == typeof(string)
                    // TODO Test for decimal since float currently works
                    || prop.PropertyType == typeof(decimal))
                {
                    var propValue = ReadField(prop.Name, input);
                    prop.SetValue(targetInstance, propValue);
                }
                else
                {
                    // Lists and collections
                    if (prop.PropertyType.GetInterface(nameof(IEnumerable)) != null
                        && !prop.PropertyType.IsArray)
                    {
                        var propValue = ReadField(prop.Name, input);
                        prop.SetValue(targetInstance, propValue);
                        continue;
                    }

                    // Arrays
                    if (prop.PropertyType.IsArray)
                    {
                        var list = ReadField(prop.Name, input);
                        var toArray = list.GetType().GetMethod("ToArray");
                        prop.SetValue(targetInstance, toArray?.Invoke(list, null));
                        continue;
                    }

                    // Nested Messages
                    // Before we actually parse fields for a nested message, we need to read off/shave off some extra bytes first
                    ReadField(prop.Name, input);

                    var instance = Activator.CreateInstance(prop.PropertyType);
                    ReadFields(input, instance, prop.PropertyType);
                    prop.SetValue(targetInstance, instance);
                }


                //if (prop.PropertyType.IsClass
                //    && !prop.PropertyType.IsPrimitive)
                //{
                //    //Lists and collections
                //    if (prop.PropertyType.GetInterface(nameof(IEnumerable)) != null
                //        && !prop.PropertyType.IsArray)
                //    {
                //        var propValue = GetPropertyValueFromMap(prop.Name, fieldMap);
                //        prop.SetValue(instance, propValue);
                //        continue;
                //    }

                //    if (prop.PropertyType.IsArray)
                //    {
                //        var list = GetPropertyValueFromMap(prop.Name, fieldMap);
                //        var toArray = list.GetType().GetMethod("ToArray");
                //        prop.SetValue(instance, toArray?.Invoke(list, null));
                //        continue;
                //    }

                //    var messageField =
                //    var classObject = ConstructObject(fieldMap, prop.PropertyType);
                //    prop.SetValue(instance, classObject);
                //}
                //else
                //{
                //    var propValue = ReadField(prop.Name, input);
                //    prop.SetValue(targetInstance, propValue);
                //}
            }
        }

        private IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            if (propertiesCache.TryGetValue(type, out var props)) return props;

            props = type.GetProperties();
            propertiesCache.Add(type, props);

            return props;
        }

        private object ReadField(string fieldName, CodedInputStream input)
        {
            if (messageSchema.TryGetValue(fieldName, out var field)) return field?.ReadValue(input);

            var lowerCasedFieldExists = messageSchema.TryGetValue(fieldName.ToLower(), out field);
            if (lowerCasedFieldExists) return field?.ReadValue(input);

            var upperCasedFieldExists = messageSchema.TryGetValue(fieldName.ToUpper(), out field);
            return !upperCasedFieldExists ? null : field?.ReadValue(input);
        }

        private object ConstructObject(IReadOnlyDictionary<string, object> fieldMap, Type type)
        {
            var instance = Activator.CreateInstance(type);

            var props = type.GetProperties();
            foreach (var prop in props)
            {
                if (prop.PropertyType.IsClass 
                    && !prop.PropertyType.IsPrimitive)
                {
                    // Lists and collections
                    if (prop.PropertyType.GetInterface(nameof(IEnumerable)) != null 
                        && !prop.PropertyType.IsArray)
                    {
                        var propValue = GetPropertyValueFromMap(prop.Name, fieldMap);
                        prop.SetValue(instance, propValue);
                        continue;
                    }

                    if (prop.PropertyType.IsArray)
                    {
                        var list = GetPropertyValueFromMap(prop.Name, fieldMap);
                        var toArray = list.GetType().GetMethod("ToArray");
                        prop.SetValue(instance, toArray?.Invoke(list, null));
                        continue;
                    }

                    var classObject = ConstructObject(fieldMap, prop.PropertyType);
                    prop.SetValue(instance, classObject);
                }
                else
                {
                    var propValue = GetPropertyValueFromMap(prop.Name, fieldMap);
                    prop.SetValue(instance, propValue);
                }
            }

            return instance;
        }

        private static object GetPropertyValueFromMap(string propertyName, IReadOnlyDictionary<string, object> fieldMap)
        {
            var fieldExists = fieldMap.TryGetValue(propertyName, out var propValue);
            if (fieldExists) return propValue;

            // Do we care about these other scenarios?
            var lowerCasedFieldExists = fieldMap.TryGetValue(propertyName.ToLower(), out propValue);
            if (lowerCasedFieldExists) return propValue;

            var upperCasedFieldExists = fieldMap.TryGetValue(propertyName.ToUpper(), out propValue);
            return upperCasedFieldExists ? propValue : null;
        }

        private Dictionary<string, IField> GetMessageSchema()
        {
            var fileDescriptorSet = FileDescriptorSet.Parser.ParseFrom(descriptorData);
            var descriptor = fileDescriptorSet.File[0];

            return ParseMessages(descriptor.MessageType);
        }

        private static Dictionary<string, IField> ParseMessages(IEnumerable<DescriptorProto> messages)
        {
            // Parse the main message first because we want all the fields to be in order before we start reading the data
            var messageSchema = new Dictionary<string, IField>();
            foreach (var message in messages)
            {
                foreach (var field in message.Field)
                {
                    messageSchema.Add(field.Name, FieldFactory.Create(message.Name, field));
                }

                foreach (var nestedMessage in message.NestedType)
                {
                    foreach (var field in nestedMessage.Field)
                    {
                        messageSchema.Add(field.Name, FieldFactory.Create(nestedMessage.Name, field));
                    }
                }
            }

            return messageSchema;
        }
    }
}
