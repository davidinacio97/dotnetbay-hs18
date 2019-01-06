using System.Linq;
using System.Web.Mvc;
using DotNetBay.Core;
using DotNetBay.Data.EF;
using DotNetBay.Data.Entity;
using DotNetBay.SignalR.Hubs;
using DotNetBay.WebApp.Models;

namespace DotNetBay.WebApp.Controllers
{
    public class AuctionsController : Controller
    {
        private EFMainRepository mainRepository;
        private IAuctionService service;

        public AuctionsController()
        {
            this.mainRepository = new EFMainRepository();
            this.service = new AuctionService(this.mainRepository, new SimpleMemberService(this.mainRepository));
        }

        // GET: Auctions
        public ActionResult Index()
        {
            return View(this.service.GetAll().ToList());
        }

        // GET: Auctions/Details/5
        public ActionResult Details(int id)
        {
            var auction = this.service.GetAll().FirstOrDefault(a => a.Id == id);
            if (auction == null)
            {
                return HttpNotFound();
            }

            var vmAuction = new NewAuctionViewModel()
            {
                Description = auction.Description,
                EndDateTimeUtc = auction.EndDateTimeUtc,
                StartDateTimeUtc = auction.StartDateTimeUtc,
                StartPrice = auction.StartPrice,
                Title = auction.Title
            };
            return View(vmAuction);
        }

        // GET: Auctions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auctions/Create
        [HttpPost]
        public ActionResult Create(
            [Bind(Include = "StartPrice,Title,Description,StartDateTimeUtc,EndDateTimeUtc,Image")]
            NewAuctionViewModel viewAuction)
        {
            if (this.ModelState.IsValid)
            {
                var members = new SimpleMemberService(this.mainRepository);

                var auction = new Auction()
                {
                    Title = viewAuction.Title,
                    Description = viewAuction.Description,
                    StartPrice = viewAuction.StartPrice,
                    StartDateTimeUtc = viewAuction.StartDateTimeUtc,
                    EndDateTimeUtc = viewAuction.EndDateTimeUtc,
                    Seller = members.GetCurrentMember(),
                };

                if (viewAuction.Image != null)
                {
                    byte[] fileContent = new byte[viewAuction.Image.InputStream.Length];
                    viewAuction.Image.InputStream.Read(fileContent, 0, fileContent.Length);

                    auction.Image = fileContent;
                }

                this.service.Save(auction);
                AuctionsHub.NotifyNewAuction(auction); 
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Image(int auctionId)
        {
            var auction = this.service.GetAll().FirstOrDefault(a => a.Id == auctionId);

            if (auction == null)
            {
                return HttpNotFound();
            }

            if (auction.Image != null)
            {
                return new FileContentResult(auction.Image, "image/jpg");
            }

            return new EmptyResult();
        }
    }
}