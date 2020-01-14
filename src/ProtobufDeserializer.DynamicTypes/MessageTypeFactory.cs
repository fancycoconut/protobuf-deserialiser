using System;
using System.Reflection;
using System.Reflection.Emit;
using Google.Protobuf.Reflection;

namespace ProtobufDeserializer.DynamicTypes
{
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
            var typeBuilder = dynamicModule.DefineType(message.Name, TypeAttributes.Public);

            //foreach (var field in message.Field)
            //{
                AddProperty(typeBuilder, "Test1", typeof(string));
                AddProperty(typeBuilder, "Test2", typeof(string));
                AddProperty(typeBuilder, "Test3", typeof(string));
            //}

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
    }
}
