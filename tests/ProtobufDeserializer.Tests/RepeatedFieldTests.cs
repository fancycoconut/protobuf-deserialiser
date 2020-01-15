using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtobufDeserializer.Tests.Dtos;

namespace ProtobufDeserializer.Tests
{
    [TestClass]
    public class RepeatedFieldTests
    {
        [TestMethod]
        public void RepeatedFieldsMessageToObjectWithLists()
        {
            // Arrange
            var repeatedObjectData = "8,c7,1,12,14,54,65,73,74,20,52,65,70,65,61,74,65,64,20,46,69,65,6c,64,73,1a,5,54,6f,6d,6d,79,1a,6,4a,6f,68,6e,6e,79,1a,4,50,68,69,6c,22,4,c,12,13,14".Split(',');
            var repeatedMessageDescriptor = "0A 7E 0A 0E 52 65 70 65 61 74 65 64 2E 70 72 6F 74 6F 22 64 0A 0E 52 65 70 65 61 74 65 64 4F 62 6A 65 63 74 12 0E 0A 02 49 64 18 01 20 01 28 05 52 02 49 64 12 12 0A 04 4E 61 6D 65 18 02 20 01 28 09 52 04 4E 61 6D 65 12 1A 0A 08 53 74 75 64 65 6E 74 73 18 03 20 03 28 09 52 08 53 74 75 64 65 6E 74 73 12 12 0A 04 41 67 65 73 18 04 20 03 28 05 52 04 41 67 65 73 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = repeatedObjectData.Select(x => Convert.ToByte(x, 16)).ToArray();
            var descriptor = repeatedMessageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Act
            var deserializer = new Deserializer(descriptor);
            var example = deserializer.Deserialize<RepeatedExample>(data);

            // Assert
            Assert.AreEqual(199, example.Id);
            Assert.AreEqual("Test Repeated Fields", example.Name);

            var students = example.Students.ToArray();
            Assert.AreEqual(3, students.Length);
            Assert.AreEqual("Tommy", students[0]);
            Assert.AreEqual("Johnny", students[1]);
            Assert.AreEqual("Phil", students[2]);

            var ages = example.Ages.ToArray();
            Assert.AreEqual(4, ages.Length);
            Assert.AreEqual(12, ages[0]);
            Assert.AreEqual(18, ages[1]);
            Assert.AreEqual(19, ages[2]);
            Assert.AreEqual(20, ages[3]);
        }

        [TestMethod]
        public void RepeatedFieldsMessageToObjectWithArrays()
        {
            // Arrange
            var repeatedObjectData = "8,c7,1,12,14,54,65,73,74,20,52,65,70,65,61,74,65,64,20,46,69,65,6c,64,73,1a,5,54,6f,6d,6d,79,1a,6,4a,6f,68,6e,6e,79,1a,4,50,68,69,6c,22,4,c,12,13,14".Split(',');
            var repeatedMessageDescriptor = "0A 7E 0A 0E 52 65 70 65 61 74 65 64 2E 70 72 6F 74 6F 22 64 0A 0E 52 65 70 65 61 74 65 64 4F 62 6A 65 63 74 12 0E 0A 02 49 64 18 01 20 01 28 05 52 02 49 64 12 12 0A 04 4E 61 6D 65 18 02 20 01 28 09 52 04 4E 61 6D 65 12 1A 0A 08 53 74 75 64 65 6E 74 73 18 03 20 03 28 09 52 08 53 74 75 64 65 6E 74 73 12 12 0A 04 41 67 65 73 18 04 20 03 28 05 52 04 41 67 65 73 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = repeatedObjectData.Select(x => Convert.ToByte(x, 16)).ToArray();
            var descriptor = repeatedMessageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Act
            var deserializer = new Deserializer(descriptor);
            var example = deserializer.Deserialize<RepeatedArrayExample>(data);

            // Assert
            Assert.AreEqual(199, example.Id);
            Assert.AreEqual("Test Repeated Fields", example.Name);

            Assert.AreEqual(3, example.Students.Length);
            Assert.AreEqual("Tommy", example.Students[0]);
            Assert.AreEqual("Johnny", example.Students[1]);
            Assert.AreEqual("Phil", example.Students[2]);

            Assert.AreEqual(4, example.Ages.Length);
            Assert.AreEqual(12, example.Ages[0]);
            Assert.AreEqual(18, example.Ages[1]);
            Assert.AreEqual(19, example.Ages[2]);
            Assert.AreEqual(20, example.Ages[3]);
        }
    }
}
