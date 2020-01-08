using Google.Protobuf;
using Google.Protobuf.Reflection;
using ProtobufDeserializer.Fields;

namespace ProtobufDeserializer.V2.Fields
{
    public class EnumField : Field
    {
        public const FieldDescriptorProto.Types.Type FieldType = FieldDescriptorProto.Types.Type.Enum;

        public EnumField(CodedInputStream input) : base(input) { }

        public override void ReadValue()
        {
            var tag = input.ReadTag();
            Value = input.ReadEnum();
        }
    }
}
