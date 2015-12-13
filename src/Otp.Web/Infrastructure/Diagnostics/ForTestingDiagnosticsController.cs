using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Otp.Web.Infrastructure.Diagnostics
{
    [RoutePrefix("api/internal")]
    public class ForTestingDiagnosticsController : ApiController
    {
        [HttpGet, Route("test-exception")]
        public Task<IHttpActionResult> Get()
        {
            throw new NotImplementedException();
        }
    }
}