using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Google.Protobuf.Reflection;

namespace ProtobufDeserializer.V2
{
    public class FieldFactory
    {
        private const string FieldsNamespace = "ProtobufDeserializer.V2.Types";
        private const string WellKnownTypesNamespace = "ProtobufDeserializer.V2.WellKnownTypes";

        private static Dictionary<string, Type> typeMap;

        static FieldFactory()
        {
            typeMap = InitTypeMap();
        }

        // This builds our type map dynamically and so it needs to only run once if possible
        private static Dictionary<string, Type> InitTypeMap()
        {
            var fieldTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.Namespace == FieldsNamespace || t.Namespace == WellKnownTypesNamespace)
                .ToList();

            var map = new Dictionary<string, Type>();
            foreach (var type in fieldTypes)
            {
                var fieldType = type.GetField("FieldTypeName").GetValue(type);
                map.Add((string)fieldType, type);
            }

            return map;
        }

        public static IField Create(string messageName, FieldDescriptorProto fieldDescriptor)
        {
            if (!string.IsNullOrEmpty(fieldDescriptor.TypeName)
                && fieldDescriptor.Type == FieldDescriptorProto.Types.Type.Message
                && typeMap.ContainsKey(fieldDescriptor.TypeName))
            {
                return ConstructFieldObject(fieldDescriptor.TypeName, messageName, fieldDescriptor);
            }

            var typeName = fieldDescriptor.Type.ToString();
            return ConstructFieldObject(typeName, messageName, fieldDescriptor);
        }

        private static IField ConstructFieldObject(string typeName, string messageName, FieldDescriptorProto fieldDescriptor)
        {
            var typeExists = typeMap.TryGetValue(typeName, out var type);
            if (!typeExists) throw new NotImplementedException($"{typeName} has not been implemented yet and is therefore not supported..");

            var instance = Activator.CreateInstance(type);

            var field = (IField)instance;
            field.Name = fieldDescriptor.Name;
            field.FieldNumber = fieldDescriptor.Number;
            field.Label = fieldDescriptor.Label;
            field.Type = fieldDescriptor.Type;
            field.TypeName = fieldDescriptor.TypeName;
            field.MessageName = messageName;

            return field;
        }
    }
}
