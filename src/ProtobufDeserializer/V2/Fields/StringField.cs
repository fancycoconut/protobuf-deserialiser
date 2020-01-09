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
     
            var tag = input.ReadTag();
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