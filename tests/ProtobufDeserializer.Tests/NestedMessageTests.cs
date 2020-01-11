using Google.Protobuf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtobufDeserializer.Tests.Helpers;
using ProtobufDeserializer.V2;

namespace ProtobufDeserializer.Tests
{
    [TestClass]
    public class NestedMessageTests
    {
        [TestMethod]
        public void NestedMessageInsideMessageToDictionary()
        {
            // Arrange
            var nestedObject = new FooInside
            {
                Id = 1,
                FirstName = "Kawai",
                Surname = "Wong",
                NestedMessage = new FooInside.Types.BarInside
                {
                    Star = "tested",
                    Fighter = "hehehe"
                }
            };

            var data = nestedObject.ToByteArray();
            var descriptor = DescriptorHelper.Read("NestedInside.pb");

            // Act
            var deserializer = new Deserializer(descriptor);
            var map = deserializer.Deserialize(data);

            // Assert
            Assert.AreEqual(6, map.Keys.Count);
            Assert.AreEqual(nestedObject.Id, map["Id"]);
            Assert.AreEqual(nestedObject.FirstName, map["FirstName"]);
            Assert.AreEqual(nestedObject.Surname, map["Surname"]);
            Assert.AreEqual(nestedObject.NestedMessage.Star, map["Star"]);
            Assert.AreEqual(nestedObject.NestedMessage.Fighter, map["Fighter"]);
        }

        [TestMethod]
        public void NestedMessageInsideMessageToObject()
        {
            // Arrange
            var nestedObject = new FooInside
            {
                Id = 1,
                FirstName = "Kawai",
                Surname = "Wong",
                NestedMessage = new FooInside.Types.BarInside
                {
                    Star = "tested",
                    Fighter = "hehehe"
                }
            };

            var data = nestedObject.ToByteArray();
            var descriptor = DescriptorHelper.Read("NestedInside.pb");

            // Act
            var deserializer = new Deserializer(descriptor);
            var foo = deserializer.Deserialize<Dtos.Foo>(data);

            // Assert
            Assert.AreEqual(nestedObject.Id, foo.Id);
            Assert.AreEqual(nestedObject.FirstName, foo.FirstName);
            Assert.AreEqual(nestedObject.Surname, foo.Surname);
            Assert.AreEqual(nestedObject.NestedMessage.Star, foo.NestedMessage.Star);
            Assert.AreEqual(nestedObject.NestedMessage.Fighter, foo.NestedMessage.Fighter);
        }

        [TestMethod]
        public void NestedMessageOutsideMessageToDictionary()
        {
            // Arrange
            var nestedObject = new FooOutside
            {
                Id = 1,
                FirstName = "Kawai",
                Surname = "Wong",
                NestedMessage = new BarOutside
                {
                    Star = "tested",
                    Fighter = "hehehe"
                }
            };

            var data = nestedObject.ToByteArray();
            var descriptor = DescriptorHelper.Read("NestedOutside.pb");

            // Act
            var deserializer = new Deserializer(descriptor);
            var map = deserializer.Deserialize(data);

            // Assert
            Assert.AreEqual(6, map.Keys.Count);
            Assert.AreEqual(nestedObject.Id, map["Id"]);
            Assert.AreEqual(nestedObject.FirstName, map["FirstName"]);
            Assert.AreEqual(nestedObject.Surname, map["Surname"]);
            Assert.AreEqual(nestedObject.NestedMessage.Star, map["Star"]);
            Assert.AreEqual(nestedObject.NestedMessage.Fighter, map["Fighter"]);
        }

        [TestMethod]
        public void NestedMessageOutsideMessageToObject()
        {
            // Arrange
            var nestedObject = new FooOutside
            {
                Id = 1,
                FirstName = "Kawai",
                Surname = "Wong",
                NestedMessage = new BarOutside
                {
                    Star = "tested",
                    Fighter = "hehehe"
                }
            };

            var data = nestedObject.ToByteArray();
            var descriptor = DescriptorHelper.Read("NestedOutside.pb");

            // Act
            var deserializer = new Deserializer(descriptor);
            var foo = deserializer.Deserialize<Dtos.Foo>(data);

            // Assert
            Assert.AreEqual(nestedObject.Id, foo.Id);
            Assert.AreEqual(nestedObject.FirstName, foo.FirstName);
            Assert.AreEqual(nestedObject.Surname, foo.Surname);
            Assert.AreEqual(nestedObject.NestedMessage.Star, foo.NestedMessage.Star);
            Assert.AreEqual(nestedObject.NestedMessage.Fighter, foo.NestedMessage.Fighter);
        }
    }
}
