using Google.Protobuf.Reflection;

namespace ProtobufDeserialiser
{
    public class FieldInfo
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public FieldDescriptorProto.Types.Type Type { get; set; }
    }
}