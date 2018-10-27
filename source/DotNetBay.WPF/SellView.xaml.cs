using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using DotNetBay.Core;
using DotNetBay.Data.Entity;
using Microsoft.Win32;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaktionslogik für SellView.xaml
    /// </summary>
    public partial class SellView : Window
    {
        private readonly IAuctionService _auctionService;

        public Auction NewAuction { get; }

        public String PathToImage { get; set; }

        public SellView()
        {
            InitializeComponent();
            var app = App.Current as App;
            if (app != null)
            {
                var simpleMemberService = new SimpleMemberService(app.MainRepository);
                var service = _auctionService = new AuctionService(app.MainRepository, simpleMemberService);
                NewAuction = new Auction()
                {
                    Seller = simpleMemberService.GetCurrentMember(),
                    StartDateTimeUtc = DateTime.UtcNow,
                    EndDateTimeUtc = DateTime.UtcNow.AddDays(7)
                };
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                PathToImage = openFileDialog.FileName;
            }
        }
    }
}
