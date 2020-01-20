using System;
using Google.Protobuf.Reflection;

namespace ProtobufDeserializer.Helpers
{
    public class ProtobufHelper
    {
        public static int ComputeFieldNumber(int tag)
        {
            return (tag & 0xF8) >> 3;
        }
    }
}
