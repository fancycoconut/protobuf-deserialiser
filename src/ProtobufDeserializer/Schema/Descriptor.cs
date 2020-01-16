using System.Collections.Generic;
using Google.Protobuf.Reflection;
using ProtobufDeserializer.Extensions;
using ProtobufDeserializer.Helpers;

namespace ProtobufDeserializer.Schema
{
    public class Descriptor
    {
        private readonly byte[] descriptorData;
        private readonly Dictionary<string, int> fieldMap;
        private readonly Dictionary<string, IField> messageFields;

        public Descriptor(byte[] descriptorData)
        {
            this.descriptorData = descriptorData;

            var fileDescriptorSet = FileDescriptorSet.Parser.ParseFrom(this.descriptorData);
            var descriptor = fileDescriptorSet.File[0];


            fieldMap = new Dictionary<string, int>();
            messageFields = new Dictionary<string, IField>();

            ParseMessages(descriptor.MessageType);
        }

        public IField GetField(uint tag, string fieldName)
        {
            var key = $"{tag}_{fieldName}";
            if (messageFields.TryGetValue(key, out var field)) return field;

            // Do we care about these other scenarios
            key = $"{tag}_{fieldName.ToLower()}";
            var lowerCasedFieldExists = messageFields.TryGetValue(key, out field);
            if (lowerCasedFieldExists) return field;

            key = $"{tag}_{fieldName.ToUpper()}";
            var upperCasedFieldExists = messageFields.TryGetValue(key, out field);
            return !upperCasedFieldExists ? null : field;
        }

        public bool FieldExists(string fieldName)
        {
            return fieldMap.ContainsKey(fieldName);
        }

        // Breakthrough!! Soo I think because there is a consistent way of generating a tag therefore this handles duplicated fields as well :)
        private void ParseMessages(IEnumerable<DescriptorProto> messages)
        {
            foreach (var message in messages)
            {
                foreach (var field in message.Field)
                {
                    var tag = ProtobufHelper.ComputeFieldTag(field);

                    fieldMap.AddIfNotExists(field.Name, 0);
                    messageFields.AddIfNotExists($"{tag}_{field.Name}", FieldFactory.Create(message.Name, field));
                }

                foreach (var nestedMessage in message.NestedType)
                {
                    foreach (var field in nestedMessage.Field)
                    {
                        var tag = ProtobufHelper.ComputeFieldTag(field);

                        fieldMap.AddIfNotExists(field.Name, 0);
                        messageFields.AddIfNotExists($"{tag}_{field.Name}", FieldFactory.Create(nestedMessage.Name, field));
                    }
                }
            }
        }
    }
}
