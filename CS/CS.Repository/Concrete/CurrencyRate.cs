using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Repository.Concrete
{
    [Serializable]
    public class CurrencyRate
    {
        private string _code;        
        private decimal _value;        
        private DateTime _timestamp;

        public string Code { get { return _code; } set { _code = value; } }
        public decimal Value { get { return _value; } set { _value = value; } }
        public DateTime Timestamp { get { return _timestamp; } set { _timestamp = value; } }
    }
}
