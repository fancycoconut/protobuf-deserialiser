namespace ProtobufDeserializer.Tests.Dtos
{
    public class InfoPascalCase
    {
        public string Serial { get; set; }
        public string Family { get; set; }
        public string Model { get; set; }
    }

    public class InfoLowerCase
    {
        public string serial { get; set; }
        public string family { get; set; }
        public string model { get; set; }
    }

    public class InfoUpperCase
    {
        public string SERIAL { get; set; }
        public string FAMILY { get; set; }
        public string MODEL { get; set; }
    }
}
