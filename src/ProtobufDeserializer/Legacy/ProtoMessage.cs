using System;
using System.Collections.Generic;

namespace ProtobufDeserializer.Legacy.V1
{
    [Obsolete("This implementation is deprecated")]
    public class ProtoMessage
    {
        public string Name { get; set; }
        public IEnumerable<FieldInfo> Fields { get; set; }


        public Dictionary<string, object> ObjectMap { get; set; }



        //public IEnumerable<ProtoMessage> NestedMessages { get; set; }
    }
}
