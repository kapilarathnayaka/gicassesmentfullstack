using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CafeEmployeeAPI.Tests.Controllers
{
    public class CafeControllerTests : IntegrationTestBase
    {
        private readonly HttpClient _client;

        public CafeControllerTests()
        {
            _client = CreateClient();
        }

        [Fact]
        public async Task GetCafes_ShouldReturnOkResult_WhenLocationIsValid()
        {
            // Arrange
            var location = "Downtown";

            // Act
            var response = await _client.GetAsync($"/cafes?location={location}");

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("Downtown");
        }

        [Fact]
        public async Task GetCafes_ShouldReturnEmptyList_WhenLocationIsInvalid()
        {
            // Arrange
            var location = "InvalidLocation";

            // Act
            var response = await _client.GetAsync($"/cafes?location={location}");

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Be("[]");
        }
    }
}
