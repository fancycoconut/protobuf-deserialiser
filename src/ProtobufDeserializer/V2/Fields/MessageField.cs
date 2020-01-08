using Google.Protobuf;
using Google.Protobuf.Reflection;
using ProtobufDeserializer.Fields;

namespace ProtobufDeserializer.V2.Fields
{
    public class MessageField : Field
    {
        public const string FieldTypeName = nameof(FieldDescriptorProto.Types.Type.Message);

        public MessageField(CodedInputStream input) : base(input) { }

        public override void ReadValue()
        {
            // For message, one field might be tag and the other might be length
            var tag = input.ReadTag();
            if (tag == 0 || input.IsAtEnd) return;

            var unknownValue = input.ReadTag();
        }
    }
}
