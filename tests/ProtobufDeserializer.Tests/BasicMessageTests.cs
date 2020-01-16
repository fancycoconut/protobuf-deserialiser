using Google.Protobuf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtobufDeserializer.Tests.Dtos;
using ProtobufDeserializer.Tests.Helpers;

namespace ProtobufDeserializer.Tests
{
    [TestClass]
    public class BasicMessageTests
    {
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
        public void BasicMessageToMap()
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
            var map = deserializer.DeserializeToMap(data, "");

            // Assert
            Assert.AreEqual(expectedCustomer.Id, map["Id"]);
            Assert.AreEqual(expectedCustomer.FirstName, map["FirstName"]);
            Assert.AreEqual(expectedCustomer.Surname, map["Surname"]);
            Assert.AreEqual((int)CustomerType.Vip, map["Type"]);
        }

        [TestMethod]
        public void BasicMessageMiddleFieldNotSetToMap()
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
            var map = deserializer.DeserializeToMap(data, "");

            // Assert
            Assert.AreEqual(expectedCustomer.Id, map["Id"]);
            Assert.AreEqual(expectedCustomer.FirstName, map["FirstName"]);
            Assert.AreEqual(null, map["Surname"]);
            Assert.AreEqual((int)CustomerType.Vip, map["Type"]);
        }
    }
}
