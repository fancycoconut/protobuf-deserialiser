using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace ProtobufDeserializer.Types
{
    public class StringField : Field
    {
        public StringField(FieldDescriptorProto fieldDescriptor) : base(fieldDescriptor)
        {
        }

        //public const string FieldTypeName = nameof(FieldDescriptorProto.Types.Type.String);

        public override object ReadValue(CodedInputStream input)
        {
            if (Label == FieldDescriptorProto.Types.Label.Repeated)
            {
                // TODO Figure out if it is packed or unpacked
                return base.ReadUnpackedRepeated(input, input.ReadString);
            }

            // Should we use field codecs or read off input?...
            //var codec = FieldCodec.ForString(tag);
            //Value = codec.Read(input);
            // OR

            var tag = input.ReadTag();
            if (tag == 0 || input.IsAtEnd) return null;

            return input.ReadString();
        }

        //private IEnumerable<string> ReadUnpackedRepeated()
        //{
        //    uint tag;
        //    var list = new List<string>();
        //    while ((tag = input.PeekTag()) != 0)
        //    {
        //        var t = tag & 0xF8;
        //        var fieldNumber = t >> 3;

        //        if (fieldNumber != this.FieldNumber) break;

        //        input.ReadTag();
        //        list.Add(input.ReadString());
        //    }

        //    return list;
        //}
    }
}