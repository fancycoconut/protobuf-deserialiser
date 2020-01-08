using Google.Protobuf;
using ProtobufDeserializer.Fields;

namespace ProtobufDeserializer.V2.Fields
{
    public class StringField : Field
    {
        public StringField(CodedInputStream input) : base(input)
        {
        }

        public override void ReadValue()
        {
            var tag = input.ReadTag();
            Value = input.ReadString();
        }
    }
}