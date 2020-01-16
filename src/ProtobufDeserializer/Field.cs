using System;
using System.Collections.Generic;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace ProtobufDeserializer
{
    public interface IField
    {
        string Name { get; set; }
        int FieldNumber { get; set; }
        string TypeName { get; set; }
        FieldDescriptorProto.Types.Label Label { get; set; }
        FieldDescriptorProto.Types.Type Type { get; set; }

        string MessageName { get; set; }

        object ReadValue(CodedInputStream input);
        // Maybe does not need to be in this level...
        void WriteValue(CodedOutputStream output);
    }

    public abstract class Field : IField
    {
        public string Name { get; set; }
        public int FieldNumber { get; set; }
        public string TypeName { get; set; }
        public FieldDescriptorProto.Types.Label Label { get; set; }
        public FieldDescriptorProto.Types.Type Type { get; set; }

        public string MessageName { get; set; }

        public virtual object ReadValue(CodedInputStream input)
        {
            throw new NotImplementedException("");
        }

        public virtual void WriteValue(CodedOutputStream output)
        {

        }

        // Do not remove just yet... I want to keep the logic to get wire type and field number from tag
        //protected bool CurrentFieldNumberIsCorrect(CodedInputStream input)
        //{
        //    return true;

        //    var tag = input.PeekTag();

        //    // (field_number << 3) | wire_type
        //    // Top 5 bits are field number
        //    var t = tag & 0xF8;
        //    // Bottom 3 bits give the wire type
        //    //var wireType = tag & 0x7;

        //    var fieldNumber = t >> 3;
        //    return this.FieldNumber == fieldNumber;
        //}

        protected IEnumerable<T> ReadPackedRepeated<T>(CodedInputStream input, Func<T> readInput)
        {
            var tag = input.ReadTag();
            var length = input.ReadLength();

            var list = new List<T>();
            for (var i = 0; i < length; i++)
            {
                list.Add(readInput());
            }

            return list;
        }

        protected IEnumerable<T> ReadUnpackedRepeated<T>(CodedInputStream input, Func<T> readInput)
        {
            uint tag;
            var list = new List<T>();
            while ((tag = input.PeekTag()) != 0)
            {
                var t = tag & 0xF8;
                var fieldNumber = t >> 3;

                if (fieldNumber != this.FieldNumber) break;

                input.ReadTag();
                list.Add(readInput());
            }

            return list;
        }
    }
}
