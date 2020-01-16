using System;
using System.Linq;
using Google.Protobuf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtobufDeserializer.Tests.Helpers;

namespace ProtobufDeserializer.Tests
{
    [TestClass]
    public class PhilsTestscs
    {
        [TestMethod]
        public void EncodedMessageHasSameFieldAppearingTwiceInDataTest()
        {
            // Arrange
            //var msg = new PhilEdge1
            //{
            //    Field1 = 2000
            //};

            //var test = msg.ToByteArray();
            // Same field Id first value is 919, second time is 2000
            var rawBytes = "8,151,7,8,208,15".Split(",");
            var data = rawBytes.Select(x => Convert.ToByte(x)).ToArray();
            var descriptor = DescriptorHelper.Read("PhilsEdgeCase1.pb");

            // Act
            var deserializer = new Deserializer(descriptor);
            var instance = deserializer.Deserialize<PhilEdge1Dto>(data);

            // Assert
            // If the same field data is appearing twice it should take the last value
            // https://developers.google.com/protocol-buffers/docs/encoding#optional
            // Normally, an encoded message would never have more than one instance of a non-repeated field.
            // However, parsers are expected to handle the case in which they do. For numeric types and strings,
            // if the same field appears multiple times, the parser accepts the last value it sees.
            Assert.AreEqual(2000, instance.Field1);
        }

        private class PhilEdge1Dto
        {
            public int Field1 { get; set; }
            public int Field2 { get; set; }
        }
    }
}
