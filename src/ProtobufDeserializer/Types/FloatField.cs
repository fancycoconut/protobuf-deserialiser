using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace ProtobufDeserializer.Types
{
    public class FloatField : Field
    {
        public const string FieldTypeName = nameof(FieldDescriptorProto.Types.Type.Float);

        public override object ReadValue(CodedInputStream input)
        {
            if (!base.CurrentFieldNumberIsCorrect(input)) return null;

            if (Label == FieldDescriptorProto.Types.Label.Repeated)
            {
                // TODO Figure out if it is packed or unpacked
                return base.ReadPackedRepeated(input, input.ReadFloat);
            }

            var tag = input.ReadTag();
            if (tag == 0 || input.IsAtEnd) return null;

            return input.ReadFloat();
        }
    }
}
