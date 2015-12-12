using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Otp.Web.OneTimePasswords
{
    [RoutePrefix("api/otp")]
    public class OtpController : ApiController
    {
        [HttpGet, Route("{userId}")]
        public string Get(string userId)
        {
            return "value";
        }

        [HttpPost, Route("{userId}")]
        public async Task<IHttpActionResult> Post(string userId)
        {
            return Ok(new OneTimePassword());
        }
    }

    public class OneTimePassword
    {
        private string Username { set; get; }
        public string Password { get; private set; }
        public DateTime ExpiresAt { get; private set; }

        public OneTimePassword()
        {
            Password = Guid.NewGuid().ToString();
            ExpiresAt = DateTime.UtcNow.AddSeconds(30);
            Username = "NOTE: security sensitive. Don't set or expose me; full credential set should not be exposed over the wire in a single lump.";
        }
    }
}