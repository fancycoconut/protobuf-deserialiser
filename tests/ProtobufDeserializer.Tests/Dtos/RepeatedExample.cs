using System.Collections.Generic;

namespace ProtobufDeserializer.Tests.Dtos
{
    public class RepeatedExample
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Students { get; set; }
        public List<int> Ages { get; set; }
    }

    public class RepeatedArrayExample
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Students { get; set; }
        public int[] Ages { get; set; }
    }
}
