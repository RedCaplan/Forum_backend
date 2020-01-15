using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Forum.IntegrationTests.Api
{
    public class CategoryController : IClassFixture<WebTestFixture>
    {
        private readonly HttpClient _client;

        public CategoryController(WebTestFixture factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task DeleteCategoryUnauthorized_ShouldReturn401()
        {
            var response = await _client.DeleteAsync("api/Category/1/delete");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
