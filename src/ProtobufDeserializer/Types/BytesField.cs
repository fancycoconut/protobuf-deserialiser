using System;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace ProtobufDeserializer.Types
{
    public class BytesField : Field
    {
        public const string FieldTypeName = nameof(FieldDescriptorProto.Types.Type.Bytes);

        public override object ReadValue(CodedInputStream input)
        {
            if (Label == FieldDescriptorProto.Types.Label.Repeated)
            {
                throw new NotImplementedException();
                // TODO Figure out if it is packed or unpacked
                //Value = base.ReadPackedRepeated(input.ReadBytes);
            }

            var tag = input.ReadTag();
            if (tag == 0 || input.IsAtEnd) return null;

            return input.ReadBytes().ToByteArray();
        }
    }
}
