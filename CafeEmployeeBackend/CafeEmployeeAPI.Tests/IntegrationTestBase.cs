using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;

namespace CafeEmployeeAPI.Tests
{
    public class IntegrationTestBase
    {
        protected HttpClient CreateClient()
        {
            var factory = new WebApplicationFactory<Program>();
            return factory.CreateClient();
        }
    }
}
