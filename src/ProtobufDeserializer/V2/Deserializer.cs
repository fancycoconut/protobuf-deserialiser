using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;

namespace ProtobufDeserializer.V2
{
    public class Deserializer
    {
        private readonly byte[] descriptorData;

        public Deserializer(byte[] descriptor)
        {
            descriptorData = descriptor;
        }

        public T Deserialize<T>(byte[] data)
        {
            return (T) Deserialize(data, typeof(T));
        }

        public object Deserialize(byte[] data, Type type)
        {
            var fieldMap = Deserialize(data);
            var instance = ConstructObject(fieldMap, type);

            return instance;
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

        private object GetPropertyValueFromMap(string propertyName, IReadOnlyDictionary<string, object> fieldMap)
        {
            var fieldExists = fieldMap.TryGetValue(propertyName, out var propValue);
            if (fieldExists) return propValue;

            var lowerCasedFieldExists = fieldMap.TryGetValue(propertyName.ToLower(), out propValue);
            if (lowerCasedFieldExists) return propValue;

            var upperCasedFieldExists = fieldMap.TryGetValue(propertyName.ToUpper(), out propValue);
            return upperCasedFieldExists ? propValue : null;
        }

        public Dictionary<string, object> Deserialize(byte[] data)
        {
            using (var input = new CodedInputStream(data))
            {
                var fileDescriptorSet = FileDescriptorSet.Parser.ParseFrom(descriptorData);
                var descriptor = fileDescriptorSet.File[0];

                var fields = ParseMessages(descriptor.MessageType, input).ToList();
                ReadFields(fields);

                return fields.ToDictionary(field => field.Name, field => field.Value);
            }
        }

        private IEnumerable<IField> ParseMessages(IEnumerable<DescriptorProto> messages, CodedInputStream input)
        {
            var fields = new List<IField>();

            // Parse the main message first because we want all the fields to be in order before we start reading the data
            foreach (var message in messages)
            {
                var messageFields = ParseFields(message.Field, input)
                    .OrderBy(f => f.FieldNumber)
                    .ToList();

                fields.AddRange(messageFields);
                foreach (var nestedMessageType in message.NestedType)
                {
                    var nestedFields = ParseFields(nestedMessageType.Field, input)
                        .OrderBy(f => f.FieldNumber)
                        .ToList();

                    fields.AddRange(nestedFields);
                }
            }

            return fields;
        }

        private static IEnumerable<IField> ParseFields(IEnumerable<FieldDescriptorProto> fields, CodedInputStream input)
        {
            return fields.Select(field => FieldFactory.Create(field, input));
        }

        private void ReadFields(IEnumerable<IField> fields)
        {
            foreach (var field in fields)
            {
                field?.ReadValue();
            }
        }
    }
}
