using System;
using System.Linq;
using Google.Protobuf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtobufDeserializer.Tests.Dtos;
using ProtobufDeserializer.Tests.Helpers;

namespace ProtobufDeserializer.Tests
{
    [TestClass]
    public class PerformanceTests
    {
        [TestMethod]
        public void BasicMessageToObjectPerformanceOneMillion()
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
            var customer = new Dtos.Customer();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (var i = 0; i < 1000000; i++)
            {
                var deserializer = new Deserializer(descriptor);
                customer = deserializer.Deserialize<Dtos.Customer>(data);
            }
            watch.Stop();

            // Report
            var elapsedMs = watch.ElapsedMilliseconds;
            System.Diagnostics.Debug.WriteLine($"Elapsed Time: {elapsedMs}");

            Assert.AreEqual(expectedCustomer.Id, customer.Id);
            Assert.AreEqual(expectedCustomer.FirstName, customer.FirstName);
            Assert.AreEqual(expectedCustomer.Surname, customer.Surname);
            Assert.AreEqual(null, customer.LastName);
            Assert.AreEqual(CustomerType.Vip, customer.Type);
        }

        [TestMethod]
        public void BasicMessageToObjectPerformanceOneHundredThousand()
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
            var customer = new Dtos.Customer();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (var i = 0; i < 10000; i++)
            {
                var deserializer = new Deserializer(descriptor);
                customer = deserializer.Deserialize<Dtos.Customer>(data);
            }
            watch.Stop();

            // Report
            var elapsedMs = watch.ElapsedMilliseconds;
            System.Diagnostics.Debug.WriteLine($"Elapsed Time: {elapsedMs}");

            //Assert.AreEqual(expectedCustomer.Id, customer.Id);
            //Assert.AreEqual(expectedCustomer.FirstName, customer.FirstName);
            //Assert.AreEqual(expectedCustomer.Surname, customer.Surname);
            //Assert.AreEqual(null, customer.LastName);
            //Assert.AreEqual(CustomerType.Vip, customer.Type);
        }

        [TestMethod]
        public void BasicMessageToObjectPerformanceTenThousand()
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
            var customer = new Dtos.Customer();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            
            for (var i = 0; i < 10000; i++)
            {
                var deserializer = new Deserializer(descriptor);
                customer = deserializer.Deserialize<Dtos.Customer>(data);
            }
            watch.Stop();

            // Report
            var elapsedMs = watch.ElapsedMilliseconds;
            System.Diagnostics.Debug.WriteLine($"Elapsed Time: {elapsedMs}");

            //Assert.AreEqual(expectedCustomer.Id, customer.Id);
            //Assert.AreEqual(expectedCustomer.FirstName, customer.FirstName);
            //Assert.AreEqual(expectedCustomer.Surname, customer.Surname);
            //Assert.AreEqual(null, customer.LastName);
            //Assert.AreEqual(CustomerType.Vip, customer.Type);
        }

        [TestMethod]
        public void NestedMessageInsideMessageToObjectPerformanceOneMillion()
        {
            // Arrange
            var nestedData = "8,1,12,5,4b,61,77,61,69,1a,4,57,6f,6e,67,22,10,a,6,74,65,73,74,65,64,12,6,68,65,68,65,68,65".Split(',');
            var nestedMessageDescriptor = "0A CB 01 0A 0C 4E 65 73 74 65 64 2E 70 72 6F 74 6F 22 B2 01 0A 03 46 6F 6F 12 0E 0A 02 49 64 18 01 20 01 28 05 52 02 49 64 12 1C 0A 09 46 69 72 73 74 4E 61 6D 65 18 02 20 01 28 09 52 09 46 69 72 73 74 4E 61 6D 65 12 18 0A 07 53 75 72 6E 61 6D 65 18 03 20 01 28 09 52 07 53 75 72 6E 61 6D 65 12 2E 0A 0D 4E 65 73 74 65 64 4D 65 73 73 61 67 65 18 04 20 01 28 0B 32 08 2E 46 6F 6F 2E 42 61 72 52 0D 4E 65 73 74 65 64 4D 65 73 73 61 67 65 1A 33 0A 03 42 61 72 12 12 0A 04 53 74 61 72 18 01 20 01 28 09 52 04 53 74 61 72 12 18 0A 07 46 69 67 68 74 65 72 18 02 20 01 28 09 52 07 46 69 67 68 74 65 72 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = nestedData.Select(x => Convert.ToByte(x, 16)).ToArray();
            var descriptor = nestedMessageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Act
            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (var i = 0; i < 1000000; i++)
            {
                var deserializer = new Deserializer(descriptor);
                var foo = deserializer.Deserialize<Dtos.Foo>(data);
            }
            watch.Stop();

            // Report
            var elapsedMs = watch.ElapsedMilliseconds;
            System.Diagnostics.Debug.WriteLine($"Elapsed Time: {elapsedMs}");
        }

        [TestMethod]
        public void NestedMessageInsideMessageToObjectPerformanceOneHundredThousand()
        {
            // Arrange
            var nestedData = "8,1,12,5,4b,61,77,61,69,1a,4,57,6f,6e,67,22,10,a,6,74,65,73,74,65,64,12,6,68,65,68,65,68,65".Split(',');
            var nestedMessageDescriptor = "0A CB 01 0A 0C 4E 65 73 74 65 64 2E 70 72 6F 74 6F 22 B2 01 0A 03 46 6F 6F 12 0E 0A 02 49 64 18 01 20 01 28 05 52 02 49 64 12 1C 0A 09 46 69 72 73 74 4E 61 6D 65 18 02 20 01 28 09 52 09 46 69 72 73 74 4E 61 6D 65 12 18 0A 07 53 75 72 6E 61 6D 65 18 03 20 01 28 09 52 07 53 75 72 6E 61 6D 65 12 2E 0A 0D 4E 65 73 74 65 64 4D 65 73 73 61 67 65 18 04 20 01 28 0B 32 08 2E 46 6F 6F 2E 42 61 72 52 0D 4E 65 73 74 65 64 4D 65 73 73 61 67 65 1A 33 0A 03 42 61 72 12 12 0A 04 53 74 61 72 18 01 20 01 28 09 52 04 53 74 61 72 12 18 0A 07 46 69 67 68 74 65 72 18 02 20 01 28 09 52 07 46 69 67 68 74 65 72 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = nestedData.Select(x => Convert.ToByte(x, 16)).ToArray();
            var descriptor = nestedMessageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Act
            var watch = System.Diagnostics.Stopwatch.StartNew();
            
            for (var i = 0; i < 100000; i++)
            {
                var deserializer = new Deserializer(descriptor);
                var foo = deserializer.Deserialize<Dtos.Foo>(data);
            }
            watch.Stop();

            // Report
            var elapsedMs = watch.ElapsedMilliseconds;
            System.Diagnostics.Debug.WriteLine($"Elapsed Time: {elapsedMs}");
        }

        [TestMethod]
        public void NestedMessageInsideMessageToObjectPerformanceTenThousand()
        {
            // Arrange
            var nestedData = "8,1,12,5,4b,61,77,61,69,1a,4,57,6f,6e,67,22,10,a,6,74,65,73,74,65,64,12,6,68,65,68,65,68,65".Split(',');
            var nestedMessageDescriptor = "0A CB 01 0A 0C 4E 65 73 74 65 64 2E 70 72 6F 74 6F 22 B2 01 0A 03 46 6F 6F 12 0E 0A 02 49 64 18 01 20 01 28 05 52 02 49 64 12 1C 0A 09 46 69 72 73 74 4E 61 6D 65 18 02 20 01 28 09 52 09 46 69 72 73 74 4E 61 6D 65 12 18 0A 07 53 75 72 6E 61 6D 65 18 03 20 01 28 09 52 07 53 75 72 6E 61 6D 65 12 2E 0A 0D 4E 65 73 74 65 64 4D 65 73 73 61 67 65 18 04 20 01 28 0B 32 08 2E 46 6F 6F 2E 42 61 72 52 0D 4E 65 73 74 65 64 4D 65 73 73 61 67 65 1A 33 0A 03 42 61 72 12 12 0A 04 53 74 61 72 18 01 20 01 28 09 52 04 53 74 61 72 12 18 0A 07 46 69 67 68 74 65 72 18 02 20 01 28 09 52 07 46 69 67 68 74 65 72 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = nestedData.Select(x => Convert.ToByte(x, 16)).ToArray();
            var descriptor = nestedMessageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Act
            var watch = System.Diagnostics.Stopwatch.StartNew();
            
            for (var i = 0; i < 10000; i++)
            {
                var deserializer = new Deserializer(descriptor);
                var foo = deserializer.Deserialize<Dtos.Foo>(data);
            }
            watch.Stop();

            // Report
            var elapsedMs = watch.ElapsedMilliseconds;
            System.Diagnostics.Debug.WriteLine($"Elapsed Time: {elapsedMs}");
        }

        [TestMethod]
        public void EdwinsMessageToObjectPerformanceOneMillion()
        {
            // Arrange
            var expectedPerson = new Person
            {
                Id = 10,
                Name = "Luke Skywalker",
                Email = "luke.skywalker@jedi.com"
            };

            var data = expectedPerson.ToByteArray();
            var descriptor = DescriptorHelper.Read("EdwinsExample.pb");

            // Act
            var watch = System.Diagnostics.Stopwatch.StartNew();
            
            for (var i = 0; i < 1000000; i++)
            {
                var deserializer = new Deserializer(descriptor);
                var person = deserializer.Deserialize<EdwinPerson>(data);
            }
            watch.Stop();

            // Report
            var elapsedMs = watch.ElapsedMilliseconds;
            System.Diagnostics.Debug.WriteLine($"Elapsed Time: {elapsedMs}");
        }

        [TestMethod]
        public void BasicWellKnownTypesToObjectWithPascalCaseFieldNames()
        {
            // Arrange
            var messageData = "a,15,a,13,4d,79,20,53,65,72,69,61,6c,20,4e,75,6d,62,65,72,2e,2e,2e,12,c,a,a,53,6c,65,65,70,53,74,79,6c,65,1a,7,a,5,4d,6f,64,65,6c".Split(',');
            var data = messageData.Select(x => Convert.ToByte(x, 16)).ToArray();
            var descriptor = DescriptorHelper.Read("Info.pb");

            // Act
            var watch = System.Diagnostics.Stopwatch.StartNew();
            
            for (var i = 0; i < 1000000; i++)
            {
                var deserializer = new Deserializer(descriptor);
                var person = deserializer.Deserialize<InfoPascalCase>(data);
            }
            watch.Stop();

            // Report
            var elapsedMs = watch.ElapsedMilliseconds;
            System.Diagnostics.Debug.WriteLine($"Elapsed Time: {elapsedMs}");
        }
    }
}
