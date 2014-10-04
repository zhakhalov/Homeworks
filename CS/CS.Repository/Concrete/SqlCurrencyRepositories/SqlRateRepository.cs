using CS.Repository.Abstract;
using CS.Repository.Concrete;
using CS.Repository.Concrete.SqlCurrencyRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace CS.Repository.Concrete.SqlCurrencyRepositories
{
    public class SqlRateRepository : IRateRepository
    {
        readonly string _connectionString;

        #region Constructors

        public SqlRateRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        #endregion Constructors

        DbContext CreateDBContext()
        {
            return new DbContext(_connectionString);
        }

        public List<CurrencyRate> GetRate(string code)
        {
            List<CurrencyRate> list = new List<CurrencyRate>();

            using (DbContext context = CreateDBContext())
            {
                List<CS.Entities.Rate> raw = context.Set<CS.Entities.Rate>().Include(r => r.Currency).Where(r => r.Currency.Code == code).ToList();
                raw.ForEach(r => list.Add(new CurrencyRate
                {
                    Code = r.Currency.Code,
                    Timestamp = r.Timestamp,
                    Value = r.Value
                }));
            }

            return list;
        }

        public List<CurrencyRate> GetRate(string code, DateTime from, DateTime to)
        {
            from -= TimeSpan.FromSeconds(1); // extend milliseconds
            to += TimeSpan.FromSeconds(1); // extend milliseconds

            List<CurrencyRate> list = new List<CurrencyRate>();

            using (DbContext context = CreateDBContext())
            {
                List<CS.Entities.Rate> raw = context.Set<CS.Entities.Rate>()
                    .Include(r => r.Currency)
                    .Where(r => ((r.Currency.Code == code) && (r.Timestamp >= from) && (r.Timestamp <= to)))
                    .ToList();
                raw.ForEach(r => list.Add(new CurrencyRate
                {
                    Code = r.Currency.Code,
                    Timestamp = r.Timestamp,
                    Value = r.Value
                }));
            }

            return list;
        }

        public Dictionary<string, List<CurrencyRate>> GetRates(string[] codes)
        {
            List<CurrencyRate> all = GetAll();
            Dictionary<string, List<CurrencyRate>> rates = new Dictionary<string, List<CurrencyRate>>();

            foreach (var c in codes)
            {
                rates.Add(c, all.Where(r => c == r.Code).ToList());
            }

            return rates;
        }

        public Dictionary<string, List<CurrencyRate>> GetRates(string[] codes, DateTime from, DateTime to)
        {
            List<CurrencyRate> all = GetAll();
            Dictionary<string, List<CurrencyRate>> rates = new Dictionary<string, List<CurrencyRate>>();

            foreach (var c in codes)
            {
                rates.Add(c, GetRate(c, from, to));
            }

            return rates;
        }

        public CurrencyRate GetLast(string code)
        {
            List<CurrencyRate> rates = GetRate(code);
            DateTime timestamp = rates.Max(r => r.Timestamp);

            return rates.FirstOrDefault(r => r.Timestamp == timestamp);
        }


        public CurrencyRate GetFirst(string code)
        {
            List<CurrencyRate> rates = GetRate(code);
            DateTime timestamp = rates.Min(r => r.Timestamp);

            return rates.FirstOrDefault(r => r.Timestamp == timestamp);
        }

        public Dictionary<string, CurrencyRate> GetLasts(string[] codes)
        {
            Dictionary<string, CurrencyRate> last = new Dictionary<string, CurrencyRate>();

            Dictionary<string, List<CurrencyRate>> rates = GetRates(codes);

            foreach (var c in codes)
            {
                DateTime timestamp = rates[c].Max(r => r.Timestamp);
                last.Add(c, rates[c].FirstOrDefault(r => r.Timestamp == timestamp));
            }

            return last; 
        }

        public Dictionary<string, CurrencyRate> GetFirsts(string[] codes)
        {
            Dictionary<string, CurrencyRate> last = new Dictionary<string, CurrencyRate>();

            Dictionary<string, List<CurrencyRate>> rates = GetRates(codes);

            foreach (var c in codes)
            {
                DateTime timestamp = rates[c].Min(r => r.Timestamp);
                last.Add(c, rates[c].FirstOrDefault(r => r.Timestamp == timestamp));
            }

            return last; 
        }

        public void Add(CurrencyRate item)
        {
            using (DbContext context = CreateDBContext())
            {
                SqlCurrencyRepository repository = new SqlCurrencyRepository(_connectionString);

                context.Set<CS.Entities.Rate>().Add(new CS.Entities.Rate
                    {
                        CurrencyId = new SqlCurrencyRepository(_connectionString).GetByCode(item.Code).Id,
                        Timestamp = item.Timestamp,
                        Value = item.Value
                    });
                context.SaveChanges();
            }
        }

        public List<CurrencyRate> GetAll()
        {
            List<CurrencyRate> list = new List<CurrencyRate>();

            using (DbContext context = CreateDBContext())
            {
                List<CS.Entities.Rate> raw = context.Set<CS.Entities.Rate>().Include(r => r.Currency).ToList();
                raw.ForEach(r => list.Add(new CurrencyRate
                {
                    Code = r.Currency.Code,
                    Timestamp = r.Timestamp,
                    Value = r.Value
                }));
            }

            return list;
        }
    }
}
