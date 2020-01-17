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
            var customerMessageDescriptor = "0A BE 01 0A 0E 43 75 73 74 6F 6D 65 72 2E 70 72 6F 74 6F 22 A3 01 0A 08 43 75 73 74 6F 6D 65 72 12 0E 0A 02 49 64 18 01 20 01 28 05 52 02 49 64 12 1C 0A 09 46 69 72 73 74 4E 61 6D 65 18 02 20 01 28 09 52 09 46 69 72 73 74 4E 61 6D 65 12 18 0A 07 53 75 72 6E 61 6D 65 18 03 20 01 28 09 52 07 53 75 72 6E 61 6D 65 12 2A 0A 04 54 79 70 65 18 04 20 01 28 0E 32 16 2E 43 75 73 74 6F 6D 65 72 2E 43 75 73 74 6F 6D 65 72 54 79 70 65 52 04 54 79 70 65 22 23 0A 0C 43 75 73 74 6F 6D 65 72 54 79 70 65 12 0A 0A 06 4E 6F 72 6D 61 6C 10 00 12 07 0A 03 56 49 50 10 01 62 06 70 72 6F 74 6F 33".Split(' ');
            var descriptor = customerMessageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

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

            //Assert.AreEqual(expectedCustomer.Id, customer.Id);
            //Assert.AreEqual(expectedCustomer.FirstName, customer.FirstName);
            //Assert.AreEqual(expectedCustomer.Surname, customer.Surname);
            //Assert.AreEqual(null, customer.LastName);
            //Assert.AreEqual(CustomerType.Vip, customer.Type);
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
            var customerMessageDescriptor = "0A BE 01 0A 0E 43 75 73 74 6F 6D 65 72 2E 70 72 6F 74 6F 22 A3 01 0A 08 43 75 73 74 6F 6D 65 72 12 0E 0A 02 49 64 18 01 20 01 28 05 52 02 49 64 12 1C 0A 09 46 69 72 73 74 4E 61 6D 65 18 02 20 01 28 09 52 09 46 69 72 73 74 4E 61 6D 65 12 18 0A 07 53 75 72 6E 61 6D 65 18 03 20 01 28 09 52 07 53 75 72 6E 61 6D 65 12 2A 0A 04 54 79 70 65 18 04 20 01 28 0E 32 16 2E 43 75 73 74 6F 6D 65 72 2E 43 75 73 74 6F 6D 65 72 54 79 70 65 52 04 54 79 70 65 22 23 0A 0C 43 75 73 74 6F 6D 65 72 54 79 70 65 12 0A 0A 06 4E 6F 72 6D 61 6C 10 00 12 07 0A 03 56 49 50 10 01 62 06 70 72 6F 74 6F 33".Split(' ');
            var descriptor = customerMessageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

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
            var customerMessageDescriptor = "0A BE 01 0A 0E 43 75 73 74 6F 6D 65 72 2E 70 72 6F 74 6F 22 A3 01 0A 08 43 75 73 74 6F 6D 65 72 12 0E 0A 02 49 64 18 01 20 01 28 05 52 02 49 64 12 1C 0A 09 46 69 72 73 74 4E 61 6D 65 18 02 20 01 28 09 52 09 46 69 72 73 74 4E 61 6D 65 12 18 0A 07 53 75 72 6E 61 6D 65 18 03 20 01 28 09 52 07 53 75 72 6E 61 6D 65 12 2A 0A 04 54 79 70 65 18 04 20 01 28 0E 32 16 2E 43 75 73 74 6F 6D 65 72 2E 43 75 73 74 6F 6D 65 72 54 79 70 65 52 04 54 79 70 65 22 23 0A 0C 43 75 73 74 6F 6D 65 72 54 79 70 65 12 0A 0A 06 4E 6F 72 6D 61 6C 10 00 12 07 0A 03 56 49 50 10 01 62 06 70 72 6F 74 6F 33".Split(' ');
            var descriptor = customerMessageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

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
            var messageDescriptor = "0A F4 01 0A 0A 69 6E 66 6F 2E 70 72 6F 74 6F 12 15 66 70 68 2E 6B 61 74 6F 2E 69 6E 66 6F 2E 76 31 62 65 74 61 31 1A 1E 67 6F 6F 67 6C 65 2F 70 72 6F 74 6F 62 75 66 2F 77 72 61 70 70 65 72 73 2E 70 72 6F 74 6F 22 A6 01 0A 04 49 6E 66 6F 12 34 0A 06 73 65 72 69 61 6C 18 01 20 01 28 0B 32 1C 2E 67 6F 6F 67 6C 65 2E 70 72 6F 74 6F 62 75 66 2E 53 74 72 69 6E 67 56 61 6C 75 65 52 06 73 65 72 69 61 6C 12 34 0A 06 66 61 6D 69 6C 79 18 02 20 01 28 0B 32 1C 2E 67 6F 6F 67 6C 65 2E 70 72 6F 74 6F 62 75 66 2E 53 74 72 69 6E 67 56 61 6C 75 65 52 06 66 61 6D 69 6C 79 12 32 0A 05 6D 6F 64 65 6C 18 03 20 01 28 0B 32 1C 2E 67 6F 6F 67 6C 65 2E 70 72 6F 74 6F 62 75 66 2E 53 74 72 69 6E 67 56 61 6C 75 65 52 05 6D 6F 64 65 6C 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = messageData.Select(x => Convert.ToByte(x, 16)).ToArray();
            var descriptor = messageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

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
