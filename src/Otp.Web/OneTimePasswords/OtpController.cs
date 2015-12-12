using System.Collections.Generic;
using System.Web.Http;

namespace Otp.Web.OneTimePasswords
{
    public class OtpController : ApiController
    {
        // GET: api/Otp
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Otp/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Otp
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Otp/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Otp/5
        public void Delete(int id)
        {
        }
    }
}
