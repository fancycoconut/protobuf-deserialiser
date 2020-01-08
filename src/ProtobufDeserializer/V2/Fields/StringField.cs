using Google.Protobuf;
using Google.Protobuf.Reflection;
using ProtobufDeserializer.Fields;

namespace ProtobufDeserializer.V2.Fields
{
    public class StringField : Field
    {
        public const FieldDescriptorProto.Types.Type FieldType = FieldDescriptorProto.Types.Type.String;

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