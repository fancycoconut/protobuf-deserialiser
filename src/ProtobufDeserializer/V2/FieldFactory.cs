using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using ProtobufDeserializer.Fields;

namespace ProtobufDeserializer.V2
{
    public class FieldFactory
    {
        private const string FieldsNamespace = "ProtobufDeserializer.V2.Fields";
        private static Dictionary<string, Type> typeMap;
        //private static Dictionary<FieldDescriptorProto.Types.Type, Type> typeMap = new Dictionary<FieldDescriptorProto.Types.Type, Type>
        //{
        //    { FieldDescriptorProto.Types.Type.Int32, typeof(Int32Field) },
        //    { FieldDescriptorProto.Types.Type.String, typeof(StringField) },
        //    { FieldDescriptorProto.Types.Type.Enum, typeof(EnumField) },
        //    { FieldDescriptorProto.Types.Type.Message, typeof(MessageField) },
        //};

        static FieldFactory()
        {
            typeMap = InitTypeMap();
        }

        // This builds our type map dynamically and so it needs to only run once if possible
        private static Dictionary<string, Type> InitTypeMap()
        {
            var fieldTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.Namespace == FieldsNamespace)
                .ToList();

            var map = new Dictionary<string, Type>();
            foreach (var type in fieldTypes)
            {
                var fieldType = type.GetField("FieldTypeName").GetValue(type);
                map.Add((string)fieldType, type);
            }

            return map;
        }

        public static IField Create(FieldDescriptorProto fieldDescriptor, CodedInputStream input)
        {
            if (!string.IsNullOrEmpty(fieldDescriptor.TypeName)
                && fieldDescriptor.Type == FieldDescriptorProto.Types.Type.Message
                && typeMap.ContainsKey(fieldDescriptor.TypeName))
            {
                return ConstructFieldObject(fieldDescriptor.TypeName, fieldDescriptor, input);
            }

            var typeName = fieldDescriptor.Type.ToString();
            return ConstructFieldObject(typeName, fieldDescriptor, input);
        }

        private static IField ConstructFieldObject(string typeName, FieldDescriptorProto fieldDescriptor, CodedInputStream input)
        {
            var typeExists = typeMap.TryGetValue(typeName, out var type);
            if (!typeExists) return null;

            var instance = Activator.CreateInstance(type, input);

            var field = (IField)instance;
            field.Name = fieldDescriptor.Name;
            field.Number = fieldDescriptor.Number;
            field.Label = fieldDescriptor.Label;
            field.Type = fieldDescriptor.Type;
            field.TypeName = fieldDescriptor.TypeName;

            return field;
        }
    }
}
