using System;
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
    }
}
