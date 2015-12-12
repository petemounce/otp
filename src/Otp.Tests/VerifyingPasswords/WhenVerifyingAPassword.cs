using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Otp.Web.OneTimePasswords;

namespace Otp.Tests.VerifyingPasswords
{
    public abstract class WhenVerifyingAPassword : WhenTestingTheApi
    {
        private string _userId;
        private string _content;
        protected HttpResponseMessage Response;
        private string _existingUserId;
        protected OneTimePassword Created;

        protected override Task Given()
        {
            return Task.Run(async () => 
            {
                _existingUserId = GivenExistingUserId();
                _userId = GivenUserIdOnRequest();
                var pw = await CreatePasswordFor(_existingUserId);
                var otp = await DtoFrom<OneTimePassword>(pw);
                _content = JsonConvert.SerializeObject(new { Password = GivenPasswordOnRequest(otp) });
            });
        }

        protected override async Task When()
        {
            Response = await AttemptPasswordVerification(_userId, _content);
        }

        protected abstract string GivenExistingUserId();

        protected abstract string GivenUserIdOnRequest();

        protected virtual string GivenPasswordOnRequest(OneTimePassword otp)
        {
            return otp.Password;
        }
    }
}