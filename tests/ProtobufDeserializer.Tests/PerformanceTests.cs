using Fph.Kato.Info.V1Beta1;
using Google.Protobuf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtobufDeserializer.Tests.Dtos;
using ProtobufDeserializer.Tests.Helpers;
using Tests.Sample.Jumbled.Fields;
using Customer = Tests.Sample.Customer.Customer;
using Foo = Tests.Sample.Foo.Foo;

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
            var message = new Foo
            {
                Id = 1,
                FirstName = "Kawai",
                Surname = "Wong",
                NestedMessage = new Foo.Types.Bar
                {
                    Star = "tested",
                    Fighter = "hehehe"
                }
            };

            var data = message.ToByteArray();
            var descriptor = DescriptorHelper.Read("Foo.pb");

            // Act
            var foo = new Dtos.Foo();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (var i = 0; i < 1000000; i++)
            {
                var deserializer = new Deserializer(descriptor);
                foo = deserializer.Deserialize<Dtos.Foo>(data);
            }
            watch.Stop();

            // Report
            var elapsedMs = watch.ElapsedMilliseconds;
            System.Diagnostics.Debug.WriteLine($"Elapsed Time: {elapsedMs}");

            //Assert.AreEqual(message.Id, foo.Id);
            //Assert.AreEqual(message.FirstName, foo.FirstName);
            //Assert.AreEqual(message.Surname, foo.Surname);
            //Assert.AreEqual(message.NestedMessage.Star, foo.NestedMessage.Star);
            //Assert.AreEqual(message.NestedMessage.Fighter, foo.NestedMessage.Fighter);
        }

        [TestMethod]
        public void NestedMessageInsideMessageToObjectPerformanceOneHundredThousand()
        {
            // Arrange
            var message = new Foo
            {
                Id = 1,
                FirstName = "Kawai",
                Surname = "Wong",
                NestedMessage = new Foo.Types.Bar
                {
                    Star = "tested",
                    Fighter = "hehehe"
                }
            };

            var data = message.ToByteArray();
            var descriptor = DescriptorHelper.Read("Foo.pb");

            // Act
            var foo = new Dtos.Foo();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            
            for (var i = 0; i < 100000; i++)
            {
                var deserializer = new Deserializer(descriptor);
                foo = deserializer.Deserialize<Dtos.Foo>(data);
            }
            watch.Stop();

            // Report
            var elapsedMs = watch.ElapsedMilliseconds;
            System.Diagnostics.Debug.WriteLine($"Elapsed Time: {elapsedMs}");

            //Assert.AreEqual(message.Id, foo.Id);
            //Assert.AreEqual(message.FirstName, foo.FirstName);
            //Assert.AreEqual(message.Surname, foo.Surname);
            //Assert.AreEqual(message.NestedMessage.Star, foo.NestedMessage.Star);
            //Assert.AreEqual(message.NestedMessage.Fighter, foo.NestedMessage.Fighter);
        }

        [TestMethod]
        public void NestedMessageInsideMessageToObjectPerformanceTenThousand()
        {
            // Arrange
            var message = new Foo
            {
                Id = 1,
                FirstName = "Kawai",
                Surname = "Wong",
                NestedMessage = new Foo.Types.Bar
                {
                    Star = "tested",
                    Fighter = "hehehe"
                }
            };

            var data = message.ToByteArray();
            var descriptor = DescriptorHelper.Read("Foo.pb");

            // Act
            var foo = new Dtos.Foo();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            
            for (var i = 0; i < 10000; i++)
            {
                var deserializer = new Deserializer(descriptor);
                foo = deserializer.Deserialize<Dtos.Foo>(data);
            }
            watch.Stop();

            // Report
            var elapsedMs = watch.ElapsedMilliseconds;
            System.Diagnostics.Debug.WriteLine($"Elapsed Time: {elapsedMs}");

            //Assert.AreEqual(message.Id, foo.Id);
            //Assert.AreEqual(message.FirstName, foo.FirstName);
            //Assert.AreEqual(message.Surname, foo.Surname);
            //Assert.AreEqual(message.NestedMessage.Star, foo.NestedMessage.Star);
            //Assert.AreEqual(message.NestedMessage.Fighter, foo.NestedMessage.Fighter);
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
            var message = new Info
            {
                Serial = "My Serial Number...",
                Family = "SleepStyle",
                Model = "Model"
            };

            var data = message.ToByteArray();
            var descriptor = DescriptorHelper.Read("Info.pb");

            // Act
            var info = new InfoPascalCase();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            
            for (var i = 0; i < 1000000; i++)
            {
                var deserializer = new Deserializer(descriptor);
                info = deserializer.Deserialize<InfoPascalCase>(data);
            }
            watch.Stop();

            // Report
            var elapsedMs = watch.ElapsedMilliseconds;
            System.Diagnostics.Debug.WriteLine($"Elapsed Time: {elapsedMs}");

            //Assert.AreEqual(message.Serial, info.Serial);
            //Assert.AreEqual(message.Family, info.Family);
            //Assert.AreEqual(message.Model, info.Model);
        }
    }
}
