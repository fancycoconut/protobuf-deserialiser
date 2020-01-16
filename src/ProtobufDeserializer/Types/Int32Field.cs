using Google.Protobuf;
using Google.Protobuf.Reflection;
using ProtobufDeserializer.Extensions;

namespace ProtobufDeserializer.Types
{
    public class Int32Field : Field
    {
        public const string FieldTypeName = nameof(FieldDescriptorProto.Types.Type.Int32);

        public override object ReadValue(CodedInputStream input)
        {
            if (Label == FieldDescriptorProto.Types.Label.Repeated)
            {
                // TODO Figure out if it is packed or unpacked
                return base.ReadPackedRepeated(input, input.ReadInt32);
            }

            var tag = input.ReadTag();
            if (tag == 0 || input.IsAtEnd) return null;

            return input.ReadInt32();
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
