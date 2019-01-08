using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetBay.Data.Entity;
using Microsoft.AspNet.SignalR;

namespace DotNetBay.SignalR.Hubs
{
    public class AuctionsHub : Hub
    {
        public static void NotifyNewAuction(Auction auction)
        {
            GlobalHost.ConnectionManager.GetHubContext<AuctionsHub>()
                .Clients.All.NewAuction(auction.Title);
        }

        public static void NotifyBidAccepted(Auction auction, Bid bid)
        {
            GlobalHost.ConnectionManager.GetHubContext<AuctionsHub>()
                .Clients.All.BidAccepted(auction.Id, bid.Amount);
        }

        public static void NotifyAuctionStarted(Auction auction)
        {
            GlobalHost.ConnectionManager.GetHubContext<AuctionsHub>()
                .Clients.All.AuctionStarted(auction.Id);
        }

        public static void NotifyAuctionEnded(Auction auction)
        {
            GlobalHost.ConnectionManager.GetHubContext<AuctionsHub>()
                .Clients.All.AuctionEnded(auction.Id);
        }
    }
}
