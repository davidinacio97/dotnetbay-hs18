using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;

namespace DotNetBay.Health.Owin
{
    public static class HealthFeature
    {
        public static void UseHealth(this IAppBuilder app, string route)
        {
            app.Use(typeof(HealthMiddleware), route);
        }
    }
}
