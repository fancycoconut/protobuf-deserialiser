using Google.Protobuf;
using ProtobufDeserializer.Fields;

namespace ProtobufDeserializer.V2.Fields
{
    public class MessageField : Field
    {
        public MessageField(CodedInputStream input) : base(input) { }

        public override void ReadValue()
        {
            // For message, one field might be tag and the other might be length
            var tag = input.ReadTag();
            input.ReadTag();
        }
    }
}
