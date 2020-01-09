using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtobufDeserializer.Tests.Proto;
using ProtobufDeserializer.V2;

namespace ProtobufDeserializer.Tests
{
    [TestClass]
    public class WellknownTypesTests
    {
        [TestMethod]
        public void BasicWellKnownTypesToDictionary()
        {
            // Arrange
            var messageData = "a,15,a,13,4d,79,20,53,65,72,69,61,6c,20,4e,75,6d,62,65,72,2e,2e,2e,12,c,a,a,53,6c,65,65,70,53,74,79,6c,65,1a,7,a,5,4d,6f,64,65,6c".Split(',');
            var messageDescriptor = "0A F4 01 0A 0A 69 6E 66 6F 2E 70 72 6F 74 6F 12 15 66 70 68 2E 6B 61 74 6F 2E 69 6E 66 6F 2E 76 31 62 65 74 61 31 1A 1E 67 6F 6F 67 6C 65 2F 70 72 6F 74 6F 62 75 66 2F 77 72 61 70 70 65 72 73 2E 70 72 6F 74 6F 22 A6 01 0A 04 49 6E 66 6F 12 34 0A 06 73 65 72 69 61 6C 18 01 20 01 28 0B 32 1C 2E 67 6F 6F 67 6C 65 2E 70 72 6F 74 6F 62 75 66 2E 53 74 72 69 6E 67 56 61 6C 75 65 52 06 73 65 72 69 61 6C 12 34 0A 06 66 61 6D 69 6C 79 18 02 20 01 28 0B 32 1C 2E 67 6F 6F 67 6C 65 2E 70 72 6F 74 6F 62 75 66 2E 53 74 72 69 6E 67 56 61 6C 75 65 52 06 66 61 6D 69 6C 79 12 32 0A 05 6D 6F 64 65 6C 18 03 20 01 28 0B 32 1C 2E 67 6F 6F 67 6C 65 2E 70 72 6F 74 6F 62 75 66 2E 53 74 72 69 6E 67 56 61 6C 75 65 52 05 6D 6F 64 65 6C 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = messageData.Select(x => Convert.ToByte(x, 16)).ToArray();
            var descriptor = messageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Act
            var deserializer = new Deserializer(descriptor);
            var map = deserializer.Deserialize(data);

            // Assert
            Assert.AreEqual(3, map.Keys.Count);
            Assert.AreEqual("My Serial Number...", map["serial"]);
            Assert.AreEqual("SleepStyle", map["family"]);
            Assert.AreEqual("Model", map["model"]);
        }

        [TestMethod]
        public void BasicWellKnownTypesToObjectWithPascalCaseFieldNames()
        {
            // Arrange
            var messageData = "a,15,a,13,4d,79,20,53,65,72,69,61,6c,20,4e,75,6d,62,65,72,2e,2e,2e,12,c,a,a,53,6c,65,65,70,53,74,79,6c,65,1a,7,a,5,4d,6f,64,65,6c".Split(',');
            var messageDescriptor = "0A F4 01 0A 0A 69 6E 66 6F 2E 70 72 6F 74 6F 12 15 66 70 68 2E 6B 61 74 6F 2E 69 6E 66 6F 2E 76 31 62 65 74 61 31 1A 1E 67 6F 6F 67 6C 65 2F 70 72 6F 74 6F 62 75 66 2F 77 72 61 70 70 65 72 73 2E 70 72 6F 74 6F 22 A6 01 0A 04 49 6E 66 6F 12 34 0A 06 73 65 72 69 61 6C 18 01 20 01 28 0B 32 1C 2E 67 6F 6F 67 6C 65 2E 70 72 6F 74 6F 62 75 66 2E 53 74 72 69 6E 67 56 61 6C 75 65 52 06 73 65 72 69 61 6C 12 34 0A 06 66 61 6D 69 6C 79 18 02 20 01 28 0B 32 1C 2E 67 6F 6F 67 6C 65 2E 70 72 6F 74 6F 62 75 66 2E 53 74 72 69 6E 67 56 61 6C 75 65 52 06 66 61 6D 69 6C 79 12 32 0A 05 6D 6F 64 65 6C 18 03 20 01 28 0B 32 1C 2E 67 6F 6F 67 6C 65 2E 70 72 6F 74 6F 62 75 66 2E 53 74 72 69 6E 67 56 61 6C 75 65 52 05 6D 6F 64 65 6C 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = messageData.Select(x => Convert.ToByte(x, 16)).ToArray();
            var descriptor = messageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Act
            var deserializer = new Deserializer(descriptor);
            var info = deserializer.Deserialize<InfoPascalCase>(data);

            // Assert
            Assert.AreEqual("My Serial Number...", info.Serial);
            Assert.AreEqual("SleepStyle", info.Family);
            Assert.AreEqual("Model", info.Model);
        }

        [TestMethod]
        public void BasicWellKnownTypesToObjectWithLowerCaseFieldNames()
        {
            // Arrange
            var messageData = "a,15,a,13,4d,79,20,53,65,72,69,61,6c,20,4e,75,6d,62,65,72,2e,2e,2e,12,c,a,a,53,6c,65,65,70,53,74,79,6c,65,1a,7,a,5,4d,6f,64,65,6c".Split(',');
            var messageDescriptor = "0A F4 01 0A 0A 69 6E 66 6F 2E 70 72 6F 74 6F 12 15 66 70 68 2E 6B 61 74 6F 2E 69 6E 66 6F 2E 76 31 62 65 74 61 31 1A 1E 67 6F 6F 67 6C 65 2F 70 72 6F 74 6F 62 75 66 2F 77 72 61 70 70 65 72 73 2E 70 72 6F 74 6F 22 A6 01 0A 04 49 6E 66 6F 12 34 0A 06 73 65 72 69 61 6C 18 01 20 01 28 0B 32 1C 2E 67 6F 6F 67 6C 65 2E 70 72 6F 74 6F 62 75 66 2E 53 74 72 69 6E 67 56 61 6C 75 65 52 06 73 65 72 69 61 6C 12 34 0A 06 66 61 6D 69 6C 79 18 02 20 01 28 0B 32 1C 2E 67 6F 6F 67 6C 65 2E 70 72 6F 74 6F 62 75 66 2E 53 74 72 69 6E 67 56 61 6C 75 65 52 06 66 61 6D 69 6C 79 12 32 0A 05 6D 6F 64 65 6C 18 03 20 01 28 0B 32 1C 2E 67 6F 6F 67 6C 65 2E 70 72 6F 74 6F 62 75 66 2E 53 74 72 69 6E 67 56 61 6C 75 65 52 05 6D 6F 64 65 6C 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = messageData.Select(x => Convert.ToByte(x, 16)).ToArray();
            var descriptor = messageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Act
            var deserializer = new Deserializer(descriptor);
            var info = deserializer.Deserialize<InfoLowerCase>(data);

            // Assert
            Assert.AreEqual("My Serial Number...", info.serial);
            Assert.AreEqual("SleepStyle", info.family);
            Assert.AreEqual("Model", info.model);
        }

        [TestMethod]
        public void BasicWellKnownTypesToObjectWithUpperCaseFieldNames()
        {
            // Arrange
            var messageData = "a,15,a,13,4d,79,20,53,65,72,69,61,6c,20,4e,75,6d,62,65,72,2e,2e,2e,12,c,a,a,53,6c,65,65,70,53,74,79,6c,65,1a,7,a,5,4d,6f,64,65,6c".Split(',');
            var messageDescriptor = "0A F4 01 0A 0A 69 6E 66 6F 2E 70 72 6F 74 6F 12 15 66 70 68 2E 6B 61 74 6F 2E 69 6E 66 6F 2E 76 31 62 65 74 61 31 1A 1E 67 6F 6F 67 6C 65 2F 70 72 6F 74 6F 62 75 66 2F 77 72 61 70 70 65 72 73 2E 70 72 6F 74 6F 22 A6 01 0A 04 49 6E 66 6F 12 34 0A 06 73 65 72 69 61 6C 18 01 20 01 28 0B 32 1C 2E 67 6F 6F 67 6C 65 2E 70 72 6F 74 6F 62 75 66 2E 53 74 72 69 6E 67 56 61 6C 75 65 52 06 73 65 72 69 61 6C 12 34 0A 06 66 61 6D 69 6C 79 18 02 20 01 28 0B 32 1C 2E 67 6F 6F 67 6C 65 2E 70 72 6F 74 6F 62 75 66 2E 53 74 72 69 6E 67 56 61 6C 75 65 52 06 66 61 6D 69 6C 79 12 32 0A 05 6D 6F 64 65 6C 18 03 20 01 28 0B 32 1C 2E 67 6F 6F 67 6C 65 2E 70 72 6F 74 6F 62 75 66 2E 53 74 72 69 6E 67 56 61 6C 75 65 52 05 6D 6F 64 65 6C 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = messageData.Select(x => Convert.ToByte(x, 16)).ToArray();
            var descriptor = messageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Act
            var deserializer = new Deserializer(descriptor);
            var info = deserializer.Deserialize<InfoUpperCase>(data);

            // Assert
            Assert.AreEqual("My Serial Number...", info.SERIAL);
            Assert.AreEqual("SleepStyle", info.FAMILY);
            Assert.AreEqual("Model", info.MODEL);
        }
    }
}
