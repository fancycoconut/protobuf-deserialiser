using System;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf.Reflection;

namespace ProtobufDeserializer.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var personData = "0A 0E 4C 75 6B 65 20 53 6B 79 77 61 6C 6B 65 72 1A 17 6C 75 6B 65 2E 73 6B 79 77 61 6C 6B 65 72 40 6A 65 64 69 2E 63 6F 6D".Split(' ');
            var personMessageDescriptor = "0A 5A 0A 0C 50 65 72 73 6F 6E 2E 70 72 6F 74 6F 22 42 0A 06 50 65 72 73 6F 6E 12 12 0A 04 6E 61 6D 65 18 01 20 01 28 09 52 04 6E 61 6D 65 12 0E 0A 02 69 64 18 02 20 01 28 05 52 02 69 64 12 14 0A 05 65 6D 61 69 6C 18 03 20 01 28 09 52 05 65 6D 61 69 6C 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = personData.Select(x => Convert.ToByte(x, 16)).ToArray();
            var descriptor = personMessageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Used for performance test
            var dto = new EdwinTestScenario1();
            for (var i = 0; i < 1000000; i++)
            {
                var deserializer = new Deserializer(descriptor);
                dto = deserializer.Deserialize<EdwinTestScenario1>(data);
            }

            // Dynamic deserialising example...
            // This first part of turning a descriptor into concrete types can only be done in non net standard and even then I'm not sure about the performance of this
            // First we get the descriptor
            //var fileDescriptorSet = FileDescriptorSet.Parser.ParseFrom(descriptor);
            //var desc = fileDescriptorSet.File[0];

            //// Then we turn the message descriptor into a concrete type(s)
            //var types = new List<Type>();
            //foreach (var message in desc.MessageType)
            //{
            //    var factory = new MessageTypeFactory(message);
            //    types.Add(factory.GetConcreteType());
            //}

            //Console.WriteLine($"Types count: {types.Count}");
            //Console.WriteLine("Printing out generated types with props...");
            //foreach (var type in types)
            //{
            //    var props = type.GetProperties();
            //    Console.WriteLine($"{type.FullName}");

            //    foreach (var prop in props)
            //    {
            //        Console.WriteLine($"{prop.Name}, {prop.PropertyType}");
            //    }
            //}

            //// Then we deserialise using the type we get back
            //var targetType = types.FirstOrDefault();
            //var deserializer = new Deserializer(descriptor);
            //var objectInstance = deserializer.Deserialize(data, targetType);

            //Console.WriteLine(objectInstance);

            //Console.ReadLine();
        }

        private class EdwinTestScenario1
        {
            public string Name { get; set; }
            public int Id { get; set; }
            public string Email { get; set; }
        }
    }
}
