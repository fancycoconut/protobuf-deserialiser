using System;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace ProtobufDeserializer.V2.WellKnownTypes
{
    public class StringValue : Field
    {
        public const string FieldTypeName = ".google.protobuf.StringValue";

        public override object ReadValue(CodedInputStream input)
        {
            if (!base.CurrentFieldNumberIsCorrect(input)) return null;

            if (Label == FieldDescriptorProto.Types.Label.Repeated)
            {
                // TODO Figure out if it is packed or unpacked
                throw new NotImplementedException();
                //Value = base.ReadUnpackedRepeated(input.ReadString);
                //return null;
            }

            var tag = input.ReadTag();
            if (tag == 0 || input.IsAtEnd) return null;

            var codec = FieldCodec.ForClassWrapper<string>(tag);
            return codec.Read(input);
        }
    }
}
