using System;
using System.Linq;
using Google.Protobuf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtobufDeserializer.Tests.Dtos;
using ProtobufDeserializer.Tests.Helpers;
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
            var customer = new Customer
            {
                Id = 1,
                FirstName = "Kawai",
                Surname = "Wong",
                Type = Customer.Types.CustomerType.Vip
            };
            
            var data = customer.ToByteArray();
            var descriptor = DescriptorHelper.Read("Customer.pb");

            // Act
            var deserializer = new Deserializer(descriptor);
            var map = deserializer.Deserialize(data);

            // Assert
            Assert.AreEqual(4, map.Keys.Count);
            Assert.AreEqual(customer.Id, map["Id"]);
            Assert.AreEqual(customer.FirstName, map["FirstName"]);
            Assert.AreEqual(customer.Surname, map["Surname"]);
            Assert.AreEqual((int)customer.Type, map["Type"]);
        }

        [TestMethod]
        public void BasicMessageToObject()
        {
            // Arrange
            var expectedCustomer = new Customer
            {
                Id = 1,
                FirstName = "Kawai",
                Surname = "Wong",
                Type = Customer.Types.CustomerType.Vip
            };

            var data = expectedCustomer.ToByteArray();
            var descriptor = DescriptorHelper.Read("Customer.pb");

            // Act
            var deserializer = new Deserializer(descriptor);
            var customer = deserializer.Deserialize<Dtos.Customer>(data);

            // Assert
            Assert.AreEqual(expectedCustomer.Id, customer.Id);
            Assert.AreEqual(expectedCustomer.FirstName, customer.FirstName);
            Assert.AreEqual(expectedCustomer.Surname, customer.Surname);
            Assert.AreEqual(CustomerType.Vip, customer.Type);
            // Should ignore fields that don't exist and default them
            Assert.AreEqual(null, customer.LastName);
        }

        [TestMethod]
        public void BasicMessageMiddleFieldNotSetToObject()
        {
            // Arrange
            var expectedCustomer = new Customer
            {
                Id = 1,
                FirstName = "Kawai",
                Type = Customer.Types.CustomerType.Vip
            };

            var data = expectedCustomer.ToByteArray();
            var descriptor = DescriptorHelper.Read("Customer.pb");

            // Act
            var deserializer = new Deserializer(descriptor);
            var customer = deserializer.Deserialize<Dtos.Customer>(data);

            // Assert
            Assert.AreEqual(expectedCustomer.Id, customer.Id);
            Assert.AreEqual(expectedCustomer.FirstName, customer.FirstName);
            Assert.AreEqual(null, customer.Surname);
            Assert.AreEqual(CustomerType.Vip, customer.Type);
            // Should ignore fields that don't exist and default them
            Assert.AreEqual(null, customer.LastName);
        }

        [TestMethod]
        public void EdwinsMessageWithJumbledUpFieldsMiddleFieldNotSetToMap()
        {
            // Arrange
            var person = new Person
            {
                Name = "Luke Skywalker",
                Email = "luke.skywalker@jedi.com"
            };

            var data = person.ToByteArray();
            var descriptor = DescriptorHelper.Read("EdwinsExample.pb");

            // Act
            var deserializer = new Deserializer(descriptor);
            var map = deserializer.Deserialize(data);

            // Assert
            Assert.AreEqual(null, map["id"]);
            Assert.AreEqual(person.Name, map["name"]);
            Assert.AreEqual(person.Email, map["email"]);
        }

        [TestMethod]
        public void EdwinsMessageWithJumbledUpFieldsLastFieldNotSetToMap()
        {
            // Arrange
            var expectedPerson = new Person
            {
                Id = 10,
                Name = "Luke Skywalker"
            };

            var data = expectedPerson.ToByteArray();
            var descriptor = DescriptorHelper.Read("EdwinsExample.pb");

            // Act
            var deserializer = new Deserializer(descriptor);
            var person = deserializer.Deserialize<Dtos.EdwinPerson>(data);

            // Assert
            Assert.AreEqual(expectedPerson.Id, person.Id);
            Assert.AreEqual(expectedPerson.Name, person.Name);
            Assert.AreEqual(null, person.Email);
        }

        [TestMethod]
        public void EdwinsMessageWithMiddleFieldNotSetToObjectOG()
        {
            // Arrange
            var personData = "0A 0E 4C 75 6B 65 20 53 6B 79 77 61 6C 6B 65 72 1A 17 6C 75 6B 65 2E 73 6B 79 77 61 6C 6B 65 72 40 6A 65 64 69 2E 63 6F 6D".Split(' ');
            var personMessageDescriptor = "0A 5A 0A 0C 50 65 72 73 6F 6E 2E 70 72 6F 74 6F 22 42 0A 06 50 65 72 73 6F 6E 12 12 0A 04 6E 61 6D 65 18 01 20 01 28 09 52 04 6E 61 6D 65 12 0E 0A 02 69 64 18 02 20 01 28 05 52 02 69 64 12 14 0A 05 65 6D 61 69 6C 18 03 20 01 28 09 52 05 65 6D 61 69 6C 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = personData.Select(x => Convert.ToByte(x, 16)).ToArray();
            var descriptor = personMessageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Act
            var deserializer = new Deserializer(descriptor);
            var map = deserializer.Deserialize(data);

            // Assert
            Assert.AreEqual(null, map["id"]);
            Assert.AreEqual("Luke Skywalker", map["name"]);
            Assert.AreEqual("luke.skywalker@jedi.com", map["email"]);
        }

        [TestMethod]
        public void EdwinsMessageWithLastFieldNotSetToObjectOG()
        {
            // Arrange
            var personData = "0A 0E 4C 75 6B 65 20 53 6B 79 77 61 6C 6B 65 72 10 0A".Split(' ');
            var personMessageDescriptor = "0A 5A 0A 0C 50 65 72 73 6F 6E 2E 70 72 6F 74 6F 22 42 0A 06 50 65 72 73 6F 6E 12 12 0A 04 6E 61 6D 65 18 01 20 01 28 09 52 04 6E 61 6D 65 12 0E 0A 02 69 64 18 02 20 01 28 05 52 02 69 64 12 14 0A 05 65 6D 61 69 6C 18 03 20 01 28 09 52 05 65 6D 61 69 6C 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = personData.Select(x => Convert.ToByte(x, 16)).ToArray();
            var descriptor = personMessageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Act
            var deserializer = new Deserializer(descriptor);
            var person = deserializer.Deserialize<Dtos.EdwinPerson>(data);

            // Assert
            Assert.AreEqual(10, person.Id);
            Assert.AreEqual("Luke Skywalker", person.Name);
            Assert.AreEqual(null, person.Email);
        }
    }
}
