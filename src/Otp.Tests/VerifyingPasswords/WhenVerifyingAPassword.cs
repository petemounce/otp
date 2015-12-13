using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Otp.Web.OneTimePasswords;

namespace Otp.Tests.VerifyingPasswords
{
    public abstract class WhenVerifyingAPassword : WhenTestingTheApi
    {
        private string _userId;
        private string _content;
        protected HttpResponseMessage Response;
        private string _existingUserId;

        protected override async Task Given()
        {
            _existingUserId = GivenExistingUserId();
            _userId = GivenUserIdOnRequest();
            var res = await CreatePasswordFor(_existingUserId);
            var otp = await DtoFrom(res);
            _content = JsonConvert.SerializeObject(new OneTimePasswordVerificationRequest { Password = GivenPasswordOnRequest(otp) });
        }

        protected override async Task When()
        {
            Response = await AttemptPasswordVerification(_userId, _content);
        }

        protected abstract string GivenExistingUserId();

        protected abstract string GivenUserIdOnRequest();

        protected virtual string GivenPasswordOnRequest(JObject otp)
        {
            return (string)otp["Password"];
        }
    }
}