using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace CafeEmployee.Tests.IntegrationTests
{
    public class EmployeeIntegrationTests : IClassFixture<TestFixture>
    {
        private readonly HttpClient _client;

        public EmployeeIntegrationTests(TestFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task CreateEmployee_ReturnsSuccessStatusCode()
        {
            var request = new
            {
                Url = "/api/Employee",
                Body = new
                {
                    Name = "John Doe",
                    Position = "Manager",
                    Age = 30
                }
            };

            var response = await _client.PostAsync(request.Url, new StringContent(JsonConvert.SerializeObject(request.Body), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetEmployees_ReturnsSuccessStatusCode()
        {
            var request = new
            {
                Url = "/api/Employee"
            };

            var response = await _client.GetAsync(request.Url);

            response.EnsureSuccessStatusCode();
        }
    }
}
