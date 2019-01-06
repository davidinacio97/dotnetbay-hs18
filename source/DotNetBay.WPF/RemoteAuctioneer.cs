using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetBay.Core.Execution;

namespace DotNetBay.WPF
{
    class RemoteAuctioneer : IAuctioneer
    {
        public event EventHandler<ProcessedBidEventArgs> BidDeclined;
        public event EventHandler<ProcessedBidEventArgs> BidAccepted;
        public event EventHandler<AuctionEventArgs> AuctionEnded;
        public event EventHandler<AuctionEventArgs> AuctionStarted;
    }
}
