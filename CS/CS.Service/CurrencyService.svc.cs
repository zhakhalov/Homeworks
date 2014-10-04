using CS.Repository.Abstract;
using CS.Repository.Concrete;
using CS.Repository.Concrete.SqlCurrencyRepositories;
using CS.Service.DTOs;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CS.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CurrencyService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CurrencyService.svc or CurrencyService.svc.cs at the Solution Explorer and start debugging.
    public class CurrencyService : ICurrencyService
    {
        public List<CurrencyDTO> GetAllCurrencies()
        {
            ICurrencyRepository repository = new RepositoryFactory().CreateCurrencyRepository(ConfigurationManager.AppSettings["repository"]);
            List<Currency> raw = repository.GetAll();

            List<CurrencyDTO> currencies = new List<CurrencyDTO>();

            raw.ForEach(r => currencies.Add(new CurrencyDTO
            {
                Code = r.Code,
                Name = r.Name
            }));

            return currencies;
        }

        public Dictionary<string, TimeRangeDTO> GetTimeRange(List<string> codes)
        {
            IRateRepository repository = new RepositoryFactory().CreateRateRepository(ConfigurationManager.AppSettings["repository"]);

            Dictionary<string, CurrencyRate> firsts = repository.GetFirsts(codes.ToArray());
            Dictionary<string, CurrencyRate> lasts = repository.GetLasts(codes.ToArray());

            Dictionary<string, TimeRangeDTO> range = new Dictionary<string, TimeRangeDTO>();

            codes.ForEach(r => range.Add(r, new TimeRangeDTO
            {
                First = firsts[r].Timestamp,
                Last = lasts[r].Timestamp
            }));

            return range;
        }

        public Dictionary<string, List<RateDTO>> GetRate(List<string> codes, DateTime? from, DateTime? to)
        {
            IRateRepository repository = new RepositoryFactory().CreateRateRepository(ConfigurationManager.AppSettings["repository"]);

            Dictionary<string, List<CurrencyRate>> raw = null;

            if (from.HasValue && to.HasValue)
            {
                raw = repository.GetRates(codes.ToArray(), from.Value, to.Value);
            }
            else if (from.HasValue && !to.HasValue)
            {
                raw = repository.GetRates(codes.ToArray(), from.Value, DateTime.Now);
            }
            else
            {
                raw = repository.GetRates(codes.ToArray());
            }

            Dictionary<string, List<RateDTO>> rate = new Dictionary<string, List<RateDTO>>();

            foreach (var c in codes)
            {
                List<RateDTO> list = new List<RateDTO>();

                raw[c].ForEach(r => list.Add(new RateDTO
                {
                    Code = r.Code,
                    Value = r.Value,
                    Timestamp = r.Timestamp
                }));

                rate.Add(c, list);
            }

            return rate;
        }
    }
}
