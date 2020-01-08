using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using ProtobufDeserializer.Fields;
using ProtobufDeserializer.V2.Fields;

namespace ProtobufDeserializer.V2
{
    public class FieldFactory
    {
        private static Dictionary<FieldDescriptorProto.Types.Type, Type> typeMap;
        //private static Dictionary<FieldDescriptorProto.Types.Type, Type> typeMap = new Dictionary<FieldDescriptorProto.Types.Type, Type>
        //{
        //    { FieldDescriptorProto.Types.Type.Int32, typeof(Int32Field) },
        //    { FieldDescriptorProto.Types.Type.String, typeof(StringField) },
        //    { FieldDescriptorProto.Types.Type.Enum, typeof(EnumField) },
        //    { FieldDescriptorProto.Types.Type.Message, typeof(MessageField) },
        //};

        static FieldFactory()
        {
            typeMap = GetTypeMapDynamically();
        }

        // This builds our type map dynamically and so it needs to only run once if possible
        private static Dictionary<FieldDescriptorProto.Types.Type, Type> GetTypeMapDynamically()
        {
            var fieldTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.Namespace == "ProtobufDeserializer.V2.Fields").ToList();

            var typeMap = new Dictionary<FieldDescriptorProto.Types.Type, Type>();
            foreach (var type in fieldTypes)
            {
                var fieldType = type.GetField("FieldType").GetValue(type);
                typeMap.Add((FieldDescriptorProto.Types.Type)fieldType, type);
            }

            return typeMap;
        }

        public static IField Create(FieldDescriptorProto fieldDescriptor, CodedInputStream input)
        {
            var typeExists = typeMap.TryGetValue(fieldDescriptor.Type, out var type);
            if (!typeExists) return null;

            var instance = Activator.CreateInstance(type, input);
            //var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty;

            instance.GetType().GetProperty("Name").SetValue(instance, fieldDescriptor.Name);
            instance.GetType().GetProperty("Number").SetValue(instance, fieldDescriptor.Number);
            instance.GetType().GetProperty("Type").SetValue(instance, fieldDescriptor.Type);

            instance.GetType().GetProperty("TypeName").SetValue(instance, fieldDescriptor.TypeName);
            //type.InvokeMember("Name", bindingFlags, Type.DefaultBinder, instance, fieldDescriptor.Name);

            return (Field)instance;
        }
    }
}
