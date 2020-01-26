using Google.Protobuf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtobufDeserializer.Tests.Helpers;
using Tests.Sample.Duplicates;
using Tests.Sample.Jumbled;

namespace ProtobufDeserializer.Tests
{
    [TestClass]
    public class DuplicateFieldNameTests
    {
        [TestMethod]
        public void DuplicateFieldNameParsingTest()
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
            var example = deserializer.Deserialize<DuplicateFieldsCatDto>(data);

            // Assert
            Assert.AreEqual(jumbledMessage.Field1, example.Field1);
            Assert.AreEqual(jumbledMessage.Pet.Field1, example.Pet.Field1);
            Assert.AreEqual(jumbledMessage.Pet.Field2, example.Pet.Field2);
            Assert.AreEqual(jumbledMessage.Pet.Field3, example.Pet.Field3);
        }

        [TestMethod]
        public void DuplicateFieldNameOfDifferentTypesTest()
        {
            // Arrange
            var msg = new Duper
            {
                Field1 = "Hello World",
                Pet = new Dup
                {
                    Field1 = 42,
                    Field2 = 77,
                    Field3 = 99999
                }
            };

            var data = msg.ToByteArray();
            var descriptor = DescriptorHelper.Read("DuplicateFieldNamesWithDifferentTypes.pb");

            // Act
            var deserializer = new Deserializer(descriptor);
            var example = deserializer.Deserialize<DuplicateFieldsDiffTypesCatDto>(data);

            // Assert
            Assert.AreEqual(msg.Field1, example.Field1);
            Assert.AreEqual(msg.Pet.Field1, example.Pet.Field1);
            Assert.AreEqual(msg.Pet.Field2, example.Pet.Field2);
            Assert.AreEqual(msg.Pet.Field3, example.Pet.Field3);
        }

        private class DuplicateFieldsCatDto
        {
            public int Field1 { get; set; }
            public DuplicateFieldsTigerDto Pet { get; set; }
        }

        private class DuplicateFieldsTigerDto
        {
            public int Field1 { get; set; }
            public int Field2 { get; set; }
            public int Field3 { get; set; }
        }

        private class DuplicateFieldsDiffTypesCatDto
        {
            public string Field1 { get; set; }
            public DuplicateFieldsDiffTypesTigerDto Pet { get; set; }
        }

        private class DuplicateFieldsDiffTypesTigerDto
        {
            public int Field1 { get; set; }
            public int Field2 { get; set; }
            public int Field3 { get; set; }
        }
    }
}
