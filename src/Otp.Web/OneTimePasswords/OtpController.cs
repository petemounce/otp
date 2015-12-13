using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using FluentValidation;

namespace Otp.Web.OneTimePasswords
{
    /// <summary>
    /// Operations relating to one time passwords
    /// </summary>
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

        /// <summary>
        /// Use a one time password for a user
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <param name="body">the request body</param>
        /// <response code="200">password was valid for user</response>
        /// <response code="400">Invalid request, check standard error response body</response>
        /// <response code="404">User not found</response>
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

        /// <summary>
        /// Create a new one time password for a user
        /// </summary>
        /// <remarks>Will create the user if not found</remarks>
        /// <param name="userId">the user id</param>
        /// <response code="200">one time password created</response>
        [HttpPost, Route("{userId}")]
        public async Task<IHttpActionResult> Post(string userId)
        {
            var otp = await _store.NewPasswordForAsync(userId, _config.AllowedAgeForPasswords);
            return Ok(otp);
        }
    }
}