using CS.Client.CurrencyService;
using CS.Client.Data;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace CS.Client.Caching
{
    public class RateHolder
    {
        private AsyncLock _sync;
        private readonly ObjectCache _cache;
        private readonly TimeSpan _cacheLifeTime;
        private readonly CurrencyHolder _currencyHolder;
        private readonly ICurrencyService _service;

        public RateHolder(TimeSpan cacheLifeTime, CurrencyHolder currencyHolder, ICurrencyService service)
        {
            _sync = new AsyncLock();
            _cache = new MemoryCache("CurrencyCache");
            _cacheLifeTime = cacheLifeTime;
            _currencyHolder = currencyHolder;
            _service = service;
        }

        public List<Rate> Get(Currency currency)
        {
            TimeRange range = _currencyHolder.GetTimeRange(currency);
            return Get(currency, range.First, range.Last);
        }

        public List<Rate> Get(Currency currency, DateTime start, DateTime end)
        {
            List<Rate> list = null;
            Task.Run(async delegate()
            {
                list = await GetAsync(currency, start, end);
            }).Wait();
            return list;
        }

        public async Task<List<Rate>> GetAsync(Currency currency)
        {
            TimeRange range = await _currencyHolder.GetTimeRangeAsync(currency);
            return await GetAsync(currency, range.First, range.Last);
        }

        public async Task<List<Rate>> GetAsync(Currency currency, DateTime start, DateTime end)
        {
            List<Rate> rate = null;
            TimeRange range = await _currencyHolder.GetTimeRangeAsync(currency);

            //clamping range
            start = (start > range.First) ? start : range.First;
            end = (end < range.Last) ? end : range.Last;

            if (start > end)
            {
                start = end;
            }

            if (_cache.Contains(currency.Code))
            {
                rate = ((List<Rate>)_cache.Get(currency.Code)).OrderBy(r => r.Timestamp).ToList();

                if ((start < rate.First().Timestamp) || (end > rate.Last().Timestamp))
                {
                    using (var unlock = await _sync.LockAsync())
                    {
                        if ((start < rate.First().Timestamp) || (end > rate.Last().Timestamp))
                        {
                            List<RateDTO> raw = (await _service.GetRateAsync(new string[] { currency.Code }, start, end))[currency.Code].ToList();
                            rate.Clear();
                            raw.ForEach(r => rate.Add(new Rate { Code = r.Code, Timestamp = r.Timestamp, Value = r.Value }));
                            _cache.Set(currency.Code, rate, DateTime.Now + _cacheLifeTime);
                        }
                    }
                }
            }
            else
            {
                using (var unlock = await _sync.LockAsync())
                {
                    if (!_cache.Contains(currency.Code))
                    {
                        List<RateDTO> raw = (await _service.GetRateAsync(new string[] { currency.Code }, start, end))[currency.Code].ToList();
                        rate = new List<Rate>();
                        raw.ForEach(r => rate.Add(new Rate { Code = r.Code, Timestamp = r.Timestamp, Value = r.Value }));
                        _cache.Add(currency.Code, rate, DateTime.Now + _cacheLifeTime);
                    }
                    else
                    {
                        rate = ((List<Rate>)_cache.Get(currency.Code)).OrderBy(r => r.Timestamp).ToList();
                    }
                }
            }

            rate = rate.Where(r => (r.Timestamp >= start && r.Timestamp <= end)).ToList();

            return rate;
        }
    }
}
