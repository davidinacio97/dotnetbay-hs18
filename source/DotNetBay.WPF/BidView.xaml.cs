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

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaktionslogik für BidView.xaml
    /// </summary>
    public partial class BidView : Window
    {
        private readonly IAuctionService _auctionService;

        public Auction CurrentAuction { get; set; }

        public String PathToImage { get; set; }

        public BidView()
        {
            InitializeComponent();
            var app = App.Current as App;
            if (app != null)
            {
                var simpleMemberService = new SimpleMemberService(app.MainRepository);
                var service = _auctionService = new AuctionService(app.MainRepository, simpleMemberService);
            }
        }
    }
}
