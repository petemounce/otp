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
        public async Task<IHttpActionResult> Put(string userId, [FromBody] string password)
        {
            if (!await _store.UserExistsAsync(userId)) return NotFound();
            return BadRequest();
        }

        [HttpPost, Route("{userId}")]
        public async Task<IHttpActionResult> Post(string userId)
        {
            var otp = await _store.NewPasswordForAsync(userId, _config.AllowedAgeForPasswords);
            return Ok(otp);
        }
    }
}