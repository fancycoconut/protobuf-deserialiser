using Google.Protobuf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtobufDeserializer.Tests.Helpers;
using ProtobufDeserializer.V2;
using System;
using System.Linq;

namespace ProtobufDeserializer.Tests
{
    [TestClass]
    public class JumbledFieldOrderingTests
    {
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

        [TestMethod]
        public void NestedMessageInsideJumbledFieldsTest()
        {
            // Arrange
            var nestedObject = new JumbledFooInside
            {
                Id = 1,
                FirstName = "Kawai",
                Surname = "Wong",
                NestedMessage = new JumbledFooInside.Types.JumbledBarInside
                {
                    Star = "tested",
                    Fighter = "hehehe"
                }
            };

            var data = nestedObject.ToByteArray();
            var descriptor = DescriptorHelper.Read("JumbledNestedInside.pb");

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
        public void NestedMessageOutsideJumbledFieldTest()
        {
            // Arrange
            var nestedObject = new JumbledFooOutside
            {
                Id = 1,
                FirstName = "Kawai",
                Surname = "Wong",
                NestedMessage = new JumbledBarOutside
                {
                    Star = "tested",
                    Fighter = "hehehe"
                }
            };

            var data = nestedObject.ToByteArray();
            var descriptor = DescriptorHelper.Read("JumbledNestedOutside.pb");

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
    }
}
