using System.Collections.ObjectModel;
using System.Windows;
using DotNetBay.Core;
using DotNetBay.Data.Entity;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var app = App.Current as App;
            InitializeComponent();
            

            IAuctionService auctionService = new AuctionService(app.MainRepository, new SimpleMemberService(app.MainRepository));
            Auctions = new ObservableCollection<Auction>(auctionService.GetAll());
            DataContext = this;

        }

        public ObservableCollection<Auction> Auctions { get; }

        private void NewAuction_OnClick(object sender, RoutedEventArgs e)
        {
            var sellView = new SellView();
            sellView.ShowDialog();
        }
    }
}
