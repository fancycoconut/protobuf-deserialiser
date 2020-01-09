using System;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using ProtobufDeserializer.Fields;

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

        private object ConstructObject(Dictionary<string, object> fieldMap, Type type)
        {
            var instance = Activator.CreateInstance(type);

            var props = type.GetProperties();
            foreach (var prop in props)
            {
                if (prop.PropertyType.IsClass
                    && prop.PropertyType.IsGenericType
                    && prop.PropertyType.Namespace == "System.Collections.Generic")
                {
                    var propValue = GetPropertyValueFromMap(prop.Name, fieldMap);
                    prop.SetValue(instance, propValue);
                }
                else if (prop.PropertyType.IsClass 
                    && prop.PropertyType != typeof(string))
                {
                    var classObject = ConstructObject(fieldMap, prop.PropertyType);
                    prop.SetValue(instance, classObject);
                }
                else
                {
                    //var fieldExists = fieldMap.TryGetValue(prop.Name, out var propValue);
                    //if (!fieldExists)
                    //{
                    //    fieldMap.TryGetValue(prop.Name.ToLower(), out propValue);
                    //}

                    var propValue = GetPropertyValueFromMap(prop.Name, fieldMap);
                    prop.SetValue(instance, propValue);
                }
            }

            return instance;
        }

        private object GetPropertyValueFromMap(string propertyName, Dictionary<string, object> fieldMap)
        {
            // TODO: Refactor this....
            var fieldExists = fieldMap.TryGetValue(propertyName, out var propValue);
            if (!fieldExists)
            {
                var lowerCasedFieldExists = fieldMap.TryGetValue(propertyName.ToLower(), out propValue);
                if (!lowerCasedFieldExists)
                {
                    var upperCasedFieldExists = fieldMap.TryGetValue(propertyName.ToUpper(), out propValue);
                    if (!upperCasedFieldExists) return null;

                    return propValue;
                }

                return propValue;
            }

            return propValue;
        }

        public Dictionary<string, object> Deserialize(byte[] data)
        {
            using (var input = new CodedInputStream(data))
            {
                var fileDescriptorSet = FileDescriptorSet.Parser.ParseFrom(descriptorData);
                var descriptor = fileDescriptorSet.File[0];

                var fields = ParseFields(descriptor.MessageType, input).ToList();
                ReadFields(fields);

                return fields.ToDictionary(field => field.Name, field => field.Value);
            }
        }

        private IEnumerable<IField> ParseFields(IEnumerable<DescriptorProto> messages, CodedInputStream input)
        {
            foreach (var messageType in messages)
            {
                foreach (var field in messageType.Field)
                {
                    yield return FieldFactory.Create(field, input);
                }

                foreach (var nestedMessageType in messageType.NestedType)
                {
                    foreach (var field in nestedMessageType.Field)
                    {
                        yield return FieldFactory.Create(field, input);
                    }
                }
            }
        }

        private void ReadFields(IEnumerable<IField> fields)
        {
            foreach (var field in fields)
            {
                field.ReadValue();
            }
        }
    }
}
