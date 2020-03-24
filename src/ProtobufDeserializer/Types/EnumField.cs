using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace ProtobufDeserializer.Types
{
    public class EnumField : Field
    {
        //public const string FieldTypeName = nameof(FieldDescriptorProto.Types.Type.Enum);

        public EnumField(FieldDescriptorProto fieldDescriptor) : base(fieldDescriptor)
        {
        }

        public override object ReadValue(CodedInputStream input)
        {
            var tag = input.ReadTag();
            if (tag == 0 || input.IsAtEnd) return null;

            return input.ReadEnum();
        }
    }
}
