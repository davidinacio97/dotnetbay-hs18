using System;
using System.Net.Http.Formatting;
using System.Web.Http;
using DotNetBay.Core.Execution;
using DotNetBay.Data.EF;
using DotNetBay.WebApi.Controller;
using DotNetBay.WebApp;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace DotNetBay.WebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var auctionRunner = new AuctionRunner(new EFMainRepository());
            auctionRunner.Start();

            var type = typeof(AuctionController);
            Console.WriteLine(type);

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "MVC - Web API Combined",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Configuration for Lab 09 A3.1
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            app.UseWebApi(config);
        }
    }
}