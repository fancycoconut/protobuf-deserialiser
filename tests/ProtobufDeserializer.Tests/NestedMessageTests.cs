using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtobufDeserializer.Tests.Dtos;
using ProtobufDeserializer.V2;

namespace ProtobufDeserializer.Tests
{
    [TestClass]
    public class NestedMessageTests
    {
        [TestMethod]
        public void NestedMessageInsideMessageToDictionary()
        {
            // Arrange
            var nestedData = "8,1,12,5,4b,61,77,61,69,1a,4,57,6f,6e,67,22,10,a,6,74,65,73,74,65,64,12,6,68,65,68,65,68,65".Split(',');
            var nestedMessageDescriptor = "0A CB 01 0A 0C 4E 65 73 74 65 64 2E 70 72 6F 74 6F 22 B2 01 0A 03 46 6F 6F 12 0E 0A 02 49 64 18 01 20 01 28 05 52 02 49 64 12 1C 0A 09 46 69 72 73 74 4E 61 6D 65 18 02 20 01 28 09 52 09 46 69 72 73 74 4E 61 6D 65 12 18 0A 07 53 75 72 6E 61 6D 65 18 03 20 01 28 09 52 07 53 75 72 6E 61 6D 65 12 2E 0A 0D 4E 65 73 74 65 64 4D 65 73 73 61 67 65 18 04 20 01 28 0B 32 08 2E 46 6F 6F 2E 42 61 72 52 0D 4E 65 73 74 65 64 4D 65 73 73 61 67 65 1A 33 0A 03 42 61 72 12 12 0A 04 53 74 61 72 18 01 20 01 28 09 52 04 53 74 61 72 12 18 0A 07 46 69 67 68 74 65 72 18 02 20 01 28 09 52 07 46 69 67 68 74 65 72 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = nestedData.Select(x => Convert.ToByte(x, 16)).ToArray();
            var descriptor = nestedMessageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Act
            var deserializer = new Deserializer(descriptor);
            var map = deserializer.Deserialize(data);

            // Assert
            Assert.AreEqual(6, map.Keys.Count);
            Assert.AreEqual(1, map["Id"]);
            Assert.AreEqual("Kawai", map["FirstName"]);
            Assert.AreEqual("Wong", map["Surname"]);
            Assert.AreEqual("tested", map["Star"]);
            Assert.AreEqual("hehehe", map["Fighter"]);
        }

        [TestMethod]
        public void NestedMessageInsideMessageToObject()
        {
            // Arrange
            var nestedData = "8,1,12,5,4b,61,77,61,69,1a,4,57,6f,6e,67,22,10,a,6,74,65,73,74,65,64,12,6,68,65,68,65,68,65".Split(',');
            var nestedMessageDescriptor = "0A CB 01 0A 0C 4E 65 73 74 65 64 2E 70 72 6F 74 6F 22 B2 01 0A 03 46 6F 6F 12 0E 0A 02 49 64 18 01 20 01 28 05 52 02 49 64 12 1C 0A 09 46 69 72 73 74 4E 61 6D 65 18 02 20 01 28 09 52 09 46 69 72 73 74 4E 61 6D 65 12 18 0A 07 53 75 72 6E 61 6D 65 18 03 20 01 28 09 52 07 53 75 72 6E 61 6D 65 12 2E 0A 0D 4E 65 73 74 65 64 4D 65 73 73 61 67 65 18 04 20 01 28 0B 32 08 2E 46 6F 6F 2E 42 61 72 52 0D 4E 65 73 74 65 64 4D 65 73 73 61 67 65 1A 33 0A 03 42 61 72 12 12 0A 04 53 74 61 72 18 01 20 01 28 09 52 04 53 74 61 72 12 18 0A 07 46 69 67 68 74 65 72 18 02 20 01 28 09 52 07 46 69 67 68 74 65 72 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = nestedData.Select(x => Convert.ToByte(x, 16)).ToArray();
            var descriptor = nestedMessageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Act
            var deserializer = new Deserializer(descriptor);
            var foo = deserializer.Deserialize<Dtos.Foo>(data);

            // Assert
            Assert.AreEqual(1, foo.Id);
            Assert.AreEqual("Kawai", foo.FirstName);
            Assert.AreEqual("Wong", foo.Surname);
            Assert.AreEqual("tested", foo.NestedMessage.Star);
            Assert.AreEqual("hehehe", foo.NestedMessage.Fighter);
        }

        [TestMethod]
        public void NestedMessageOutsideMessageToDictionary()
        {
            // Arrange
            var nestedData = "8,1,12,5,4b,61,77,61,69,1a,4,57,6f,6e,67,22,10,a,6,74,65,73,74,65,64,12,6,68,65,68,65,68,65".Split(',');
            var nestedMessageDescriptor = "0A C6 01 0A 0C 4E 65 73 74 65 64 2E 70 72 6F 74 6F 22 79 0A 03 46 6F 6F 12 0E 0A 02 49 64 18 01 20 01 28 05 52 02 49 64 12 1C 0A 09 46 69 72 73 74 4E 61 6D 65 18 02 20 01 28 09 52 09 46 69 72 73 74 4E 61 6D 65 12 18 0A 07 53 75 72 6E 61 6D 65 18 03 20 01 28 09 52 07 53 75 72 6E 61 6D 65 12 2A 0A 0D 4E 65 73 74 65 64 4D 65 73 73 61 67 65 18 04 20 01 28 0B 32 04 2E 42 61 72 52 0D 4E 65 73 74 65 64 4D 65 73 73 61 67 65 22 33 0A 03 42 61 72 12 12 0A 04 53 74 61 72 18 01 20 01 28 09 52 04 53 74 61 72 12 18 0A 07 46 69 67 68 74 65 72 18 02 20 01 28 09 52 07 46 69 67 68 74 65 72 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = nestedData.Select(x => Convert.ToByte(x, 16)).ToArray();
            var descriptor = nestedMessageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Act
            var deserializer = new Deserializer(descriptor);
            var map = deserializer.Deserialize(data);

            // Assert
            Assert.AreEqual(6, map.Keys.Count);
            Assert.AreEqual(1, map["Id"]);
            Assert.AreEqual("Kawai", map["FirstName"]);
            Assert.AreEqual("Wong", map["Surname"]);
            Assert.AreEqual("tested", map["Star"]);
            Assert.AreEqual("hehehe", map["Fighter"]);
        }

        [TestMethod]
        public void NestedMessageOutsideMessageToObject()
        {
            // Arrange
            var nestedData = "8,1,12,5,4b,61,77,61,69,1a,4,57,6f,6e,67,22,10,a,6,74,65,73,74,65,64,12,6,68,65,68,65,68,65".Split(',');
            var nestedMessageDescriptor = "0A C6 01 0A 0C 4E 65 73 74 65 64 2E 70 72 6F 74 6F 22 79 0A 03 46 6F 6F 12 0E 0A 02 49 64 18 01 20 01 28 05 52 02 49 64 12 1C 0A 09 46 69 72 73 74 4E 61 6D 65 18 02 20 01 28 09 52 09 46 69 72 73 74 4E 61 6D 65 12 18 0A 07 53 75 72 6E 61 6D 65 18 03 20 01 28 09 52 07 53 75 72 6E 61 6D 65 12 2A 0A 0D 4E 65 73 74 65 64 4D 65 73 73 61 67 65 18 04 20 01 28 0B 32 04 2E 42 61 72 52 0D 4E 65 73 74 65 64 4D 65 73 73 61 67 65 22 33 0A 03 42 61 72 12 12 0A 04 53 74 61 72 18 01 20 01 28 09 52 04 53 74 61 72 12 18 0A 07 46 69 67 68 74 65 72 18 02 20 01 28 09 52 07 46 69 67 68 74 65 72 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = nestedData.Select(x => Convert.ToByte(x, 16)).ToArray();
            var descriptor = nestedMessageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Act
            var deserializer = new Deserializer(descriptor);
            var foo = deserializer.Deserialize<Dtos.Foo>(data);

            // Assert
            Assert.AreEqual(1, foo.Id);
            Assert.AreEqual("Kawai", foo.FirstName);
            Assert.AreEqual("Wong", foo.Surname);
            Assert.AreEqual("tested", foo.NestedMessage.Star);
            Assert.AreEqual("hehehe", foo.NestedMessage.Fighter);
        }
    }
}
