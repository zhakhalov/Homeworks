using CS.Client;
using CS.WpfPresentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CS.WpfPresentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CurrencyRateClient client = new CurrencyRateClient(TimeSpan.FromMinutes(double.Parse(ConfigurationManager.AppSettings["cacheLifeTime"])), ConfigurationManager.AppSettings["binding"]);
            vwTable.DataContext = new TableViewModel(client);
            vvChart.DataContext = new ChartViewModel(client);
        }
    }
}
