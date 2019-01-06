using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
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
            this.Title = auction.Title;
            this.Description = auction.Description;
            this.StartPrice = auction.StartPrice;
            this.CurrentPrice = auction.CurrentPrice;
            this.SellerName = auction.Seller.DisplayName;
            this.WinnerName = auction.Winner?.DisplayName;
            this.CurrentWinnerName = auction.ActiveBid?.Bidder.DisplayName;
            this.RunningDays = auction.EndDateTimeUtc.DayOfYear - auction.StartDateTimeUtc.DayOfYear;

            if (StartPrice > CurrentPrice)
            {
                CurrentPrice = StartPrice;
            }
        }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double StartPrice { get; set; }

        public double CurrentPrice { get; set; }

        [Required]
        public string SellerName { get; set; }

        [Required]
        [Range(1, 20)]
        public double RunningDays { get; set; }

        public string WinnerName { get; set; }

        public string CurrentWinnerName { get; set; }
    }
}
