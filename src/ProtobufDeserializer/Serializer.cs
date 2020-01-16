using System;

namespace ProtobufDeserializer
{
    /// <summary>
    /// TODO Implement serialization...
    /// </summary>
    public class Serializer
    {
        public Serializer(byte[] descriptor)
        {
            // Parse and hold the descriptor
        }

        public byte[] SerializeObject(object instance)
        {
            // We basically want to use our descriptor to serialize our object as much as possible...
            // Reflect through object
            // Use the descriptor to determine what to write, order does not matter ;)
            throw new NotImplementedException();
        }
    }
}
