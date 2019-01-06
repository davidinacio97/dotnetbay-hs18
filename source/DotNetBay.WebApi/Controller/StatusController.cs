using System.Web.Http;

namespace DotNetBay.WebApi.Controller
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
