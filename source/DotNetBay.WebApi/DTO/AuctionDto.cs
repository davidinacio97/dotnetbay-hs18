using DotNetBay.Data.Entity;

namespace DotNetBay.WebApi.DTO
{
    public class AuctionDto
    {
        public AuctionDto(Auction auction)
        {
            this.CurrentPrice = auction.CurrentPrice;
            this.SellerName = auction.Seller.DisplayName;
            this.WinnerName = auction.Winner?.DisplayName;
            this.CurrentWinnerName = auction.Winner?.DisplayName;
        }

        public double CurrentPrice { get; set; }

        public string SellerName { get; set; }

        public string WinnerName { get; set; }

        public string CurrentWinnerName { get; set; }
    }
}
