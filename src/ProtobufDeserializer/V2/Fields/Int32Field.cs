using Google.Protobuf;
using Google.Protobuf.Reflection;
using ProtobufDeserializer.Fields;

namespace ProtobufDeserializer.V2.Fields
{
    public class Int32Field : Field
    {
        public const FieldDescriptorProto.Types.Type FieldType = FieldDescriptorProto.Types.Type.Int32;

        public Int32Field(CodedInputStream input) : base(input) { }

        public override void ReadValue()
        {
            var tag = input.ReadTag();
            Value = input.ReadInt32();
        }
    }
}
