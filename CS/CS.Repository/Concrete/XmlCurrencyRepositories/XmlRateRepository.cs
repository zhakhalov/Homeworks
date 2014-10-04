using CS.Repository.Abstract;
using CS.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CS.Repository.Implementation.XmlCurrencyRepositories
{
    public class XmlRateRepository : IRateRepository
    {
        private readonly string _fileName;

        public XmlRateRepository(string fileName)
        {
            _fileName = fileName;
        }

        public List<CurrencyRate> GetAll()
        {
            List<CurrencyRate> rates = new List<CurrencyRate>();

            if (File.Exists(_fileName))
            {
                using (FileStream stream = File.OpenRead(_fileName))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<CurrencyRate>));
                    rates = (List<CurrencyRate>)serializer.Deserialize(stream);
                }
            }

            return rates;
        }

        public List<CurrencyRate> GetRate(string code)
        {
            return GetAll().Where(r => code == r.Code).ToList();
        }

        public List<CurrencyRate> GetRate(string code, DateTime from, DateTime to)
        {
            List<CurrencyRate> rates = GetRate(code);
            return rates.Where(r => r.Timestamp >= from && r.Timestamp <= to).ToList();
        }

        public void Add(CurrencyRate item)
        {
            List<CurrencyRate> list = GetAll();
            list.Add(item);

            using (FileStream stream = File.OpenWrite(_fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<CurrencyRate>));
                serializer.Serialize(stream, list);
            }
        }

        public void Remove(CurrencyRate item)
        {
            List<CurrencyRate> list = GetAll();

            if (!list.Contains(item))
            {
                return;
            }

            list.Remove(item);

            using (FileStream stream = File.OpenWrite(_fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<CurrencyRate>));
                serializer.Serialize(stream, list);
            }
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
                rates.Add(c, all.Where(r => c == r.Code && r.Timestamp >= from && r.Timestamp <= to).ToList());
            }

            return rates;
        }

        public CurrencyRate GetLast(string code)
        {
            List<CurrencyRate> rates = GetRate(code);
            DateTime timestamp = rates.Max(r => r.Timestamp);

            try
            {
                return rates.First(r => r.Timestamp == timestamp);
            }
            catch
            {
                return null;
            }
        }

        public Dictionary<string, CurrencyRate> GetLasts(string[] codes)
        {
            Dictionary<string, CurrencyRate> last = new Dictionary<string, CurrencyRate>();

            Dictionary<string, List<CurrencyRate>> rates = GetRates(codes);

            foreach (var c in codes)
            {
                try
                {
                    DateTime timestamp = rates[c].Max(r => r.Timestamp);
                    last.Add(c, rates[c].First(r => r.Timestamp == timestamp));
                }
                catch
                {
                    last.Add(c, null);
                }
            }

            return last;            
        }

        public CurrencyRate GetFirst(string code)
        {
            List<CurrencyRate> rates = GetRate(code);
            DateTime timestamp = rates.Min(r => r.Timestamp);

            try
            {
                return rates.First(r => r.Timestamp == timestamp);
            }
            catch
            {
                return null;
            }
        }

        public Dictionary<string, CurrencyRate> GetFirsts(string[] codes)
        {
            Dictionary<string, CurrencyRate> last = new Dictionary<string, CurrencyRate>();

            Dictionary<string, List<CurrencyRate>> rates = GetRates(codes);

            foreach (var c in codes)
            {
                try
                {
                    DateTime timestamp = rates[c].Min(r => r.Timestamp);
                    last.Add(c, rates[c].First(r => r.Timestamp == timestamp));
                }
                catch
                {
                    last.Add(c, null);
                }
            }

            return last; 
        }        
    }
}
