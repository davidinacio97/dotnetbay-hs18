using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using DotNetBay.Core.Services.DTO;
using DotNetBay.Data.EF;
using DotNetBay.Data.Entity;

namespace DotNetBay.Core.Services
{
    public class RemoteAuctionService : IAuctionService
    {
        private EFMainRepository repository;

        public RemoteAuctionService()
        {
            this.repository = new EFMainRepository();
        }

        public IQueryable<Auction> GetAll()
        {
            return new EnumerableQuery<Auction>(GetAllAuctions());
        }

        public Auction Save(Auction auction)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var httpResponseMessage = client.PostAsJsonAsync($"http://localhost:53837/api/auction/", new AuctionDto(auction)).Result;
                httpResponseMessage.Dispose();
                var allAuctions = GetAllAuctions();
                var createdAuction = allAuctions.FirstOrDefault(a => a.Title == auction.Title && a.Description == auction.Description);
                var imageBinaryContent = new ByteArrayContent(auction.Image);
                var responseMessage = client.PostAsync($"http://localhost:53837/api/auction/{createdAuction?.Id}/image", imageBinaryContent).Result;
                responseMessage.Dispose();
            }
            return new Auction();
        }

        public Bid PlaceBid(Auction auction, double amount)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var httpResponseMessage = client.PostAsJsonAsync($"http://localhost:53837/api/auction/{auction.Id}/bid", new BidDto
                {
                    Amount = amount
                }).Result;
            }
            return new Bid();
        }

        private IList<Auction> GetAllAuctions()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (HttpResponseMessage httpResponseMessage =
                client.GetAsync(new Uri("http://localhost:53837/api/auction")).Result)
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var auctionDtos = httpResponseMessage.Content.ReadAsAsync<IList<AuctionDto>>().Result;
                    client.Dispose();
                    return CreateAuctions(auctionDtos);
                }
            }
            client.Dispose();
            return new List<Auction>();
        }

        private IList<Auction> CreateAuctions(IList<AuctionDto> auctionDtos)
        {
            var auctions = auctionDtos.Select(dto => CreateAuction(dto, GetImageForAuctionId(dto.Id))).ToList();
            return auctions;
        }

        private byte[] GetImageForAuctionId(long id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jpg"));
            using (HttpResponseMessage httpResponseMessage = client.GetAsync(new Uri($"http://localhost:53837/api/auction/{id}/image")).Result)
            {
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    client.Dispose();
                    return httpResponseMessage.Content.ReadAsByteArrayAsync().Result;
                }
            }
            client.Dispose();
            return new byte[0];
        }

        private Auction CreateAuction(AuctionDto auctionDto, byte[] image)
        {
            Auction auction = new Auction();
            auction.Id = auctionDto.Id;
            auction.Title = auctionDto.Title;
            auction.Description = auctionDto.Description;
            auction.StartPrice = auctionDto.StartPrice;
            auction.CurrentPrice = auctionDto.CurrentPrice;
            auction.Seller = repository.GetMembers().FirstOrDefault(m => m.DisplayName.Equals(auctionDto.SellerName));
            auction.StartDateTimeUtc = DateTime.ParseExact(auctionDto.StartDateTimeUtc, "MM/dd/yyyy HH:mm:ss", null);
            auction.EndDateTimeUtc = DateTime.ParseExact(auctionDto.EndDateTimeUtc, "MM/dd/yyyy HH:mm:ss", null);
            auction.CloseDateTimeUtc = DateTime.ParseExact(auctionDto.CloseDateTimeUtc, "MM/dd/yyyy HH:mm:ss", null);
            auction.IsClosed = auctionDto.IsRunning;
            auction.IsRunning = auctionDto.IsRunning;
            auction.Image = image;
            return auction;
        }
    }
}
