﻿using System;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace ProtobufDeserializer.Fields
{
    public interface IField
    {
        string Name { get; set; }
        int Number { get; set; }
        string TypeName { get; set; }
        FieldDescriptorProto.Types.Label Label { get; set; }
        FieldDescriptorProto.Types.Type Type { get; set; }
        object Value { get; set; }

        void ReadValue();
    }

    public abstract class Field : IField
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public string TypeName { get; set; }
        public FieldDescriptorProto.Types.Label Label { get; set; }
        public FieldDescriptorProto.Types.Type Type { get; set; }
        public object Value { get; set; }

        internal readonly CodedInputStream input;


        protected Field(CodedInputStream input)
        {
            this.input = input;
        }

        public virtual void ReadValue()
        {
            throw new NotImplementedException();
        }

        protected bool CurrentFieldNumberIsCorrect()
        {
            var tag = input.PeekTag();

            // (field_number << 3) | wire_type
            // Top 5 bits are field number
            var t = tag & 0xF8;
            // Bottom 3 bits give the wire type
            var wireType = tag & 0x7;

            // Wire Types
            // 0    Varint int32, int64, uint32, uint64, sint32, sint64, bool, enum
            // 1    64-bit fixed64, sfixed64, double
            // 2	Length-delimited string, bytes, embedded messages, packed repeated fields
            // 3	Start group groups(deprecated)
            // 4	End group   groups(deprecated)
            // 5	32-bit fixed32, sfixed32, float

            var fieldNumber = t >> 3;
            return this.Number == fieldNumber;
        }
    }
}
