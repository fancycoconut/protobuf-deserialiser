using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace ProtobufDeserializer
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

        public IEnumerable<Type> GetMessageTypes()
        {
            var fileDescriptorSet = FileDescriptorSet.Parser.ParseFrom(descriptorData);
            var descriptor = fileDescriptorSet.File[0];

            var types = new List<Type>();
            foreach (var message in descriptor.MessageType)
            {
                //var typeBuilder = new MessageTypeFactory(message);

                foreach (var nestedMessage in message.NestedType)
                {

                }
            }

            return types;
        }

        //private Type GetAnonymousType()
        //{

        //}

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
                    // Lists and arrays
                    if (prop.PropertyType.GetInterface(nameof(IEnumerable)) != null)
                    {
                        var list = ReadField(prop.Name, input);
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
                    ReadField(prop.Name, input);

                    var instance = Activator.CreateInstance(prop.PropertyType);
                    ReadFields(input, instance, prop.PropertyType);
                    prop.SetValue(targetInstance, instance);
                }
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

            // Do we care about these other scenarios
            var lowerCasedFieldExists = messageSchema.TryGetValue(fieldName.ToLower(), out field);
            if (lowerCasedFieldExists) return field?.ReadValue(input);

            var upperCasedFieldExists = messageSchema.TryGetValue(fieldName.ToUpper(), out field);
            return !upperCasedFieldExists ? null : field?.ReadValue(input);
        }

        private Dictionary<string, IField> GetMessageSchema()
        {
            var fileDescriptorSet = FileDescriptorSet.Parser.ParseFrom(descriptorData);
            var descriptor = fileDescriptorSet.File[0];

            return ParseMessage(descriptor.MessageType);
        }

        private static Dictionary<string, IField> ParseMessage(IEnumerable<DescriptorProto> messages)
        {
            var schema = new Dictionary<string, IField>();

            foreach (var message in messages)
            {
                foreach (var field in message.Field)
                {
                    schema.Add(field.Name, FieldFactory.Create(message.Name, field));
                }

                foreach (var nestedMessage in message.NestedType)
                {
                    foreach (var field in nestedMessage.Field)
                    {
                        schema.Add(field.Name, FieldFactory.Create(nestedMessage.Name, field));
                    }
                }
            }

            return schema;
        }
    }
}
