using System.Threading.Tasks;
using System.Web.Http;

namespace Otp.Web
{
    public class RootController : ApiController
    {
        [HttpGet, Route("ping")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok("pong");
        }
    }
}