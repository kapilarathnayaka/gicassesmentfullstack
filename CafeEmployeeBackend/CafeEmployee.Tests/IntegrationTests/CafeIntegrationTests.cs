using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace CafeEmployee.Tests.IntegrationTests
{
    public class CafeIntegrationTests : IClassFixture<TestFixture>
    {
        private readonly HttpClient _client;

        public CafeIntegrationTests(TestFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task CreateCafe_ReturnsOk()
        {
            var request = new
            {
                Url = "/api/Cafe",
                Body = new
                {
                    Name = "Test Cafe",
                    Location = "Test Location",
                    Description = "Test Description"
                }
            };

            var response = await _client.PostAsync(request.Url, new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetCafes_ReturnsOk()
        {
            var response = await _client.GetAsync("/api/Cafe");
            response.EnsureSuccessStatusCode();
        }
    }
}
