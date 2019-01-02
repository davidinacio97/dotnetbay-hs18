using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using DotNetBay.Core;
using DotNetBay.Data.EF;
using DotNetBay.Data.Entity;
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
            return View();
        }

        // GET: Auctions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auctions/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "StartPrice,Title,Description,StartDateTimeUtc,EndDateTimeUtc")]
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

                this.service.Save(auction);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}