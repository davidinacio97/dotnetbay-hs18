using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetBay.Core.Execution;
using DotNetBay.Data.EF;
using Microsoft.Owin.Hosting;

namespace DotNetBay.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var auctionRunner = new AuctionRunner(new EFMainRepository());
            auctionRunner.Start();

            WebApp.Start<Startup>("http://localhost:1901/");

            Console.ReadLine();
        }
    }
}