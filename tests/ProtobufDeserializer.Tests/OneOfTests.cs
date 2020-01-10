using System;
using System.Linq;
using Google.Protobuf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtobufDeserializer.V2;

namespace ProtobufDeserializer.Tests
{
    [TestClass]
    public class OneOfTests
    {
        public void BasicOneOfToDictionaryTest()
        {
            // Arrange
            var oneOfMessage = new OneOfExample
            {
                IntValue = 42
            };
            var data = oneOfMessage.ToByteArray();
            var oneOfMessageDescriptor = "0A 8B 02 0A 0B 6F 6E 65 6F 66 2E 70 72 6F 74 6F 22 F3 01 0A 0C 4F 6E 65 4F 66 45 78 61 6D 70 6C 65 12 10 0A 03 6B 65 79 18 01 20 01 28 09 52 03 6B 65 79 12 1F 0A 0A 62 6F 6F 6C 5F 76 61 6C 75 65 18 02 20 01 28 08 48 00 52 09 62 6F 6F 6C 56 61 6C 75 65 12 1D 0A 09 69 6E 74 5F 76 61 6C 75 65 18 03 20 01 28 05 48 00 52 08 69 6E 74 56 61 6C 75 65 12 1F 0A 0A 75 69 6E 74 5F 76 61 6C 75 65 18 04 20 01 28 0D 48 00 52 09 75 69 6E 74 56 61 6C 75 65 12 21 0A 0B 66 6C 6F 61 74 5F 76 61 6C 75 65 18 05 20 01 28 02 48 00 52 0A 66 6C 6F 61 74 56 61 6C 75 65 12 23 0A 0C 73 74 72 69 6E 67 5F 76 61 6C 75 65 18 06 20 01 28 09 48 00 52 0B 73 74 72 69 6E 67 56 61 6C 75 65 12 1F 0A 0A 62 79 74 65 5F 76 61 6C 75 65 18 07 20 01 28 0C 48 00 52 09 62 79 74 65 56 61 6C 75 65 42 07 0A 05 76 61 6C 75 65 62 06 70 72 6F 74 6F 33".Split(' ');
            var descriptor = oneOfMessageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Act
            var deserializer = new Deserializer(descriptor);
            var map = deserializer.Deserialize(data);

            // Assert
            Assert.AreEqual(1, map.Keys.Count);
        }
    }
}
