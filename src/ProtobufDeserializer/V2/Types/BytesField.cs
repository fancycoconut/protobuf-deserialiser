using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace ProtobufDeserializer.V2.Types
{
    public class BytesField : Field
    {
        public const string FieldTypeName = nameof(FieldDescriptorProto.Types.Type.Bytes);

        public BytesField(CodedInputStream input) : base(input) { }

        public override void ReadValue()
        {
            if (!base.CurrentFieldNumberIsCorrect()) return;

            if (Label == FieldDescriptorProto.Types.Label.Repeated)
            {
                // TODO Figure out if it is packed or unpacked
                //Value = base.ReadPackedRepeated(input.ReadBytes);
                return;
            }

            var tag = input.ReadTag();
            if (tag == 0 || input.IsAtEnd) return;

            Value = input.ReadBytes();
        }
    }
}
