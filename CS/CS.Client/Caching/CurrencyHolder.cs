using CS.Client.CurrencyService;
using CS.Client.Data;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CS.Client.Caching
{
    public class CurrencyHolder
    {
        private AsyncLock _sync;

        private const string AVAILABLE = "available";

        private readonly ObjectCache _cache;
        private readonly TimeSpan _cacheLifeTime;
        private readonly ICurrencyService _service;

        public bool IsReady { get { return _cache.Contains(AVAILABLE); } }

        public CurrencyHolder(TimeSpan cacheLifeTime, ICurrencyService service)
        {
            _sync = new AsyncLock();
            _cache = new MemoryCache("CurrencyCache");
            _cacheLifeTime = cacheLifeTime;
            _service = service;
        }

        public TimeRange GetTimeRange(Currency currency)
        {
            TimeRange tr = null;
            Task.Run(async delegate()
            {
                tr = await GetTimeRangeAsync(currency);
            }).Wait();
            return tr;
        }

        public async Task<TimeRange> GetTimeRangeAsync(Currency currency)
        {
            if (!_cache.Contains(currency.Code))
            {
                using (var unlock = await _sync.LockAsync())
                {
                    if (!_cache.Contains(currency.Code))
                    {
                        TimeRangeDTO tr = (await _service.GetTimeRangeAsync(new string[] { currency.Code }))[currency.Code];
                        _cache.Add(currency.Code, new TimeRange { First = tr.First, Last = tr.Last }, DateTime.Now + _cacheLifeTime);
                    }
                }
            }

            return (TimeRange)_cache.Get(currency.Code);
        }

        public List<Currency> GetAll()
        {
            List<Currency> list = null;
            Task.Run(async delegate()
            {
                list = await GetAllAsync();
            }).Wait();
            return list;
        }

        public async Task<List<Currency>> GetAllAsync()
        {
            if (!_cache.Contains(AVAILABLE))
            {
                using (var unlock = await _sync.LockAsync())
                {
                    if (!_cache.Contains(AVAILABLE))
                    {
                        List<CurrencyDTO> raw = (await _service.GetAllCurrenciesAsync()).ToList();
                        List<Currency> all = new List<Currency>();

                        raw.ForEach(r => all.Add(new Currency { Code = r.Code, Name = r.Name }));
                        _cache.Add(AVAILABLE, all, DateTime.Now + _cacheLifeTime);
                    }
                }
            }

            return (List<Currency>)_cache.Get(AVAILABLE);
        }
    }
}
