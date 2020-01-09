using System.Collections.Generic;

namespace ProtobufDeserializer.Tests.Proto
{
    public class RepeatedExample
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Students { get; set; }
        public List<int> Ages { get; set; }
    }
}
