using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using ProtobufDeserializer.Extensions;
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
            var messageMap = BuildMessageMap();
            if (string.IsNullOrEmpty(messageName) && messageMap.Keys.Count > 1)
                throw new ArgumentException("Please provide a message name since the descriptor contains multiple message types.");

            if (string.IsNullOrEmpty(messageName) && messageMap.Keys.Count == 1)
                messageName = messageMap.Keys.FirstOrDefault();

            if (!messageMap.TryGetValue(messageName, out var fieldMap))
                throw new ArgumentException("The provided message name does not match anything that is defined in the descriptor.");

            using (var input = new CodedInputStream(data))
            {
                ReadFields(input, fieldMap, messageMap);
            }

            return fieldMap;
        }

        // This can/should be cached as well like how I cache property types...
        private Dictionary<string, Dictionary<string, object>> BuildMessageMap()
        {
            //var fileDescriptorSet = FileDescriptorSet.Parser.ParseFrom(descriptorData);
            //var descriptor = fileDescriptorSet.File[0];

            //var map = new Dictionary<string, Dictionary<string, object>>();
            //foreach (var message in descriptor.MessageType)
            //{
            //    var messageMap = new Dictionary<string, object>();
            //    foreach (var field in message.Field)
            //    {
            //        messageMap.Add(field.Name, null);
            //    }
            //    map.Add(message.Name, messageMap);
                
            //    foreach (var nestedMessage in message.NestedType)
            //    {
            //        var nestedMessageMap = new Dictionary<string, object>();
            //        foreach (var field in nestedMessage.Field)
            //        {
            //            nestedMessageMap.Add(field.Name, null);
            //        }
            //        map.Add(nestedMessage.Name, nestedMessageMap);
            //    }
            //}

            //return map;

            return new Dictionary<string, Dictionary<string, object>>();;
        }

        private void ReadFields(CodedInputStream input, Dictionary<string, object> fieldMap, IReadOnlyDictionary<string, Dictionary<string, object>> messageMap)
        {
            var propsQueue = new Queue<string>(fieldMap.Keys.ToList());
            while (propsQueue.Count > 0)
            {
                var prop = propsQueue.Dequeue();
                var tag = input.PeekTag();
                if (tag == 0) break;

                var field = descriptor.GetField(tag, prop);
                if (field == null)
                {
                    propsQueue.Enqueue(prop);
                    continue;
                }

                var propValue = field?.ReadValue(input);
                if (field.Type == FieldDescriptorProto.Types.Type.Message)
                {
                    // TODO chanveg to trygetvalue
                    var nestedMessage = messageMap[field.MessageName];

                    ReadFields(input, nestedMessage, messageMap);
                    fieldMap[prop] = nestedMessage;
                }
                else
                {
                    fieldMap[prop] = propValue;
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

        //private void ReadFields(CodedInputStream input, object targetInstance, Type targetInstanceType)
        //{
        //    uint tag;
        //    //var propsQueue = typeProperties.GetQueue(targetInstanceType);
        //    var props = typeProperties.GetList(targetInstanceType);

        //    var propNode = props.First;
        //    while ((tag = input.PeekTag()) != 0)
        //    {
        //        var prop = propNode.Value;
        //        propNode = propNode.NextOrFirst();

        //        var field = descriptor.GetField(tag, prop.Name);
        //        if (field == null)
        //        {

        //            continue;
        //        }

        //        var propValue = field?.ReadValue(input);

        //        if (prop.PropertyType.IsPrimitive
        //               || prop.PropertyType.IsEnum
        //               || prop.PropertyType == typeof(string)
        //               // TODO Test for decimal since float currently works
        //               || prop.PropertyType == typeof(decimal))
        //        {
        //            prop.SetValue(targetInstance, propValue);
        //        }
        //        else
        //        {
        //            // Lists and arrays
        //            if (prop.PropertyType.GetInterface(nameof(IEnumerable)) != null)
        //            {
        //                if (prop.PropertyType.IsArray && propValue != null)
        //                {
        //                    var toArray = propValue.GetType().GetMethod("ToArray");
        //                    prop.SetValue(targetInstance, toArray?.Invoke(propValue, null));
        //                }
        //                else
        //                {
        //                    prop.SetValue(targetInstance, propValue);
        //                }

        //                continue;
        //            }

        //            // Nested Messages
        //            // Before we actually parse fields for a nested message, we need to read off/shave off some extra bytes first

        //            var instance = Activator.CreateInstance(prop.PropertyType);
        //            ReadFields(input, instance, prop.PropertyType);
        //            prop.SetValue(targetInstance, instance);
        //        }
        //    }
        //}

        private void ReadFields(CodedInputStream input, object targetInstance, Type targetInstanceType)
        {
            uint tag;
            var propsQueue = typeProperties.GetQueue(targetInstanceType);

            while ((tag = input.PeekTag()) != 0 && propsQueue.Count > 0)
            {
                var prop = propsQueue.Dequeue();
                var field = descriptor.GetField(tag, prop.Name);
                if (field == null && descriptor.FieldExists(prop.Name))
                {
                    propsQueue.Enqueue(prop);
                    continue;
                }

                var propValue = field?.ReadValue(input);

                if (prop.PropertyType.IsPrimitive
                       || prop.PropertyType.IsEnum
                       || prop.PropertyType == typeof(string)
                       // TODO Test for decimal since float currently works
                       || prop.PropertyType == typeof(decimal))
                {
                    prop.SetValue(targetInstance, propValue);
                }
                else
                {
                    // Lists and arrays
                    if (prop.PropertyType.GetInterface(nameof(IEnumerable)) != null)
                    {
                        if (prop.PropertyType.IsArray && propValue != null)
                        {
                            var toArray = propValue.GetType().GetMethod("ToArray");
                            prop.SetValue(targetInstance, toArray?.Invoke(propValue, null));
                        }
                        else
                        {
                            prop.SetValue(targetInstance, propValue);
                        }

                        continue;
                    }

                    // Nested Messages
                    // Before we actually parse fields for a nested message, we need to read off/shave off some extra bytes first

                    var instance = Activator.CreateInstance(prop.PropertyType);
                    ReadFields(input, instance, prop.PropertyType);
                    prop.SetValue(targetInstance, instance);
                }
            }
        }

        //private void ReadFields(CodedInputStream input, object targetInstance, Type targetInstanceType)
        //{
        //    uint tag;
        //    var propsQueue = typeProperties.GetQueue(targetInstanceType);

        //    while ((tag = input.PeekTag()) != 0 && propsQueue.Count > 0)
        //    {
        //        var prop = propsQueue.Dequeue();
        //        var field = descriptor.GetField(tag, prop.Name);
        //        if (field == null && descriptor.FieldExists(prop.Name))
        //        {
        //            propsQueue.Enqueue(prop);
        //            continue;
        //        }

        //        var propValue = field?.ReadValue(input);

        //        if (prop.PropertyType.IsPrimitive
        //               || prop.PropertyType.IsEnum
        //               || prop.PropertyType == typeof(string)
        //               // TODO Test for decimal since float currently works
        //               || prop.PropertyType == typeof(decimal))
        //        {
        //            prop.SetValue(targetInstance, propValue);
        //        }
        //        else
        //        {
        //            // Lists and arrays
        //            if (prop.PropertyType.GetInterface(nameof(IEnumerable)) != null)
        //            {
        //                if (prop.PropertyType.IsArray && propValue != null)
        //                {
        //                    var toArray = propValue.GetType().GetMethod("ToArray");
        //                    prop.SetValue(targetInstance, toArray?.Invoke(propValue, null));
        //                }
        //                else
        //                {
        //                    prop.SetValue(targetInstance, propValue);
        //                }

        //                continue;
        //            }

        //            // Nested Messages
        //            // Before we actually parse fields for a nested message, we need to read off/shave off some extra bytes first

        //            var instance = Activator.CreateInstance(prop.PropertyType);
        //            ReadFields(input, instance, prop.PropertyType);
        //            prop.SetValue(targetInstance, instance);
        //        }
        //    }
        //}

        //private void SetPropertyValue(object obj, object value)
        //{

        //}
    }
}
