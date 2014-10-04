using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CS.Service.DTOs
{
    [DataContract]
    public class RateDTO
    {
        private string _code;
        private decimal _value;
        private DateTime _date;

        [DataMember]
        public string Code { get { return _code; } set { _code = value; } }
        [DataMember]
        public decimal Value { get { return _value; } set { _value = value; } }
        [DataMember]
        public DateTime Timestamp { get { return _date; } set { _date = value; } }
    }
}