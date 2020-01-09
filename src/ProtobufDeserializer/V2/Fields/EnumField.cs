using Google.Protobuf;
using Google.Protobuf.Reflection;
using ProtobufDeserializer.Fields;

namespace ProtobufDeserializer.V2.Fields
{
    public class EnumField : Field
    {
        public const string FieldTypeName = nameof(FieldDescriptorProto.Types.Type.Enum);

        public EnumField(CodedInputStream input) : base(input) { }

        public override void ReadValue()
        {
            if (!base.CurrentFieldNumberIsCorrect()) return;

            var tag = input.ReadTag();
            if (tag == 0 || input.IsAtEnd) return;

            Value = input.ReadEnum();
        }
    }
}
