﻿using System;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace ProtobufDeserialiser
{
    public class Deserializer
    {
        public static T Deserialize<T>(byte[] descriptorData, byte[] data)
        {
            return (T) Deserialize(descriptorData, data, typeof(T));
        }

        public static object Deserialize(byte[] descriptorData, byte[] data, Type type)
        {
            var objectMap = Deserialize(descriptorData, data);
            var instance = Activator.CreateInstance(type);

            var props = type.GetProperties();
            foreach (var prop in props)
            {
                objectMap.TryGetValue(prop.Name, out object propValue);
                prop.SetValue(instance, propValue);
            }

            return instance;
        }

        public static Dictionary<string, object> Deserialize(byte[] descriptorData, byte[] data)
        {
            var fileDescriptorSet = Google.Protobuf.Reflection.FileDescriptorSet.Parser.ParseFrom(descriptorData);
            var descriptor = fileDescriptorSet.File[0];

            var message = descriptor.MessageType[0];
            var fields = message.Field.Select(x => new FieldInfo
            {
                Name = x.Name,
                Number = x.Number,
                Type = x.Type
            }).ToList();

            var objectMap = new Dictionary<string, object>();
            var input = new CodedInputStream(data);

            // Parse data
            foreach (var field in fields)
            {
                object val = GetDefaultValue(field.Type);
                var tag = input.ReadTag();

                if (tag != 0 && !input.IsAtEnd)
                {
                    switch (field.Type)
                    {
                        case FieldDescriptorProto.Types.Type.Int32:
                            val = input.ReadInt32();
                            break;
                        case FieldDescriptorProto.Types.Type.String:
                            val = input.ReadString();
                            break;
                        case FieldDescriptorProto.Types.Type.Enum:
                            val = input.ReadEnum();
                            break;
                        default:
                            // No field mapping found then skip
                            break;
                    }
                }

                objectMap.Add(field.Name, val);
            }
            
            return objectMap;
        }

        private static object GetDefaultValue(FieldDescriptorProto.Types.Type type)
        {
            switch (type)
            {
                case FieldDescriptorProto.Types.Type.Int32:
                    return 0;
                case FieldDescriptorProto.Types.Type.String:
                    return string.Empty;
                case FieldDescriptorProto.Types.Type.Enum:
                    return 0;
                default:
                    return null;
            }
        }
    }
}