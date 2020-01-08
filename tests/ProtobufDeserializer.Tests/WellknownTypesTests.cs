using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtobufDeserializer.V2;

namespace ProtobufDeserializer.Tests
{
    [TestClass]
    public class WellknownTypesTests
    {
        [TestMethod]
        public void BasicWellKnownTypes()
        {
            // Arrange
            var messageData = "8,1,12,5,4b,61,77,61,69,1a,4,57,6f,6e,67,22,10,a,6,74,65,73,74,65,64,12,6,68,65,68,65,68,65".Split(',');
            var messageDescriptor = "0A F4 01 0A 0A 69 6E 66 6F 2E 70 72 6F 74 6F 12 15 66 70 68 2E 6B 61 74 6F 2E 69 6E 66 6F 2E 76 31 62 65 74 61 31 1A 1E 67 6F 6F 67 6C 65 2F 70 72 6F 74 6F 62 75 66 2F 77 72 61 70 70 65 72 73 2E 70 72 6F 74 6F 22 A6 01 0A 04 49 6E 66 6F 12 34 0A 06 73 65 72 69 61 6C 18 01 20 01 28 0B 32 1C 2E 67 6F 6F 67 6C 65 2E 70 72 6F 74 6F 62 75 66 2E 53 74 72 69 6E 67 56 61 6C 75 65 52 06 73 65 72 69 61 6C 12 34 0A 06 66 61 6D 69 6C 79 18 02 20 01 28 0B 32 1C 2E 67 6F 6F 67 6C 65 2E 70 72 6F 74 6F 62 75 66 2E 53 74 72 69 6E 67 56 61 6C 75 65 52 06 66 61 6D 69 6C 79 12 32 0A 05 6D 6F 64 65 6C 18 03 20 01 28 0B 32 1C 2E 67 6F 6F 67 6C 65 2E 70 72 6F 74 6F 62 75 66 2E 53 74 72 69 6E 67 56 61 6C 75 65 52 05 6D 6F 64 65 6C 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = messageData.Select(x => Convert.ToByte(x, 16)).ToArray();
            var descriptor = messageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Act
            var deserializer = new Deserializer(descriptor);
            var map = deserializer.Deserialize(data);

            // Assert
        }
    }
}
