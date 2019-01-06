using DotNetBay.Core;
using DotNetBay.Data.EF;
using DotNetBay.Data.Entity;
using DotNetBay.WebApi.DTO;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace DotNetBay.WebApi.Controller
{
    [RoutePrefix("api/auction")]
    public class AuctionController : ApiController
    {
        private EFMainRepository repository;

        private IAuctionService service;

        public AuctionController()
        {
            repository = new EFMainRepository();
            service = new AuctionService(repository, new SimpleMemberService(repository));
        }

        [HttpGet]
        public IHttpActionResult Read()
        {
            return Ok(repository.GetAuctions().ToList().Select(a => new AuctionDto(a)));
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Read(int id)
        {
            var auction = repository.GetAuctions().FirstOrDefault(a => a.Id == id);
            if (auction != null)
            {
                return Ok(new AuctionDto(auction));
            }

            return NotFound();
        }

        [HttpGet]
        [Route("{id}/image")]
        public IHttpActionResult ReadImage(int id)
        {
            var auction = repository.GetAuctions().FirstOrDefault(a => a.Id == id);
            if (auction == null)
            {
                return BadRequest($"No auction with id {id} found");
            }

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            responseMessage.Content = new ByteArrayContent(auction.Image ?? new byte[0]);
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
            return ResponseMessage(responseMessage);

        }

        [HttpPost]
        public IHttpActionResult Create(AuctionDto auctionDto)
        {
            Auction auction = CreateAuction(auctionDto);
            service.Save(auction);
            return Ok();
        }

        [HttpPost, Route("{id}/bid")]
        public IHttpActionResult Create(int id, BidDto bidDto)
        {
            var auction = service.GetAll().FirstOrDefault(a => a.Id == id);
            if (auction == null)
            {
                return BadRequest($"No auction with id {id} found");
            }
            service.PlaceBid(auction, bidDto.Amount);
            return Ok();
        }

        [HttpPost]
        [Route("{id}/image")]
        public IHttpActionResult Create(long id)
        {
            Auction auction = repository.GetAuctions().FirstOrDefault(a => a.Id == id);
            if (auction == null)
            {
                return BadRequest($"No auction with id {id} found");
            }

            var image = Request.Content.ReadAsByteArrayAsync().Result;
            auction.Image = image;
            //repository.Add(auction);
            repository.SaveChanges();
            return Ok();
        }

        private Auction CreateAuction(AuctionDto auctionDto)
        {
            Auction auction = new Auction();
            auction.Title = auctionDto.Title;
            auction.Description = auctionDto.Description;
            auction.StartPrice = auctionDto.StartPrice;
            auction.CurrentPrice = auctionDto.StartPrice;
            auction.Seller = repository.GetMembers().FirstOrDefault(m => m.DisplayName.Equals(auctionDto.SellerName));
            auction.StartDateTimeUtc = DateTime.ParseExact(auctionDto.StartDateTimeUtc, "MM/dd/yyyy HH:mm:ss", null);
            auction.EndDateTimeUtc = DateTime.ParseExact(auctionDto.EndDateTimeUtc, "MM/dd/yyyy HH:mm:ss", null);
            return auction;
        }
    }
}