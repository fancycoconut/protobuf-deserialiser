using System;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace ProtobufDeserializer.Types
{
    public class BooleanField : Field
    {
        public const string FieldTypeName = nameof(FieldDescriptorProto.Types.Type.Bool);

        public override object ReadValue(CodedInputStream input)
        {
            if (Label == FieldDescriptorProto.Types.Label.Repeated)
            {
                // TODO Figure out if it is packed or unpacked
                throw new NotImplementedException();
                //Value = base.ReadPackedRepeated(input.ReadBool);
                //return;
            }

            var tag = input.ReadTag();
            if (tag == 0 || input.IsAtEnd) return null;

            return input.ReadBool();
        }
    }
}
