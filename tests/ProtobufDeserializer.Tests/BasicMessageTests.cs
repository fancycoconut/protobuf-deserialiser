using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtobufDeserializer.Tests.Proto;
using ProtobufDeserializer.V2;

namespace ProtobufDeserializer.Tests
{
    [TestClass]
    public class BasicMessageTests
    {
        [TestMethod]
        public void BasicMessageToDictionary()
        {
            // Arrange
            var customerData = "8,1,18,5,75,97,119,97,105,26,4,87,111,110,103,32,1".Split(',');
            var customerMessageDescriptor = "0A BE 01 0A 0E 43 75 73 74 6F 6D 65 72 2E 70 72 6F 74 6F 22 A3 01 0A 08 43 75 73 74 6F 6D 65 72 12 0E 0A 02 49 64 18 01 20 01 28 05 52 02 49 64 12 1C 0A 09 46 69 72 73 74 4E 61 6D 65 18 02 20 01 28 09 52 09 46 69 72 73 74 4E 61 6D 65 12 18 0A 07 53 75 72 6E 61 6D 65 18 03 20 01 28 09 52 07 53 75 72 6E 61 6D 65 12 2A 0A 04 54 79 70 65 18 04 20 01 28 0E 32 16 2E 43 75 73 74 6F 6D 65 72 2E 43 75 73 74 6F 6D 65 72 54 79 70 65 52 04 54 79 70 65 22 23 0A 0C 43 75 73 74 6F 6D 65 72 54 79 70 65 12 0A 0A 06 4E 6F 72 6D 61 6C 10 00 12 07 0A 03 56 49 50 10 01 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = customerData.Select(x => Convert.ToByte(x)).ToArray();
            var descriptor = customerMessageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Act
            var deserializer = new Deserializer(descriptor);
            var map = deserializer.Deserialize(data);

            // Assert
            Assert.AreEqual(4, map.Keys.Count);
            Assert.AreEqual(1, map["Id"]);
            Assert.AreEqual("Kawai", map["FirstName"]);
            Assert.AreEqual("Wong", map["Surname"]);
            Assert.AreEqual(1, map["Type"]);
        }

        [TestMethod]
        public void BasicMessageToObject()
        {
            // Arrange
            var customerData = "8,1,18,5,75,97,119,97,105,26,4,87,111,110,103,32,1".Split(',');
            var customerMessageDescriptor = "0A BE 01 0A 0E 43 75 73 74 6F 6D 65 72 2E 70 72 6F 74 6F 22 A3 01 0A 08 43 75 73 74 6F 6D 65 72 12 0E 0A 02 49 64 18 01 20 01 28 05 52 02 49 64 12 1C 0A 09 46 69 72 73 74 4E 61 6D 65 18 02 20 01 28 09 52 09 46 69 72 73 74 4E 61 6D 65 12 18 0A 07 53 75 72 6E 61 6D 65 18 03 20 01 28 09 52 07 53 75 72 6E 61 6D 65 12 2A 0A 04 54 79 70 65 18 04 20 01 28 0E 32 16 2E 43 75 73 74 6F 6D 65 72 2E 43 75 73 74 6F 6D 65 72 54 79 70 65 52 04 54 79 70 65 22 23 0A 0C 43 75 73 74 6F 6D 65 72 54 79 70 65 12 0A 0A 06 4E 6F 72 6D 61 6C 10 00 12 07 0A 03 56 49 50 10 01 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = customerData.Select(x => Convert.ToByte(x)).ToArray();
            var descriptor = customerMessageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Act
            var deserializer = new Deserializer(descriptor);
            var customer = deserializer.Deserialize<Customer>(data);

            // Assert
            Assert.AreEqual(1, customer.Id);
            Assert.AreEqual("Kawai", customer.FirstName);
            Assert.AreEqual("Wong", customer.Surname);
            Assert.AreEqual(CustomerType.Vip, customer.Type);
            // Should ignore fields that don't exist and default them
            Assert.AreEqual(null, customer.LastName);
        }

        [TestMethod]
        public void BasicMessageMiddleFieldNotSetToObject()
        {
            // Arrange
            var customerData = "8,1,18,5,75,97,119,97,105,32,1".Split(',');
            var customerMessageDescriptor = "0A BE 01 0A 0E 43 75 73 74 6F 6D 65 72 2E 70 72 6F 74 6F 22 A3 01 0A 08 43 75 73 74 6F 6D 65 72 12 0E 0A 02 49 64 18 01 20 01 28 05 52 02 49 64 12 1C 0A 09 46 69 72 73 74 4E 61 6D 65 18 02 20 01 28 09 52 09 46 69 72 73 74 4E 61 6D 65 12 18 0A 07 53 75 72 6E 61 6D 65 18 03 20 01 28 09 52 07 53 75 72 6E 61 6D 65 12 2A 0A 04 54 79 70 65 18 04 20 01 28 0E 32 16 2E 43 75 73 74 6F 6D 65 72 2E 43 75 73 74 6F 6D 65 72 54 79 70 65 52 04 54 79 70 65 22 23 0A 0C 43 75 73 74 6F 6D 65 72 54 79 70 65 12 0A 0A 06 4E 6F 72 6D 61 6C 10 00 12 07 0A 03 56 49 50 10 01 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = customerData.Select(x => Convert.ToByte(x)).ToArray();
            var descriptor = customerMessageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Act
            var deserializer = new Deserializer(descriptor);
            var customer = deserializer.Deserialize<Customer>(data);

            // Assert
            Assert.AreEqual(1, customer.Id);
            Assert.AreEqual("Kawai", customer.FirstName);
            Assert.AreEqual(null, customer.Surname);
            Assert.AreEqual(CustomerType.Vip, customer.Type);
            // Should ignore fields that don't exist and default them
            Assert.AreEqual(null, customer.LastName);
        }

        [TestMethod]
        public void EdwinsMessageWithMiddleFieldNotSetToObject()
        {
            // Arrange
            var personData = "0A 0E 4C 75 6B 65 20 53 6B 79 77 61 6C 6B 65 72 1A 17 6C 75 6B 65 2E 73 6B 79 77 61 6C 6B 65 72 40 6A 65 64 69 2E 63 6F 6D".Split(' ');
            var personMessageDescriptor = "0A 5A 0A 0C 50 65 72 73 6F 6E 2E 70 72 6F 74 6F 22 42 0A 06 50 65 72 73 6F 6E 12 12 0A 04 6E 61 6D 65 18 01 20 01 28 09 52 04 6E 61 6D 65 12 0E 0A 02 69 64 18 02 20 01 28 05 52 02 69 64 12 14 0A 05 65 6D 61 69 6C 18 03 20 01 28 09 52 05 65 6D 61 69 6C 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = personData.Select(x => Convert.ToByte(x, 16)).ToArray();
            var descriptor = personMessageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Act
            var deserializer = new Deserializer(descriptor);
            var person = deserializer.Deserialize(data);

            // Assert
            //Assert.AreEqual(0, person.Id);
            //Assert.AreEqual("Luke Skywalker", person.Name);
            //Assert.AreEqual(null, person.Email);
        }

        [TestMethod]
        public void EdwinsMessageWithLastFieldNotSetToObject()
        {
            // Arrange
            var personData = "0A 0E 4C 75 6B 65 20 53 6B 79 77 61 6C 6B 65 72 10 0A".Split(' ');
            var personMessageDescriptor = "0A 5A 0A 0C 50 65 72 73 6F 6E 2E 70 72 6F 74 6F 22 42 0A 06 50 65 72 73 6F 6E 12 12 0A 04 6E 61 6D 65 18 01 20 01 28 09 52 04 6E 61 6D 65 12 0E 0A 02 69 64 18 02 20 01 28 05 52 02 69 64 12 14 0A 05 65 6D 61 69 6C 18 03 20 01 28 09 52 05 65 6D 61 69 6C 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = personData.Select(x => Convert.ToByte(x, 16)).ToArray();
            var descriptor = personMessageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Act
            var deserializer = new Deserializer(descriptor);
            var person = deserializer.Deserialize<EdwinPerson>(data);

            // Assert
            Assert.AreEqual(10, person.Id);
            Assert.AreEqual("Luke Skywalker", person.Name);
            Assert.AreEqual(null, person.Email);
        }
    }
}
