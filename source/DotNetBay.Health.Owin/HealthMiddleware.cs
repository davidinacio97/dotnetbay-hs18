using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetBay.Data.EF;
using Microsoft.Owin;

namespace DotNetBay.Health.Owin
{
    public class HealthMiddleware : OwinMiddleware
    {
        private readonly EFMainRepository repository;

        private readonly string route;

        public HealthMiddleware(OwinMiddleware next, string route) : base(next)
        {
            this.route = route;
            repository = new EFMainRepository();
        }

        public override async Task Invoke(IOwinContext context)
        {
            if (context.Request.Path.Value.EndsWith(route))
            {
                var healthPage = CreateHealthPage();
                context.Response.Write(healthPage);
                return;
            }

            await Next.Invoke(context);
        }

        private string CreateHealthPage()
        {
            StringBuilder healthBuilder = new StringBuilder();
            healthBuilder.AppendLine("<h1>Health Information</h1>");
            healthBuilder.AppendLine("<h2>Database Connection String</h2>");
            var connectionConnectionString = repository.Database.Connection.ConnectionString;
            healthBuilder.AppendLine($"Data Source: {connectionConnectionString}");
            healthBuilder.AppendLine("<h2>Data Health</h2>");
            healthBuilder.AppendLine($"Number of Auctions: {repository.GetAuctions().Count()}");
            return healthBuilder.ToString();
        }
    }
}