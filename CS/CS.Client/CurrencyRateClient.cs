using CS.Client.Caching;
using CS.Client.CurrencyService;
using CS.Client.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Client
{
    public class CurrencyRateClient
    {
        private readonly CurrencyHolder _currencyHolder;
        private readonly RateHolder _rateHolder;

        public CurrencyHolder Currency { get { return _currencyHolder; } }
        public RateHolder Rate { get { return _rateHolder; } }

        public CurrencyRateClient(TimeSpan _cacheLifeTime, string endpointConfigurationName)
        {
            ICurrencyService service = new CurrencyServiceClient(endpointConfigurationName);

            _currencyHolder = new CurrencyHolder(_cacheLifeTime, service);
            _rateHolder = new RateHolder(_cacheLifeTime, _currencyHolder, service);
        }

        public async Task<decimal> ConvertAsync(decimal source, Currency from, Currency to)
        {
            return source * (await GetConversionAsync(from, to)).Value;
        }

        public async Task<Rate> GetConversionAsync(Currency from, Currency to)
        {
            List<Rate> fromRate = await _rateHolder.GetAsync(from, DateTime.Today, DateTime.Today);
            List<Rate> toRate = await _rateHolder.GetAsync(to, DateTime.Today, DateTime.Today);

            if ((0 == fromRate.Count) || (0 == toRate.Count))
            {
                throw new Exception("Not Enought Data for Conveting");
            }

            return new Rate
            {
                Code = to.Code,
                Value = fromRate.Last().Value / toRate.Last().Value
            };
        }

        public async Task<List<Rate>> GetConversionRate(Currency from, Currency to, TimeRange range)
        {
            List<Rate> fromRate = await _rateHolder.GetAsync(from, range.First, range.Last);
            List<Rate> toRate = await _rateHolder.GetAsync(to, range.First, range.Last);

            if ((0 == fromRate.Count) || (0 == toRate.Count))
            {
                throw new Exception("Not Enought Data for Conveting");
            }

            int minLength = (fromRate.Count > toRate.Count) ? toRate.Count : fromRate.Count;

            List<Rate> rate = new List<Rate>();

            for (int i = 0; i < minLength; ++i)
            {
                rate.Add(new Rate
                {
                    Code = string.Format("{0}-{1}", from.Code, to.Code),
                    Timestamp = fromRate[i].Timestamp,
                    Value = fromRate[i].Value / toRate[i].Value
                });
            }
            
            return rate;
        }
    }
}
