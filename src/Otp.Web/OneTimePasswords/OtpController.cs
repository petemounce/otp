using System.Collections.Generic;
using System.Web.Http;

namespace Otp.Web.OneTimePasswords
{
    [RoutePrefix("api/otp")]
    public class OtpController : ApiController
    {
        [HttpGet, Route("")]
        // GET: api/Otp
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet, Route("{id}")]
        // GET: api/Otp/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost, Route("")]
        // POST: api/Otp
        public void Post([FromBody]string value)
        {
        }

        [HttpPut, Route("")]
        // PUT: api/Otp/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete, Route("")]
        // DELETE: api/Otp/5
        public void Delete(int id)
        {
        }
    }
}
