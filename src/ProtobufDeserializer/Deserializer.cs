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
            var tag = Convert.ToInt32(input.PeekTag());

            var key = $"{tag}_{fieldName}";
            if (messageSchema.TryGetValue(key, out var field)) return field?.ReadValue(input);

            // Do we care about these other scenarios
            key = $"{tag}_{fieldName.ToLower()}";
            var lowerCasedFieldExists = messageSchema.TryGetValue(key, out field);
            if (lowerCasedFieldExists) return field?.ReadValue(input);

            key = $"{tag}_{fieldName.ToUpper()}";
            var upperCasedFieldExists = messageSchema.TryGetValue(key, out field);
            return !upperCasedFieldExists ? null : field?.ReadValue(input);
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
                    AddItemToDictionary($"{ComputeFieldTag(field)}_{field.Name}", FieldFactory.Create(message.Name, field), schema);
                }

                foreach (var nestedMessage in message.NestedType)
                {
                    foreach (var field in nestedMessage.Field)
                    {
                        AddItemToDictionary($"{ComputeFieldTag(field)}_{field.Name}", FieldFactory.Create(nestedMessage.Name, field), schema);
                    }
                }
            }

            return schema;
        }

        private static void AddItemToDictionary<TKey, TValue>(TKey key, TValue item, IDictionary<TKey, TValue> dictionary)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, item);
            }
        }

        private static int ComputeFieldTag(FieldDescriptorProto field)
        {
            // (field_number << 3) | wire_type
            return (field.Number << 3) | GetWireType(field);
        }

        // TODO Refactor/change this....
        private static int GetWireType(FieldDescriptorProto field)
        {
            // Wire Types
            // 0    Varint                  int32, int64, uint32, uint64, sint32, sint64, bool, enum
            // 1    64-bit                  fixed64, sfixed64, double
            // 2	Length-delimited        string, bytes, embedded messages, packed repeated fields
            // 3	Start                   group groups (deprecated)
            // 4	End group               groups (deprecated)
            // 5	32-bit                  fixed32, sfixed32, float

            switch (field.Type)
            {
                case FieldDescriptorProto.Types.Type.Int32:
                case FieldDescriptorProto.Types.Type.Int64:
                case FieldDescriptorProto.Types.Type.Uint32:
                case FieldDescriptorProto.Types.Type.Uint64:
                case FieldDescriptorProto.Types.Type.Sint32:
                case FieldDescriptorProto.Types.Type.Sint64:
                case FieldDescriptorProto.Types.Type.Bool:
                case FieldDescriptorProto.Types.Type.Enum:
                    return field.Label == FieldDescriptorProto.Types.Label.Repeated ? 2 : 0;

                case FieldDescriptorProto.Types.Type.Fixed64:
                case FieldDescriptorProto.Types.Type.Sfixed64:
                case FieldDescriptorProto.Types.Type.Double:
                    return 1;

                case FieldDescriptorProto.Types.Type.String:
                case FieldDescriptorProto.Types.Type.Bytes:
                case FieldDescriptorProto.Types.Type.Message:
                    return 2;

                case FieldDescriptorProto.Types.Type.Sfixed32:
                case FieldDescriptorProto.Types.Type.Fixed32:
                case FieldDescriptorProto.Types.Type.Float:
                    return 5;

                default:
                    throw new ArgumentOutOfRangeException(nameof(field.Type), field.Type, null);
            }
        }

        // TODO Refactor this
        private static Type GetNativeType(FieldDescriptorProto.Types.Type type)
        {
            // Needs refactoring, these types map to the output types of the ReadValue method
            switch (type)
            {
                case FieldDescriptorProto.Types.Type.Int32:
                    return typeof(int);
                case FieldDescriptorProto.Types.Type.Int64:
                    return typeof(long);
                case FieldDescriptorProto.Types.Type.Uint32:
                    return typeof(uint);
                case FieldDescriptorProto.Types.Type.Uint64:
                    return typeof(ulong);
                case FieldDescriptorProto.Types.Type.Sint32:
                    return typeof(int);
                case FieldDescriptorProto.Types.Type.Sint64:
                    return typeof(long);
                case FieldDescriptorProto.Types.Type.Bool:
                    return typeof(bool);
                case FieldDescriptorProto.Types.Type.Enum:
                    return typeof(int);
                case FieldDescriptorProto.Types.Type.Fixed64:
                    return typeof(ulong);
                case FieldDescriptorProto.Types.Type.Sfixed64:
                    return typeof(long);
                case FieldDescriptorProto.Types.Type.Double:
                    return typeof(double);
                case FieldDescriptorProto.Types.Type.String:
                    return typeof(string);
                case FieldDescriptorProto.Types.Type.Bytes:
                    return typeof(byte[]);
                case FieldDescriptorProto.Types.Type.Message:
                    return typeof(object);
                case FieldDescriptorProto.Types.Type.Sfixed32:
                    //return typeof()
                case FieldDescriptorProto.Types.Type.Fixed32:
                case FieldDescriptorProto.Types.Type.Float:
                    //return 5;

                default:
                    throw new Exception(); //ArgumentOutOfRangeException(nameof(field.Type), field.Type, null);
            }
        }

    }
}
