using System.Collections.Generic;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using ProtobufDeserializer.Fields;

namespace ProtobufDeserializer.V2.Fields
{
    public class StringField : Field
    {
        public const string FieldTypeName = nameof(FieldDescriptorProto.Types.Type.String);

        public StringField(CodedInputStream input) : base(input)
        {
        }

        public override void ReadValue()
        {
            if (!base.CurrentFieldNumberIsCorrect()) return;

            if (Label == FieldDescriptorProto.Types.Label.Repeated)
            {
                // TODO Figure out if it is packed or unpacked
                Value = base.ReadUnpackedRepeated(input.ReadString);
                return;
            }

            // Should we use field codecs or read off input?...
            //var codec = FieldCodec.ForString(tag);
            //Value = codec.Read(input);
            // OR

            var tag = input.ReadTag();
            if (tag == 0 || input.IsAtEnd) return;

            Value = input.ReadString();
        }

        private IEnumerable<string> ReadUnpackedRepeated()
        {
            uint tag;
            var list = new List<string>();
            while ((tag = input.PeekTag()) != 0)
            {
                var t = tag & 0xF8;
                var fieldNumber = t >> 3;

                if (fieldNumber != this.FieldNumber) break;

                input.ReadTag();
                list.Add(input.ReadString());
            }

            return list;
        }
    }
}