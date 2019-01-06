using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetBay.Core;
using DotNetBay.Data.EF;
using DotNetBay.Data.Entity;
using DotNetBay.SignalR.Hubs;
using DotNetBay.WebApp.Models;

namespace DotNetBay.WebApp.Controllers
{
    public class BidsController : Controller
    {
        private EFMainRepository mainRepository;
        private IAuctionService service;

        public BidsController()
        {
            this.mainRepository = new EFMainRepository();

            this.service = new AuctionService(this.mainRepository, new SimpleMemberService(this.mainRepository));
        }

        // GET: Bids/Create?AuctionId=
        public ActionResult Create(int auctionId)
        {
            var auction = service.GetAll().FirstOrDefault(a => a.Id == auctionId);

            if (auction == null)
            {
                return this.HttpNotFound();
            }

            var newBidViewModel = new NewBidViewModel
            {
                AuctionTitle = auction.Title,
                AuctionDescription = auction.Description,
                StartPrice = auction.StartPrice,
                CurrentPrice = auction.CurrentPrice,
                BidAmount = Math.Max(auction.StartPrice, auction.CurrentPrice) + 1
            };

            return View(newBidViewModel);
        }

        // POST: Bids/Create
        [HttpPost]
        public ActionResult Create(NewBidViewModel newBid)
        {
            if (this.ModelState.IsValid)
            {
                var auction = this.service.GetAll().FirstOrDefault(a => a.Id == newBid.AuctionId);

                Bid bid = this.service.PlaceBid(auction, newBid.BidAmount);
                if (bid.Accepted.HasValue && bid.Accepted.Value)
                {
                    AuctionsHub.NotifyBidAccepted(auction, bid);
                }

                return RedirectToAction("Index","Auctions");
            }
            return View();
        }

        // GET: Bids/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Bids/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Bids/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Bids/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}