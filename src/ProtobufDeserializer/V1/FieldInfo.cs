using Google.Protobuf.Reflection;

namespace ProtobufDeserializer.V1
{
    public class FieldInfo
    {
        public string Name { get; set; }
        public int Number { get; set; }
        // This field only used if the field is of type Message
        public string TypeName { get; set; }
        public FieldDescriptorProto.Types.Type Type { get; set; }
    }
}