using System.ComponentModel.DataAnnotations;
using System.Globalization;
using DotNetBay.Data.Entity;

namespace DotNetBay.WebApi.DTO
{
    public class AuctionDto
    {
        public AuctionDto()
        {
        }

        public AuctionDto(Auction auction)
        {
            Id = auction.Id;
            Title = auction.Title;
            Description = auction.Description;
            StartPrice = auction.StartPrice;
            CurrentPrice = auction.CurrentPrice;
            SellerName = auction.Seller.DisplayName;
            WinnerName = auction.Winner?.DisplayName;
            CurrentWinnerName = auction.ActiveBid?.Bidder.DisplayName;
            StartDateTimeUtc = auction.StartDateTimeUtc.ToString("MM/dd/yyyy HH:mm:ss");
            EndDateTimeUtc = auction.EndDateTimeUtc.ToString("MM/dd/yyyy HH:mm:ss");
            CloseDateTimeUtc = auction.CloseDateTimeUtc.ToString("MM/dd/yyyy HH:mm:ss");
            IsClosed = auction.IsClosed;
            IsRunning = auction.IsRunning;

            if (StartPrice > CurrentPrice)
            {
                CurrentPrice = StartPrice;
            }
        }

        public long Id { get; set; }

        [Required]
        public string StartDateTimeUtc { get; set; }

        [Required]
        public string EndDateTimeUtc { get; set; }

        public string CloseDateTimeUtc { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double StartPrice { get; set; }

        public double CurrentPrice { get; set; }

        [Required]
        public string SellerName { get; set; }

        public string WinnerName { get; set; }

        public bool IsClosed { get; set; }

        public bool IsRunning { get; set; }

        public string CurrentWinnerName { get; set; }
    }
}
