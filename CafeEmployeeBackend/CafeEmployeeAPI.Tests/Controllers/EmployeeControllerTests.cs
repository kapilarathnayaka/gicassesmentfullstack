using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace CafeEmployeeAPI.Tests.Controllers
{
    public class EmployeeControllerTests : IntegrationTestBase
    {
        private readonly HttpClient _client;

        public EmployeeControllerTests()
        {
            _client = CreateClient();
        }

        [Fact]
        public async Task CreateEmployee_ShouldReturnCreatedResult_WhenValidDataIsProvided()
        {
            // Arrange
            var employee = new
            {
                Id = "UI1234567",
                Name = "John Doe",
                EmailAddress = "johndoe@example.com",
                PhoneNumber = "91234567",
                Gender = "Male",
                CafeId = "123"
            };
            var content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/employee", content);

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(201); // Created
        }

        [Fact]
        public async Task GetEmployee_ShouldReturnEmployee_WhenExists()
        {
            // Arrange
            var employeeId = "UI1234567";

            // Act
            var response = await _client.GetAsync($"/employees/{employeeId}");

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("John Doe");
        }

        [Fact]
        public async Task GetEmployees_ShouldReturnAllEmployees_WhenNoCafeProvided()
        {
            // Act
            var response = await _client.GetAsync("/employees");

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("John Doe");
        }

        [Fact]
        public async Task UpdateEmployee_ShouldReturnOkResult_WhenValidDataIsProvided()
        {
            // Arrange
            var employee = new
            {
                Id = "UI1234567",
                Name = "John Doe Updated",
                EmailAddress = "johnupdated@example.com",
                PhoneNumber = "91234567",
                Gender = "Male",
                CafeId = "123"
            };
            var content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("/employee", content);

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(200); // OK
        }

        [Fact]
        public async Task DeleteEmployee_ShouldReturnOkResult_WhenValidIdIsProvided()
        {
            // Arrange
            var employeeId = "UI1234567";

            // Act
            var response = await _client.DeleteAsync($"/employee/{employeeId}");

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(200); // OK
        }
    }
}
