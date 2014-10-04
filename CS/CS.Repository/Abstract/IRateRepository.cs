using CS.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Repository.Abstract
{
    public interface IRateRepository : IRepository<CurrencyRate>
    {
        List<CurrencyRate> GetRate(string code);
        List<CurrencyRate> GetRate(string code, DateTime from, DateTime to);

        Dictionary<string, List<CurrencyRate>> GetRates(string[] codes);
        Dictionary<string, List<CurrencyRate>> GetRates(string[] codes, DateTime from, DateTime to);

        CurrencyRate GetLast(string code);
        CurrencyRate GetFirst(string code);

        Dictionary<string, CurrencyRate> GetLasts(string[] codes);
        Dictionary<string, CurrencyRate> GetFirsts(string[] codes);
    }
}
