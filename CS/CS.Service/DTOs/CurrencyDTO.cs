using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CS.Service.DTOs
{
    [DataContract]
    public class CurrencyDTO
    {
        private string _name;
        private string _code;

        [DataMember]
        public string Name { get { return _name; } set { _name = value; } }
        [DataMember]
        public string Code { get { return _code; } set { _code = value; } }
    }
}