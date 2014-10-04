using CS.Client;
using CS.Client.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.WpfPresentation.ViewModels
{
    public class ChartViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Currency> _availableSourceCurrencies;
        private ObservableCollection<Currency> _availableTargetCurrencies;

        private Currency _selectedSourceCurrency;
        private Currency _selectedTargetCurrency;

        private TimeRange _selectedTimeRange;
        private TimeRange _requestTimeRange;

        private ObservableCollection<Rate> _rate;

        

        #region Binding

        public ObservableCollection<Currency> AvailableSourceCurrencies
        {
            get
            {
                if (!_client.Currency.IsReady) // Lazy
                {
                    LoadCurrencies();
                }

                return _availableSourceCurrencies;
            }
        }

        public ObservableCollection<Currency> AvailableTargetCurrencies
        {
            get
            {
                if (!_client.Currency.IsReady) // Lazy
                {
                    LoadCurrencies();
                }

                return _availableTargetCurrencies;
            }
        }

        public Currency SelectedSourceCurrency
        {
            get { return _selectedSourceCurrency; }
            set
            {
                _selectedSourceCurrency = value;

                IsReady = false;
                NotifyPropertyChanged("IsReady");

                LoadDates();
            }
        }

        public Currency SelectedTargetCurrency
        {
            get { return _selectedTargetCurrency; }
            set
            {
                _selectedTargetCurrency = value;

                IsReady = false;
                NotifyPropertyChanged("IsReady");

                LoadDates();
            }
        }

        public bool IsReady { get; set; }

        public DateTime CurrencyStartDate { get { return _selectedTimeRange.First.Date; } }
        public DateTime CurrencyEndDate { get { return _selectedTimeRange.Last.Date; } }

        public DateTime SelectedStartDate
        {
            get { return _requestTimeRange.First; }
            set
            {
                _requestTimeRange.First = value;
                NotifyPropertyChanged("SelectedStartDate");
                LoadRates();
            }
        }

        public DateTime SelectedEndDate
        {
            get { return _requestTimeRange.Last; }
            set
            {
                _requestTimeRange.Last = value;
                NotifyPropertyChanged("SelectedEndDate");
                LoadRates();
            }
        }

        public ObservableCollection<Rate> Values { get { return _rate; } }

        public string Direction { get; private set; }

        #endregion Binding

        private readonly CurrencyRateClient _client;

        public ChartViewModel(Client.CurrencyRateClient client)
        {
            _client = client;
            _availableSourceCurrencies = new ObservableCollection<Currency>();
            _availableTargetCurrencies = new ObservableCollection<Currency>();
            _requestTimeRange = new TimeRange();
            _selectedTimeRange = new TimeRange();
            _rate = new ObservableCollection<Rate>();
        }

        private async void LoadCurrencies()
        {
            List<Currency> list = await _client.Currency.GetAllAsync();

            _availableSourceCurrencies.Clear();
            _availableTargetCurrencies.Clear();

            list.ForEach((Currency r) =>
            {
                _availableSourceCurrencies.Add(r);
                _availableTargetCurrencies.Add(r);
            });

            SelectedSourceCurrency = _availableSourceCurrencies.First();
            SelectedTargetCurrency = _availableTargetCurrencies.First();

            NotifyPropertyChanged("SelectedSourceCurrency");
            NotifyPropertyChanged("SelectedTargetCurrency");
        }

        private async void LoadDates()
        {
            if ((null == _selectedSourceCurrency) || (null == _selectedTargetCurrency))
            {
                return;
            }

            TimeRange sourceTR = await _client.Currency.GetTimeRangeAsync(_selectedSourceCurrency);
            TimeRange targetTR = await _client.Currency.GetTimeRangeAsync(_selectedTargetCurrency);

            _selectedTimeRange.First = (sourceTR.First > targetTR.First) ? sourceTR.First : targetTR.First;
            _selectedTimeRange.Last = (sourceTR.Last < targetTR.Last) ? sourceTR.Last : targetTR.Last;

            _requestTimeRange.First = _selectedTimeRange.First;
            _requestTimeRange.Last = _selectedTimeRange.Last;

            IsReady = true;
            NotifyPropertyChanged("IsReady");
            NotifyPropertyChanged("CurrencyStartDate");
            NotifyPropertyChanged("CurrencyEndDate");
            NotifyPropertyChanged("SelectedStartDate");
            NotifyPropertyChanged("SelectedEndDate");

            LoadRates();
        }

        private async void LoadRates()
        {
            List<Rate> rate = await _client.GetConversionRate(_selectedSourceCurrency, _selectedTargetCurrency, _requestTimeRange);

            _rate.Clear();
            rate.ForEach(r => _rate.Add(r));

            Direction = string.Format("{0}-{1}", _selectedSourceCurrency.Code, _selectedTargetCurrency.Code);
            NotifyPropertyChanged("Direction");

        }        

        #region event PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string name)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion event PropertyChanged
    }
}
