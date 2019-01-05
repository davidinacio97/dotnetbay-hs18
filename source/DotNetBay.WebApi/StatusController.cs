using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DotNetBay.WebApi
{
    [RoutePrefix("status")]
    public class StatusController : ApiController
    {

        [HttpGet]
        public IHttpActionResult AreYouFine()
        {
            return this.Ok("I'm fine");
        }
    }
}
