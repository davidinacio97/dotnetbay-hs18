using System;
using System.Web.Http;
using DotNetBay.WebApi.Controller;
using DotNetBay.WebApp;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace DotNetBay.WebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var type = typeof(AuctionController);
            Console.WriteLine(type);

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "MVC - Web API Combined",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            app.UseWebApi(config);
        }
    }
}