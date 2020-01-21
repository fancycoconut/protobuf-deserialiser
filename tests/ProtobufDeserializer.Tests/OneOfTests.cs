using System;
using System.Linq;
using Google.Protobuf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtobufDeserializer.Tests.Helpers;

namespace ProtobufDeserializer.Tests
{
    [TestClass]
    public class OneOfTests
    {
        [TestMethod]
        public void BasicOneOfWithOneFieldUsedToObjectTest()
        {
            // Arrange
            var oneOfMessage = new OneOfExample
            {
                Key = "one of test key",
                IntValue = 42
            };
            var data = oneOfMessage.ToByteArray();
            var descriptor = DescriptorHelper.Read("OneOfExample.pb");

            // Act
            var deserializer = new Deserializer(descriptor);
            var example = deserializer.Deserialize< OneOfExampleDeserialiseDto>(data);

            // Assert
            Assert.AreEqual("one of test key", example.key);
            Assert.AreEqual(false, example.bool_value);
            Assert.AreEqual(42, example.int_value);
            Assert.AreEqual((uint)0, example.uint_value);
            Assert.AreEqual(0f, example.float_value);
            Assert.AreEqual(null, example.string_value);
            Assert.AreEqual(null, example.byte_value);
        }

        [TestMethod]
        public void BasicOneOfWithTwoFieldsUsedToObjectTest()
        {
            // Arrange
            var oneOfMessage = new OneOfExample
            {
                Key = "one of test key",
                IntValue = 42,
                FloatValue = 3.14f,
            };
            var data = oneOfMessage.ToByteArray();
            var descriptor = DescriptorHelper.Read("OneOfExample.pb");

            // Act
            var deserializer = new Deserializer(descriptor);
            var example = deserializer.Deserialize<OneOfExampleDeserialiseDto>(data);

            // Assert
            Assert.AreEqual("one of test key", example.key);
            Assert.AreEqual(false, example.bool_value);
            Assert.AreEqual(0, example.int_value);
            Assert.AreEqual((uint)0, example.uint_value);
            Assert.AreEqual(3.14f, example.float_value);
            Assert.AreEqual(null, example.string_value);
            Assert.AreEqual(null, example.byte_value);
        }

        private class OneOfExampleDeserialiseDto
        {
            public string key { get; set; }
            public bool bool_value { get; set; }
            public int int_value { get; set; }
            public uint uint_value { get; set; }
            public float float_value { get; set; }
            public string string_value { get; set; }
            public byte[] byte_value { get; set; }
        }
    }
}
