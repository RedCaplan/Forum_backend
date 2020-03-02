using System.Net.Http;
using Xunit;

namespace Forum.IntegrationTests.Api
{
    public class UsersController : IClassFixture<WebTestFixture>
    {
        private readonly HttpClient _client;

        public UsersController(WebTestFixture factory)
        {
            _client = factory.CreateClient();
        }

    }
}
