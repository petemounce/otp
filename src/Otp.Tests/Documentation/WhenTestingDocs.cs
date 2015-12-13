using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Otp.Tests.Documentation
{
    public abstract class WhenTestingDocs : WhenTestingTheApi
    {
        protected HttpResponseMessage Response;
        protected JObject Body;

        protected override Task Given()
        {
            return Task.Run(() => {});
        }
    }
}