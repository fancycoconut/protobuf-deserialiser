using Google.Protobuf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtobufDeserializer.Tests.Helpers;
using System;
using System.Linq;
using ProtobufDeserializer.Tests.Dtos;

namespace ProtobufDeserializer.Tests
{
    [TestClass]
    public class JumbledOrderingTests
    {
        [TestMethod]
        public void EdwinsMessageWithJumbledUpFieldsMiddleFieldNotSet()
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
            var parsedObject = deserializer.Deserialize<EdwinPerson>(data);

            // Assert
            Assert.AreEqual(0, parsedObject.Id);
            Assert.AreEqual(person.Name, parsedObject.Name);
            Assert.AreEqual(person.Email, parsedObject.Email);
        }

        [TestMethod]
        public void EdwinsMessageWithJumbledUpFieldsLastFieldNotSet()
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
            var person = deserializer.Deserialize<EdwinTestScenario1>(data);

            // Assert
            Assert.AreEqual(expectedPerson.Id, person.Id);
            Assert.AreEqual(expectedPerson.Name, person.Name);
            Assert.AreEqual(null, person.Email);
        }

        [TestMethod]
        public void EdwinsMessageWithMiddleFieldNotSetToObjectOg()
        {
            // Arrange
            var personData = "0A 0E 4C 75 6B 65 20 53 6B 79 77 61 6C 6B 65 72 1A 17 6C 75 6B 65 2E 73 6B 79 77 61 6C 6B 65 72 40 6A 65 64 69 2E 63 6F 6D".Split(' ');
            var personMessageDescriptor = "0A 5A 0A 0C 50 65 72 73 6F 6E 2E 70 72 6F 74 6F 22 42 0A 06 50 65 72 73 6F 6E 12 12 0A 04 6E 61 6D 65 18 01 20 01 28 09 52 04 6E 61 6D 65 12 0E 0A 02 69 64 18 02 20 01 28 05 52 02 69 64 12 14 0A 05 65 6D 61 69 6C 18 03 20 01 28 09 52 05 65 6D 61 69 6C 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = personData.Select(x => Convert.ToByte(x, 16)).ToArray();
            var descriptor = personMessageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Act
            var deserializer = new Deserializer(descriptor);
            var person = deserializer.Deserialize<EdwinTestScenario1>(data);

            // Assert
            Assert.AreEqual(0, person.Id);
            Assert.AreEqual("Luke Skywalker", person.Name);
            Assert.AreEqual("luke.skywalker@jedi.com", person.Email);
        }

        [TestMethod]
        public void EdwinsMessageWithLastFieldNotSetToObjectOg()
        {
            // Arrange
            var personData = "0A 0E 4C 75 6B 65 20 53 6B 79 77 61 6C 6B 65 72 10 0A".Split(' ');
            var personMessageDescriptor = "0A 5A 0A 0C 50 65 72 73 6F 6E 2E 70 72 6F 74 6F 22 42 0A 06 50 65 72 73 6F 6E 12 12 0A 04 6E 61 6D 65 18 01 20 01 28 09 52 04 6E 61 6D 65 12 0E 0A 02 69 64 18 02 20 01 28 05 52 02 69 64 12 14 0A 05 65 6D 61 69 6C 18 03 20 01 28 09 52 05 65 6D 61 69 6C 62 06 70 72 6F 74 6F 33".Split(' ');
            var data = personData.Select(x => Convert.ToByte(x, 16)).ToArray();
            var descriptor = personMessageDescriptor.Select(x => Convert.ToByte(x, 16)).ToArray();

            // Act
            var deserializer = new Deserializer(descriptor);
            var person = deserializer.Deserialize<EdwinTestScenario1>(data);

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
            var example = deserializer.Deserialize<JumbledFooInsideDeserialiserDto>(data);

            // Assert
            Assert.AreEqual(nestedObject.Id, example.Id);
            Assert.AreEqual(nestedObject.FirstName, example.FirstName);
            Assert.AreEqual(nestedObject.Surname, example.Surname);
            Assert.AreEqual(nestedObject.NestedMessage.Star, example.NestedMessage.Star);
            Assert.AreEqual(nestedObject.NestedMessage.Fighter, example.NestedMessage.Fighter);
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
            var example = deserializer.Deserialize<JumbledFooOutsideDeserialiserDto>(data);

            // Assert
            Assert.AreEqual(nestedObject.Id, example.Id);
            Assert.AreEqual(nestedObject.FirstName, example.FirstName);
            Assert.AreEqual(nestedObject.Surname, example.Surname);
            Assert.AreEqual(nestedObject.NestedMessage.Star, example.NestedMessage.Star);
            Assert.AreEqual(nestedObject.NestedMessage.Fighter, example.NestedMessage.Fighter);
        }

        [TestMethod]
        public void JumbledMessageWithDuplicateFieldNameParsingTest()
        {
            // Arrange
            var jumbledMessage = new Cats
            {
                Field1 = 919,
                Pet = new Tiger
                {
                    Field1 = 42,
                    Field2 = 77,
                    Field3 = 99999
                }
            };

            var data = jumbledMessage.ToByteArray();
            var descriptor = DescriptorHelper.Read("JumbledMessageOrderBasic.pb");

            // Act
            var deserializer = new Deserializer(descriptor);
            var example = deserializer.Deserialize<JumbledMessageOrderBasicCatDto>(data);

            // Assert
            Assert.AreEqual(jumbledMessage.Field1, example.Field1);
            Assert.AreEqual(jumbledMessage.Pet.Field1, example.Pet.Field1);
            Assert.AreEqual(jumbledMessage.Pet.Field2, example.Pet.Field2);
            Assert.AreEqual(jumbledMessage.Pet.Field3, example.Pet.Field3);
        }

        // Below are classes used for deserialising a protobuf message
        // The fields in these classes must be defined in the same order as the actual fields in the schema

        private class EdwinTestScenario1
        {
            public string Name { get; set; }
            public int Id { get; set; }
            public string Email { get; set; }
        }

        private class JumbledFooInsideDeserialiserDto
        {
            public int Id { get; set; }
            public string Surname { get; set; }
            public JumbledNestedBarInsideDto NestedMessage { get; set; }
            public string FirstName { get; set; }
        }

        private class JumbledNestedBarInsideDto
        {
            public string Fighter { get; set; }
            public string Star { get; set; }
        }

        private class JumbledFooOutsideDeserialiserDto
        {
            public int Id { get; set; }
            public JumbledNestedBarOutsideDto NestedMessage { get; set; }
            public string FirstName { get; set; }
            public string Surname { get; set; }
        }

        private class JumbledNestedBarOutsideDto
        {
            public string Fighter { get; set; }
            public string Star { get; set; }
        }

        private class JumbledMessageOrderBasicCatDto
        {
            public int Field1 { get; set; }
            public JumbledMessageOrderBasicTigerDto Pet { get; set; }
        }

        private class JumbledMessageOrderBasicTigerDto
        {
            public int Field1 { get; set; }
            public int Field2 { get; set; }
            public int Field3 { get; set; }
        }
    }
}
