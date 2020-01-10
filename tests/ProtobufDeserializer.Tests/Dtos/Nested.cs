namespace ProtobufDeserializer.Tests.Dtos
{
    public class Foo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public Bar NestedMessage { get; set; }
    }

    public class Bar
    {
        public string Star { get; set; }
        public string Fighter { get; set; }
    }
}
