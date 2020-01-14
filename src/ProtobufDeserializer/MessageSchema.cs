//using System.Collections.Generic;
//using Google.Protobuf.Reflection;

//namespace ProtobufDeserializer.V2
//{
//    public class MessageSchema
//    {
//        private readonly byte[] descriptorData;
//        private readonly Dictionary<string, IField> schema;

//        public MessageSchema(byte[] descriptor)
//        {
//            descriptorData = descriptor;

//            schema = GetMessageSchema();
//        }

//        private Dictionary<string, IField> GetMessageSchema()
//        {
//            var fileDescriptorSet = FileDescriptorSet.Parser.ParseFrom(descriptorData);
//            var descriptor = fileDescriptorSet.File[0];

//            return ParseMessages(descriptor.MessageType);
//        }

//        private static Dictionary<string, IField> ParseMessages(IEnumerable<DescriptorProto> messages)
//        {
//            var messageSchema = new Dictionary<string, IField>();
//            foreach (var message in messages)
//            {
//                foreach (var field in message.Field)
//                {
//                    messageSchema.Add(field.Name, FieldFactory.Create(message.Name, field));
//                }

//                foreach (var nestedMessage in message.NestedType)
//                {
//                    foreach (var field in nestedMessage.Field)
//                    {
//                        messageSchema.Add(field.Name, FieldFactory.Create(nestedMessage.Name, field));
//                    }
//                }
//            }

//            return messageSchema;
//        }

//        public void AddField(string name, IField field)
//        {
//            if (schema.ContainsKey(name))
//            {

//            }

//            schema.Add(name, field);
//        }
//    }
//}
