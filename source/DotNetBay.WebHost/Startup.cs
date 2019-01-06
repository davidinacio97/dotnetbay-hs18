using System.Collections.Generic;
using System.Reflection;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Web.Http.Dispatcher;

[assembly: OwinStartup(typeof(DotNetBay.WebHost.Startup))]

namespace DotNetBay.WebHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Services.Replace(typeof(IAssembliesResolver), new AssembliesResolver());
            config.MapHttpAttributeRoutes();

            /*
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            */

            app.UseWebApi(config);
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