using CS.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CS.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICurrencyService" in both code and config file together.
    [ServiceContract]
    public interface ICurrencyService
    {
        [OperationContract]
        List<CurrencyDTO> GetAllCurrencies();

        [OperationContract]
        Dictionary<string, TimeRangeDTO> GetTimeRange(List<string> codes);

        [OperationContract]
        Dictionary<string, List<RateDTO>> GetRate(List<string> codes, DateTime? from, DateTime? to);
    }
}
