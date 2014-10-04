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
    public class TableViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Currency> _availableCurrencies;
        private ObservableCollection<Rate> _rate;
        private Currency _selectedCurrency;

        #region Binding

        public ObservableCollection<Currency> AvailableCurrencies
        {
            get
            {
                if (!_client.Currency.IsReady) // Lazy
                {
                    LoadCurrencies();
                }

                return _availableCurrencies;
            }
        }

        public Currency SelectedCurrency
        {
            get { return _selectedCurrency; }
            set
            {
                _selectedCurrency = value;
                _rate.Clear();
                LoadRates();
            }
        }

        public ObservableCollection<Rate> Rate { get { return _rate; } }

        #endregion Binding

        private readonly CurrencyRateClient _client;

        public TableViewModel(CurrencyRateClient client)
        {
            _client = client;
            _availableCurrencies = new ObservableCollection<Currency>();
            _rate = new ObservableCollection<Rate>();
        }

        private async void LoadCurrencies()
        {
            List<Currency> list = await _client.Currency.GetAllAsync();
            list.ForEach(r => _availableCurrencies.Add(r));

            SelectedCurrency = list.First();
            NotifyPropertyChanged("SelectedCurrency");
        }

        private async void LoadRates()
        {
            List<Currency> currencies = _availableCurrencies.ToList();
            foreach (var r in currencies)
            {
                _rate.Add(await _client.GetConversionAsync(_selectedCurrency, r));
            }
        }

        #region event PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void NotifyPropertyChanged(string name)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion event PropertyChanged
    }
}
