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
        private EFMainRepository repository;

        public HealthMiddleware(OwinMiddleware next) : base(next)
        {
            repository = new EFMainRepository();
        }

        public override async Task Invoke(IOwinContext context)
        {
            context.Response.Write(CreateHealthPage());
            await this.Next.Invoke(context);
        }

        private string CreateHealthPage()
        {
            StringBuilder healthBuilder = new StringBuilder();
            healthBuilder.AppendLine("<h1>Health Information</h1>");
            healthBuilder.AppendLine("<h2>Database Connection String</h2>");
            healthBuilder.AppendLine($"Data Source: {repository.Database.Connection.ConnectionString}");
            healthBuilder.AppendLine("<h2>Data Health</h2>");
            healthBuilder.AppendLine($"Number of Auctions: {repository.GetAuctions().Count()}");
            return healthBuilder.ToString();
        }
    }
}