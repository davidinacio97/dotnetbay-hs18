using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace DotNetBay.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            WebApp.Start<Startup>(url: "http://localhost:1901/");
        }
    }
}