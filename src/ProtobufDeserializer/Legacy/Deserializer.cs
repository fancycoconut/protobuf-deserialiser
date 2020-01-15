using System;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using ProtobufDeserializer.Legacy.V1;

namespace ProtobufDeserializer.Legacy
{
    [Obsolete("This implementation is deprecated")]
    public class Deserializer
    {
        public static T Deserialize<T>(byte[] descriptorData, byte[] data)
        {
            return (T) Deserialize(descriptorData, data, typeof(T));
        }

        public static object Deserialize(byte[] descriptorData, byte[] data, Type type)
        {
            var messageMap = Deserialize(descriptorData, data);
            var objectMap = messageMap[type.Name].ObjectMap;
            var instance = Activator.CreateInstance(type);

            var props = type.GetProperties();
            foreach (var prop in props)
            {
                objectMap.TryGetValue(prop.Name, out object propValue);
                prop.SetValue(instance, propValue);
            }

            return instance;
        }

        // Pseudo code
        // Step 1 Break down all the message types into an object map with all the fields (including all nested types)
        // Step 2 Parse the data

        public static Dictionary<string, ProtoMessage> Deserialize(byte[] descriptorData, byte[] data)
        {
            var fileDescriptorSet = FileDescriptorSet.Parser.ParseFrom(descriptorData);
            var descriptor = fileDescriptorSet.File[0];

            var input = new CodedInputStream(data);

            // Build a list of message types that needs to be resolved later
            // By resolved I mean with data fed into it

            var messageMap = BuildMessageMap(descriptor);

            // Parse message
            foreach (var message in messageMap.Values)
            {
                message.ObjectMap = ParseProto(input, message, messageMap);
            }

            return messageMap;
        }

        private static Dictionary<string, object> ParseProto(CodedInputStream input, ProtoMessage message, IReadOnlyDictionary<string, ProtoMessage> messageMap)
        {
            uint tag = 0;
            var objectMap = new Dictionary<string, object>();
            foreach (var field in message.Fields)
            {
                tag = input.ReadTag();
                var val = GetDefaultValue(field.Type);

                if (!input.IsAtEnd)
                {
                    switch (field.Type)
                    {
                        case FieldDescriptorProto.Types.Type.Int32:
                            //tag = input.ReadTag();
                            val = input.ReadInt32();
                            break;
                        case FieldDescriptorProto.Types.Type.String:
                            //tag = input.ReadTag();
                            val = input.ReadString();
                            break;
                        case FieldDescriptorProto.Types.Type.Enum:
                            //tag = input.ReadTag();
                            val = input.ReadEnum(); 
                            break;
                        case FieldDescriptorProto.Types.Type.Message:
                            tag = input.ReadTag();
                            val = ParseProto(input, messageMap[field.TypeName], messageMap);
                            break;
                        default:
                            // No field mapping found then skip
                            throw new Exception("Unknown type encountered...");
                    }
                }

                objectMap.Add(field.Name, val);
            }

            return objectMap;
        }

        private static Dictionary<string, ProtoMessage> BuildMessageMap(FileDescriptorProto descriptor)
        {
            // TODO: Simplify...
            var messageMap = new Dictionary<string, ProtoMessage>();
            foreach (var messageType in descriptor.MessageType)
            {
                messageMap.Add(messageType.Name, new ProtoMessage
                {
                    Name = messageType.Name,
                    Fields = ParseFields(messageType)
                });

                foreach (var nestedMessageType in messageType.NestedType)
                {
                    messageMap.Add(nestedMessageType.Name, new ProtoMessage
                    {
                        Name = nestedMessageType.Name,
                        Fields = ParseFields(nestedMessageType)
                    });
                }
            }

            return messageMap;
        }

        private static IEnumerable<FieldInfo> ParseFields(DescriptorProto message)
        {
            return message.Field.Select(x => new FieldInfo
            {
                Name = x.Name,
                Number = x.Number,
                Type = x.Type,
                // Type name needs to be more robust to support well known types
                TypeName = x.TypeName.Replace(".", "").Replace(message.Name, "")
            }).ToList();
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
                case FieldDescriptorProto.Types.Type.Message:
                    return new Dictionary<string, object>();
                default:
                    return null;
            }
        }
    }
}
