using ProtobufDeserializer.Fields;
using Google.Protobuf;

namespace ProtobufDeserializer.V2.Fields
{
    public class WellknownStringValue : Field
    {
        public const string FieldTypeName = ".google.protobuf.StringValue";

        public WellknownStringValue(CodedInputStream input) : base(input) { }

        public override void ReadValue()
        {
            if (!base.CurrentFieldNumberIsCorrect()) return;

            var tag = input.ReadTag();
            if (tag == 0 || input.IsAtEnd) return;

            var codec = FieldCodec.ForClassWrapper<string>(tag);
            Value = codec.Read(input);
        }
    }
}
