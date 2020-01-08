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
                    && prop.PropertyType != typeof(string))
                {
                    var val = ConstructObject(fieldMap, prop.PropertyType);
                    prop.SetValue(instance, val);
                }
                else
                {
                    fieldMap.TryGetValue(prop.Name, out object propValue);
                    prop.SetValue(instance, propValue);
                }
            }

            return instance;
        }

        public Dictionary<string, object> Deserialize(byte[] data)
        {
            var input = new CodedInputStream(data);
            var fileDescriptorSet = FileDescriptorSet.Parser.ParseFrom(descriptorData);
            var descriptor = fileDescriptorSet.File[0];

            var fields = ParseFields(descriptor.MessageType, input).ToList();
            ReadFields(fields);

            return fields.ToDictionary(field => field.Name, field => field.Value);
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
