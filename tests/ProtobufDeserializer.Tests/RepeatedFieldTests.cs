using System;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtobufDeserializer.Tests.Dtos;
using ProtobufDeserializer.Tests.Helpers;

namespace ProtobufDeserializer.Tests
{
    [TestClass]
    public class RepeatedFieldTests
    {
        [TestMethod]
        public void RepeatedFieldsMessageToObjectWithLists()
        {
            // Arrange
            var message = new RepeatedFieldsExample
            {
                Id = 199,
                Name = "Test Repeated Fields",
                Students = {"Tommy", "Johnny", "Phil"},
                Ages = { 12, 18, 19, 20 }
            };

            var data = message.ToByteArray();
            var descriptor = DescriptorHelper.Read("RepeatedFieldsExample.pb");

            // Act
            var deserializer = new Deserializer(descriptor);
            var example = deserializer.Deserialize<RepeatedExample>(data);

            // Assert
            Assert.AreEqual(message.Id, example.Id);
            Assert.AreEqual(message.Name, example.Name);

            var students = example.Students.ToArray();
            var expectedStudents = message.Students.ToArray();
            Assert.AreEqual(3, students.Length);
            Assert.AreEqual(expectedStudents[0], students[0]);
            Assert.AreEqual(expectedStudents[1], students[1]);
            Assert.AreEqual(expectedStudents[2], students[2]);

            var ages = example.Ages.ToArray();
            var expectedAges = message.Ages.ToArray();
            Assert.AreEqual(4, ages.Length);
            Assert.AreEqual(expectedAges[0], ages[0]);
            Assert.AreEqual(expectedAges[1], ages[1]);
            Assert.AreEqual(expectedAges[2], ages[2]);
            Assert.AreEqual(expectedAges[3], ages[3]);
        }

        [TestMethod]
        public void RepeatedFieldsMessageToObjectWithArrays()
        {
            // Arrange
            var message = new RepeatedFieldsExample
            {
                Id = 199,
                Name = "Test Repeated Fields",
                Students = { "Tommy", "Johnny", "Phil" },
                Ages = { 12, 18, 19, 20 }
            };

            var data = message.ToByteArray();
            var descriptor = DescriptorHelper.Read("RepeatedFieldsExample.pb");

            // Act
            var deserializer = new Deserializer(descriptor);
            var example = deserializer.Deserialize<RepeatedArrayExample>(data);

            // Assert
            Assert.AreEqual(message.Id, example.Id);
            Assert.AreEqual(message.Name, example.Name);

            var expectedStudents = message.Students.ToArray();
            Assert.AreEqual(3, example.Students.Length);
            Assert.AreEqual(expectedStudents[0], example.Students[0]);
            Assert.AreEqual(expectedStudents[1], example.Students[1]);
            Assert.AreEqual(expectedStudents[2], example.Students[2]);

            var expectedAges = message.Ages.ToArray();
            Assert.AreEqual(4, example.Ages.Length);
            Assert.AreEqual(expectedAges[0], example.Ages[0]);
            Assert.AreEqual(expectedAges[1], example.Ages[1]);
            Assert.AreEqual(expectedAges[2], example.Ages[2]);
            Assert.AreEqual(expectedAges[3], example.Ages[3]);
        }
    }
}
