﻿using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace ProtobufDeserializer.Types
{
    public class MessageField : Field
    {
        public const string FieldTypeName = nameof(FieldDescriptorProto.Types.Type.Message);

        public override object ReadValue(CodedInputStream input)
        {
            if (!base.CurrentFieldNumberIsCorrect(input)) return null;

            // For message, one field might be tag and the other might be length
            var tag = input.ReadTag();
            if (tag == 0 || input.IsAtEnd) return null;

            var unknownValue = input.ReadTag();

            // It doesn't matter for a message type because you need to proceed with reading off the fields within that given message
            return null;
        }
    }
}
