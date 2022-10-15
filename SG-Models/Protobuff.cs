using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SG_Models
{
    public class ProtoBuff
    {
        public static byte[] ProtoSerialize<T>(T record) where T : class
        {
            using var stream = new MemoryStream();
            Serializer.Serialize(stream, record);
            return stream.ToArray();
        }
    }
}
