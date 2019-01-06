using System;
using System.Windows;
using DotNetBay.Core;
using DotNetBay.Core.Services;
using DotNetBay.Data.Entity;
using DotNetBay.WPF.ViewModel;

namespace DotNetBay.WPF.View
{
    /// <summary>
    /// Interaction logic for SellView.xaml
    /// </summary>
    public partial class BidView : Window
    {
        public BidView(Auction selectedAuction)
        {
            this.InitializeComponent();

            var app = Application.Current as App;

            var auctionService = new RemoteAuctionService();

            this.DataContext = new BidViewModel(selectedAuction, auctionService);

        }
    }
}
