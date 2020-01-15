namespace ProtobufDeserializer.Tests.Dtos
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        // An extra field that should get ignored...
        public string LastName { get; set; }
        public CustomerType Type { get; set; }
    }

    public enum CustomerType
    {
        Normal = 0,
        Vip
    }
}
