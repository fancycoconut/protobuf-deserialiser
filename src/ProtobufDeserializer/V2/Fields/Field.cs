using System;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace ProtobufDeserializer.Fields
{
    public class Field
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public string TypeName { get; set; }
        public FieldDescriptorProto.Types.Type Type { get; set; }
        public object Value { get; set; }

        internal readonly CodedInputStream input;

        public Field(CodedInputStream input)
        {
            this.input = input;
        }

        public virtual void ReadValue()
        {
            throw new NotImplementedException();
        }
    }
}
