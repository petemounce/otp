using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using FluentValidation;

namespace Otp.Web.OneTimePasswords
{
    [RoutePrefix("api/otp")]
    public class OtpController : ApiController
    {
        private readonly IStoreUsers _store;
        private readonly IConfigDataForOneTimePasswords _config;
        private readonly IValidator<OneTimePassword> _checker;

        public OtpController(IStoreUsers store, IConfigDataForOneTimePasswords config, IValidator<OneTimePassword> checker)
        {
            _store = store;
            _config = config;
            _checker = checker;
        }

        [HttpPut, Route("{userId}/passwords")]
        public async Task<IHttpActionResult> Put(string userId, [FromBody] OneTimePasswordVerificationRequest body)
        {
            var tokens = await _store.TokensByUserIdAsync(userId);
            if (!tokens.Any()) return NotFound();
            var match = tokens.SingleOrDefault(t => t.Password.Equals(body.Password));
            await _checker.ValidateAndThrowAsync(match);
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