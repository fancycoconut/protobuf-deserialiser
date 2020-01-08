using System;
using System.Collections.Generic;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using ProtobufDeserializer.Fields;
using ProtobufDeserializer.V2.Fields;

namespace ProtobufDeserializer.V2
{
    public class FieldFactory
    {
        private static Dictionary<FieldDescriptorProto.Types.Type, Type> typeMap = new Dictionary<FieldDescriptorProto.Types.Type, Type>
        {
            { FieldDescriptorProto.Types.Type.Int32, typeof(IntField) },
            { FieldDescriptorProto.Types.Type.String, typeof(StringField) },
            { FieldDescriptorProto.Types.Type.Enum, typeof(EnumField) },
            { FieldDescriptorProto.Types.Type.Message, typeof(MessageField) },
        };

        public static Field Create(FieldDescriptorProto fieldDescriptor, CodedInputStream input)
        {
            var typeExists = typeMap.TryGetValue(fieldDescriptor.Type, out var type);
            if (!typeExists) return null;

            var instance = Activator.CreateInstance(type, input);
            //var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty;

            instance.GetType().GetProperty("Name").SetValue(instance, fieldDescriptor.Name);
            instance.GetType().GetProperty("Number").SetValue(instance, fieldDescriptor.Number);
            instance.GetType().GetProperty("Type").SetValue(instance, fieldDescriptor.Type);
            // Type name needs to be more robust to support well known types
            instance.GetType().GetProperty("TypeName").SetValue(instance, fieldDescriptor.TypeName.Replace(".", "").Replace(fieldDescriptor.Name, ""));
            //type.InvokeMember("Name", bindingFlags, Type.DefaultBinder, instance, fieldDescriptor.Name);

            return (Field)instance;
        }
    }
}
