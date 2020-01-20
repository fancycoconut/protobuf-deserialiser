using System;
using Google.Protobuf.Reflection;

namespace ProtobufDeserializer.Helpers
{
    public class ProtobufHelper
    {
        public static int ComputeFieldTag(FieldDescriptorProto field)
        {
            // (field_number << 3) | wire_type
            return (field.Number << 3) | GetWireType(field);
        }

        public static int ComputeFieldNumber(int tag)
        {
            return (tag & 0xF8) >> 3;
        }

        // TODO Refactor/change this....
        public static int GetWireType(FieldDescriptorProto field)
        {
            // Wire Types
            // 0    Varint                  int32, int64, uint32, uint64, sint32, sint64, bool, enum
            // 1    64-bit                  fixed64, sfixed64, double
            // 2	Length-delimited        string, bytes, embedded messages, packed repeated fields
            // 3	Start                   group groups (deprecated)
            // 4	End group               groups (deprecated)
            // 5	32-bit                  fixed32, sfixed32, float

            switch (field.Type)
            {
                case FieldDescriptorProto.Types.Type.Int32:
                case FieldDescriptorProto.Types.Type.Int64:
                case FieldDescriptorProto.Types.Type.Uint32:
                case FieldDescriptorProto.Types.Type.Uint64:
                case FieldDescriptorProto.Types.Type.Sint32:
                case FieldDescriptorProto.Types.Type.Sint64:
                case FieldDescriptorProto.Types.Type.Bool:
                case FieldDescriptorProto.Types.Type.Enum:
                    return field.Label == FieldDescriptorProto.Types.Label.Repeated ? 2 : 0;

                case FieldDescriptorProto.Types.Type.Fixed64:
                case FieldDescriptorProto.Types.Type.Sfixed64:
                case FieldDescriptorProto.Types.Type.Double:
                    return 1;

                case FieldDescriptorProto.Types.Type.String:
                case FieldDescriptorProto.Types.Type.Bytes:
                case FieldDescriptorProto.Types.Type.Message:
                    return 2;

                case FieldDescriptorProto.Types.Type.Sfixed32:
                case FieldDescriptorProto.Types.Type.Fixed32:
                case FieldDescriptorProto.Types.Type.Float:
                    return 5;

                default:
                    throw new ArgumentOutOfRangeException(nameof(field.Type), field.Type, null);
            }
        }
    }
}
