using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CS.Service.DTOs
{
    [DataContract]
    public class TimeRangeDTO
    {
        private DateTime _first;
        private DateTime _last;

        [DataMember]
        public DateTime First { get { return _first; } set { _first = value; } }

        [DataMember]
        public DateTime Last { get { return _last; } set { _last = value; } }
    }
}