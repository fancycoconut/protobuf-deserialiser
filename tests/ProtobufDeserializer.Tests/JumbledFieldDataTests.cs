using Google.Protobuf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtobufDeserializer.Tests.Dtos;
using ProtobufDeserializer.Tests.Helpers;
using System;
using System.Linq;
using Customer = Tests.Sample.Customer.Customer;

namespace ProtobufDeserializer.Tests
{
    [TestClass]
    public class JumbledFieldDataTests
    {
        [TestMethod]
        public void ProtoBinaryJumbledFieldsData()
        {
            // Arrange
            var expectedCustomer = new Customer
            {
                Id = 1,
                FirstName = "Kawai",
                Surname = "Wong",
                Type = Customer.Types.CustomerType.Vip
            };

            var rawBytes = "18,5,75,97,119,97,105,8,1,26,4,87,111,110,103,32,1".Split(',');
            var data = rawBytes.Select(x => Convert.ToByte(x)).ToArray();
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
    }
}
