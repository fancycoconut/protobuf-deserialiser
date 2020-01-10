using Google.Protobuf;
using Google.Protobuf.Reflection;
using ProtobufDeserializer.V2.Types;

namespace ProtobufDeserializer.V2.WellKnownTypes
{
    public class StringValue : Field
    {
        public const string FieldTypeName = ".google.protobuf.StringValue";

        public StringValue(CodedInputStream input) : base(input) { }

        public override void ReadValue()
        {
            if (!base.CurrentFieldNumberIsCorrect()) return;

            if (Label == FieldDescriptorProto.Types.Label.Repeated)
            {
                // TODO Figure out if it is packed or unpacked
                //Value = base.ReadUnpackedRepeated(input.ReadString);
                return;
            }

            var tag = input.ReadTag();
            if (tag == 0 || input.IsAtEnd) return;

            var codec = FieldCodec.ForClassWrapper<string>(tag);
            Value = codec.Read(input);
        }
    }
}
