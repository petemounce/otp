using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Otp.Web.OneTimePasswords
{
    [RoutePrefix("api/otp")]
    public class OtpController : ApiController
    {
        private readonly IStoreUsers _store;
        private readonly IConfig _config;

        public OtpController(IStoreUsers store, IConfig config)
        {
            _store = store;
            _config = config;
        }

        [HttpPut, Route("{userId}/passwords")]
        public async Task<IHttpActionResult> Put(string userId, [FromBody] OneTimePasswordVerificationRequest body)
        {
            var tokens = await _store.TokensByUserIdAsync(userId);
            if (!tokens.Any()) return NotFound();
            var match = tokens.SingleOrDefault(t => t.Password.Equals(body.Password));
            if (match == null) return BadRequest();
            if (DateTime.UtcNow > match.ExpiresAt) return BadRequest();
            if (match.HasBeenUsed) return BadRequest();
            await _store.ConsumeTokenAsync(userId, body.Password);
            return Ok();
        }

        [HttpPost, Route("{userId}")]
        public async Task<IHttpActionResult> Post(string userId)
        {
            var otp = await _store.NewPasswordForAsync(userId, _config.AllowedAgeForPasswords);
            return Ok(otp);
        }
    }
}