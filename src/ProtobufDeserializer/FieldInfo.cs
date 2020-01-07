using Google.Protobuf.Reflection;

namespace ProtobufDeserializer
{
    public class FieldInfo
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public FieldDescriptorProto.Types.Type Type { get; set; }
    }
}