using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace ProtobufDeserializer.V2.Types
{
    public class BooleanField : Field
    {
        public const string FieldTypeName = nameof(FieldDescriptorProto.Types.Type.Bool);

        public BooleanField(CodedInputStream input) : base(input) { }

        public override void ReadValue()
        {
            if (!base.CurrentFieldNumberIsCorrect()) return;

            if (Label == FieldDescriptorProto.Types.Label.Repeated)
            {
                // TODO Figure out if it is packed or unpacked
                //Value = base.ReadPackedRepeated(input.ReadBool);
                return;
            }

            var tag = input.ReadTag();
            if (tag == 0 || input.IsAtEnd) return;

            Value = input.ReadBool();
        }
    }
}
