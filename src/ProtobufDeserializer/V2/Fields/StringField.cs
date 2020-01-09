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
            var tag = input.PeekTag();

            // (field_number << 3) | wire_type
            // Top 5 bits are field number
            var t = tag & 0xF8;
            // Bottom 3 bits give the wire type
            var wireType = tag & 0x7;
            // Wire Types
            // 0    Varint int32, int64, uint32, uint64, sint32, sint64, bool, enum
            // 1    64-bit fixed64, sfixed64, double
            // 2	Length-delimited string, bytes, embedded messages, packed repeated fields
            // 3	Start group groups(deprecated)
            // 4	End group   groups(deprecated)
            // 5	32-bit fixed32, sfixed32, float

           var fieldNumber = t >> 3;
            if (Number != fieldNumber) return;
     
            tag = input.ReadTag();
            if (tag == 0 || input.IsAtEnd) return;

            // Should we use field codecs or read off input?...
            //var codec = FieldCodec.ForString(tag);
            //Value = codec.Read(input);
            // OR

            //if (Label == FieldDescriptorProto.Types.Label.Repeated)
            //{
            //    var codec = FieldCodec.ForString()
            //}

            Value = input.ReadString();
        }
    }
}