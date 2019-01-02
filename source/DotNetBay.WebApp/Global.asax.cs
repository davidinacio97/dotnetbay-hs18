using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DotNetBay.Core.Execution;
using DotNetBay.Data.EF;

namespace DotNetBay.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IAuctionRunner AuctionRunner { get; private set; }

        protected void Application_Start()
        {
            //MVC related startup
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //DotNetBay related startup
            var mainRepository = new EFMainRepository();
            mainRepository.SaveChanges();

            AuctionRunner = new AuctionRunner(mainRepository);
            AuctionRunner.Start();
        }
    }
}
