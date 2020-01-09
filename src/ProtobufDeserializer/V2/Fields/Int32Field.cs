using Google.Protobuf;
using Google.Protobuf.Reflection;
using ProtobufDeserializer.Fields;

namespace ProtobufDeserializer.V2.Fields
{
    public class Int32Field : Field
    {
        public const string FieldTypeName = nameof(FieldDescriptorProto.Types.Type.Int32);

        public Int32Field(CodedInputStream input) : base(input) { }

        public override void ReadValue()
        {
            var tag = input.ReadTag();
            if (tag == 0 || input.IsAtEnd) return;

            //var codec = FieldCodec.ForInt32(tag);

            Value = input.ReadInt32();
        }
    }
}
