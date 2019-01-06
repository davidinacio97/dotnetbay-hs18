using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using DotNetBay.Health.Owin;
using DotNetBay.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Owin;

[assembly: OwinStartup(typeof(DotNetBay.SelfHost.Startup))]

namespace DotNetBay.SelfHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Lab 08 - OWIN
            //app.UseHealth("/health");

            // Lab 09 - Web API
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DotNetBay",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Services.Replace(typeof(IAssembliesResolver), new AssembliesResolver());
            app.UseWebApi(config);
            config.EnsureInitialized();
        }
    }

    class AssembliesResolver : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            ICollection<Assembly> assemblies = base.GetAssemblies();
            var apiAssembly = Assembly.LoadFrom("DotNetBay.WebApi.dll");
            assemblies.Add(apiAssembly);
            return assemblies;
        }
    }
}
