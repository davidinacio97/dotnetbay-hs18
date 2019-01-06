using System.Windows;
using DotNetBay.Core;
using DotNetBay.Core.Services;
using DotNetBay.Data.EF;
using DotNetBay.WPF.ViewModel;

namespace DotNetBay.WPF.View
{
    /// <summary>
    /// Interaction logic for SellView.xaml
    /// </summary>
    public partial class SellView : Window
    {
        public SellView()
        {
            InitializeComponent();

            var app = Application.Current as App;
            var auctionService = new RemoteAuctionService();
            DataContext = new SellViewModel(new SimpleMemberService(new EFMainRepository()), auctionService);
        }
    }
}