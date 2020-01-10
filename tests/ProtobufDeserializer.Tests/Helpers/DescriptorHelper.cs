using System.IO;

namespace ProtobufDeserializer.Tests.Helpers
{
    public class DescriptorHelper
    {
        public static byte[] Read(string filename)
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            return File.ReadAllBytes($"{path}\\ProtoDescriptors\\{filename}");
        }
    }
}
