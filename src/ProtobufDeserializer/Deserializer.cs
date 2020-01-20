using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using ProtobufDeserializer.Extensions;
using ProtobufDeserializer.Helpers;
using ProtobufDeserializer.Reflection;
using ProtobufDeserializer.Schema;

namespace ProtobufDeserializer
{
    public class Deserializer
    {
        private readonly ITypeProperties typeProperties;

        private readonly Descriptor descriptor;
        private readonly Dictionary<string, IField> messageSchema;

        public Deserializer(byte[] descriptorData)
        {
            this.descriptor = new Descriptor(descriptorData);

            typeProperties = new CachedTypeProperties(new TypeProperties());
        }

        /// <summary>
        /// Deserializes a given stream of protobuf based on the provided descriptor
        /// This can be a potentially expensive process especially for nested messages
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> DeserializeToMap(byte[] data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserializes a given stream of protobuf based on the provided descriptor and message name
        /// </summary>
        /// <param name="data"></param>
        /// <param name="messageName"></param>
        /// <returns></returns>
        public Dictionary<string, object> DeserializeToMap(byte[] data, string messageName)
        {
            var messageSchema = descriptor.GetMessageSchema();
            if (string.IsNullOrEmpty(messageName) && messageSchema.Keys.Count > 1)
                throw new ArgumentException("Please provide a message name since the descriptor contains multiple message types.");

            if (string.IsNullOrEmpty(messageName) && messageSchema.Keys.Count == 1)
                messageName = messageSchema.Keys.FirstOrDefault();

            if (!messageSchema.TryGetValue(messageName, out var message))
                throw new ArgumentException("The provided message name does not match anything that is defined in the descriptor.");

            using (var input = new CodedInputStream(data))
            {
                ReadFields(input, message);
            }

            return message;
        }

        private void ReadFields(CodedInputStream input, Dictionary<string, object> message)
        {
            uint tag;
            while ((tag = input.PeekTag()) != 0)
            {
                foreach (var key in message.Keys)
                {
                    var field = descriptor.GetField(tag, key);
                    if (field == null) continue;
                    var propValue = field?.ReadValue(input);

                    if (field.Type == FieldDescriptorProto.Types.Type.Message)
                    {
                        return;
                    }






                    message[key] = propValue;
                    break;

                    


                    //if (prop.PropertyType.IsPrimitive
                    //    || prop.PropertyType.IsEnum
                    //    || prop.PropertyType == typeof(string)
                    //    // TODO Test for decimal since float currently works
                    //    || prop.PropertyType == typeof(decimal))
                    //{
                    //    prop.SetValue(targetInstance, propValue);
                    //}
                    //else
                    //{
                    //    // Lists and arrays
                    //    if (prop.PropertyType.GetInterface(nameof(IEnumerable)) != null)
                    //    {
                    //        if (prop.PropertyType.IsArray && propValue != null)
                    //        {
                    //            var toArray = propValue.GetType().GetMethod("ToArray");
                    //            prop.SetValue(targetInstance, toArray?.Invoke(propValue, null));
                    //        }
                    //        else
                    //        {
                    //            prop.SetValue(targetInstance, propValue);
                    //        }

                    //        continue;
                    //    }

                    //    // Nested Messages
                    //    // Before we actually parse fields for a nested message, we need to read off/shave off some extra bytes first

                    //    var instance = Activator.CreateInstance(prop.PropertyType);
                    //    ReadFields(input, instance, prop.PropertyType);
                    //    prop.SetValue(targetInstance, instance);
                    //}
                }
            }
        }

        /// <summary>
        /// Deserializes a given stream of protobuf based on the provided descriptor into a concrete typed object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public T Deserialize<T>(byte[] data)
        {
            return (T) Deserialize(data, typeof(T));
        }

        /// <summary>
        /// Deserializes a given stream of protobuf based on the provided descriptor into a concrete typed object
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
            uint tag;
            var props = typeProperties.GetList(targetInstanceType);

            var propNode = props.First;
            while ((tag = input.PeekTag()) != 0)
            {
                if (ProtobufHelper.ComputeFieldNumber((int)tag) > props.Count)
                    return;

                var prop = propNode.Value;
                propNode = propNode.NextOrFirst();
                if (!descriptor.FieldExists(prop.Name)) continue;

                var field = descriptor.GetField(tag, prop.Name);
                if (field == null) continue;

                var propValue = field.ReadValue(input);
                SetProperty(input, prop, propValue, targetInstance);
            }
        }

        private void SetProperty(CodedInputStream input, PropertyInfo prop, object propertyValue, object targetInstance)
        {
            if (prop.PropertyType.IsPrimitive
                || prop.PropertyType.IsEnum
                || prop.PropertyType == typeof(string)
                // TODO Test for decimal since float currently works
                || prop.PropertyType == typeof(decimal))
            {
                prop.SetValue(targetInstance, propertyValue);
                return;
            }

            // Lists and arrays
            if (prop.PropertyType.GetInterface(nameof(IEnumerable)) != null)
            {
                if (prop.PropertyType.IsArray && propertyValue != null)
                {
                    var toArray = propertyValue.GetType().GetMethod("ToArray");
                    prop.SetValue(targetInstance, toArray?.Invoke(propertyValue, null));
                }
                else
                {
                    prop.SetValue(targetInstance, propertyValue);
                }

                return;
            }

            // Nested Messages
            // Before we actually parse fields for a nested message, we need to read off/shave off some extra bytes first

            var instance = Activator.CreateInstance(prop.PropertyType);
            ReadFields(input, instance, prop.PropertyType);
            prop.SetValue(targetInstance, instance);
        }
    }
}
