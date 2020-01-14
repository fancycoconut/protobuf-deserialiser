using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Google.Protobuf.Reflection;

namespace ProtobufDeserializer.Example
{
    /// <summary>
    /// This doesn't work in net standard because net standard does not support dynamic assembly
    /// https://benohead.com/blog/2013/12/26/create-anonymous-types-at-runtime-in-c-sharp/
    /// </summary>
    public class MessageTypeFactory
    {
        private Type concreteType;
        private readonly DescriptorProto message;

        public MessageTypeFactory(DescriptorProto message)
        {
            this.message = message;

            CreateConcreteType();
        }

        private void CreateConcreteType()
        {
            var dynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("StronglyTypedMessage"), AssemblyBuilderAccess.RunAndCollect);
            var dynamicModule = dynamicAssembly.DefineDynamicModule("StronglyTypedMessageModule");
            var typeBuilder = dynamicModule.DefineType($"ProtoNinja.Generated.{message.Name}", TypeAttributes.Public);

            var fields = message.Field.OrderBy(x => x.Number);
            foreach (var field in fields)
            {
                AddProperty(typeBuilder, field.Name, GetFieldType(field.Type));
            }

            // Attach a ToString method to every type
            //typeBuilder.DefineMethod()

            concreteType = typeBuilder.CreateType();
        }

        private static void AddProperty(TypeBuilder typeBuilder, string name, Type type)
        {
            const MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.HideBySig;

            var field = typeBuilder.DefineField("_" + name, typeof(string), FieldAttributes.Public);
            var property = typeBuilder.DefineProperty(name, PropertyAttributes.None, type, new[] { type });

            var getMethodBuilder = typeBuilder.DefineMethod("get_value", getSetAttr, type,
                Type.EmptyTypes);
            var getIl = getMethodBuilder.GetILGenerator();
            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, field);
            getIl.Emit(OpCodes.Ret);

            var setMethodBuilder = typeBuilder.DefineMethod("set_value", getSetAttr, null,
                new[] { type });
            var setIl = setMethodBuilder.GetILGenerator();
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, field);
            setIl.Emit(OpCodes.Ret);

            property.SetGetMethod(getMethodBuilder);
            property.SetSetMethod(setMethodBuilder);
        }

        public Type GetConcreteType()
        {
            return concreteType;
        }

        private static Type GetFieldType(FieldDescriptorProto.Types.Type type)
        {
            switch (type)
            {
                case FieldDescriptorProto.Types.Type.Float:
                    return typeof(float);
                case FieldDescriptorProto.Types.Type.Int32:
                    return typeof(int);
                case FieldDescriptorProto.Types.Type.Bool:
                    return typeof(bool);
                case FieldDescriptorProto.Types.Type.String:
                    return typeof(string);
                case FieldDescriptorProto.Types.Type.Message:
                    // Might need recursion for this...
                    return typeof(object);
                case FieldDescriptorProto.Types.Type.Uint32:
                    return typeof(uint);
                case FieldDescriptorProto.Types.Type.Enum:
                    return typeof(Enum);
                default:
                    return typeof(object);
            }
        }
    }
}
