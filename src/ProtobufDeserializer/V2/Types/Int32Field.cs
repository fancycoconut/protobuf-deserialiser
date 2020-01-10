using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace ProtobufDeserializer.V2.Types
{
    public class Int32Field : Field
    {
        public const string FieldTypeName = nameof(FieldDescriptorProto.Types.Type.Int32);

        public Int32Field(CodedInputStream input) : base(input) { }

        public override void ReadValue()
        {
            if (!base.CurrentFieldNumberIsCorrect()) return;

            if (Label == FieldDescriptorProto.Types.Label.Repeated)
            {
                // TODO Figure out if it is packed or unpacked
                Value = base.ReadPackedRepeated(input.ReadInt32);
                return;
            }

            var tag = input.ReadTag();
            if (tag == 0 || input.IsAtEnd) return;

            Value = input.ReadInt32();
        }

        //private IEnumerable<int> ReadPackedRepeated()
        //{
        //    var tag = input.ReadTag();
        //    var length = input.ReadLength();

        //    var list = new List<int>();
        //    for (var i = 0; i < length; i++)
        //    {
        //        list.Add(input.ReadInt32());
        //    }

        //    return list;
        //}
    }
}
