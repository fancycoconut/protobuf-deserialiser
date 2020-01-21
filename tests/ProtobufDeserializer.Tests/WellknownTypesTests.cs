using System;
using System.Linq;
using Fph.Kato.Info.V1Beta1;
using Google.Protobuf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtobufDeserializer.Tests.Dtos;
using ProtobufDeserializer.Tests.Helpers;

namespace ProtobufDeserializer.Tests
{
    [TestClass]
    public class WellknownTypesTests
    {
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
            var deserializer = new Deserializer(descriptor);
            var info = deserializer.Deserialize<InfoPascalCase>(data);

            // Assert
            Assert.AreEqual(message.Serial, info.Serial);
            Assert.AreEqual(message.Family, info.Family);
            Assert.AreEqual(message.Model, info.Model);
        }

        [TestMethod]
        public void BasicWellKnownTypesToObjectWithLowerCaseFieldNames()
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
            var deserializer = new Deserializer(descriptor);
            var info = deserializer.Deserialize<InfoLowerCase>(data);

            // Assert
            Assert.AreEqual(message.Serial, info.serial);
            Assert.AreEqual(message.Family, info.family);
            Assert.AreEqual(message.Model, info.model);
        }

        [TestMethod]
        public void BasicWellKnownTypesToObjectWithUpperCaseFieldNames()
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
            var deserializer = new Deserializer(descriptor);
            var info = deserializer.Deserialize<InfoUpperCase>(data);

            // Assert
            Assert.AreEqual(message.Serial, info.SERIAL);
            Assert.AreEqual(message.Family, info.FAMILY);
            Assert.AreEqual(message.Model, info.MODEL);
        }
    }
}
