using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Repository.Concrete
{
    [Serializable]
    public class Currency
    {
        private string _code;
        private string _name;

        public string Code { get { return _code; } set { _code = value; } }
        public string Name { get { return _name; } set { _name = value; } }
    }
}
