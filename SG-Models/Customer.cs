using ProtoBuf;
using System;

namespace SG_Models
{
    [ProtoContract]
    public class Customer
    {
        [ProtoMember(1)]
        public int customerid { get; set; }
        [ProtoMember(2)]
        public string firstName { get; set; }
        [ProtoMember(3)]
        public string lastName { get; set; }
        [ProtoMember(4)]
        public string email { get; set; }
    }


}
