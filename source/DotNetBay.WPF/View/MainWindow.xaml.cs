using System.Windows;
using DotNetBay.Core.Services;
using DotNetBay.WPF.ViewModel;

namespace DotNetBay.WPF.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var app = Application.Current as App;

            InitializeComponent();

            var auctionService = new RemoteAuctionService();
            DataContext = new MainViewModel(app.AuctionRunner.Auctioneer, auctionService);
        }
    }
}
