using System;
using System.Collections.Generic;
using Google.Protobuf.Reflection;
using ProtobufDeserializer.Types;
using ProtobufDeserializer.WellKnownTypes;

namespace ProtobufDeserializer
{
    public class FieldFactory
    {
        //private const string FieldsNamespace = "ProtobufDeserializer.Types";
        //private const string WellKnownTypesNamespace = "ProtobufDeserializer.WellKnownTypes";

        private static Dictionary<string, Type> typeMap;

        static FieldFactory()
        {
            //typeMap = InitTypeMap();
        }

        //// This builds our type map dynamically and so it needs to only run once if possible
        //private static Dictionary<string, Type> InitTypeMap()
        //{
        //    var fieldTypes = Assembly.GetExecutingAssembly().GetTypes()
        //        .Where(t => t.Namespace == FieldsNamespace || t.Namespace == WellKnownTypesNamespace)
        //        .ToList();

        //    var map = new Dictionary<string, Type>();
        //    foreach (var type in fieldTypes)
        //    {
        //        var fieldType = type.GetField("FieldTypeName").GetValue(type);
        //        map.Add((string)fieldType, type);
        //    }

        //    return map;
        //}

        public static IField Create(FieldDescriptorProto fieldDescriptor)
        {
            //if (!string.IsNullOrEmpty(fieldDescriptor.TypeName)
            //    && fieldDescriptor.Type == FieldDescriptorProto.Types.Type.Message
            //    && typeMap.ContainsKey(fieldDescriptor.TypeName))
            //{
            //    return ConstructFieldObject(fieldDescriptor.TypeName, messageName, fieldDescriptor);
            //}

            //var typeName = fieldDescriptor.Type.ToString();
            //return ConstructFieldObject(typeName, messageName, fieldDescriptor);
            switch (fieldDescriptor.Type)
            {
                case FieldDescriptorProto.Types.Type.Bool:
                    return new BooleanField(fieldDescriptor);
                case FieldDescriptorProto.Types.Type.Bytes:
                    return new BytesField(fieldDescriptor);
                case FieldDescriptorProto.Types.Type.Float:
                    return new FloatField(fieldDescriptor);
                case FieldDescriptorProto.Types.Type.Int32:
                    return new Int32Field(fieldDescriptor);
                case FieldDescriptorProto.Types.Type.Uint32:
                    return new UInt32Field(fieldDescriptor);
                case FieldDescriptorProto.Types.Type.Enum:
                    return new EnumField(fieldDescriptor);
                case FieldDescriptorProto.Types.Type.String:
                    return new StringField(fieldDescriptor);
                case FieldDescriptorProto.Types.Type.Message:
                    if (fieldDescriptor.TypeName == ".google.protobuf.StringValue")
                        return new StringValue(fieldDescriptor);

                    return new MessageField(fieldDescriptor);
                default:
                    throw new NotImplementedException($"{fieldDescriptor.Type} is not supported!");
            }
        }
    }
}
