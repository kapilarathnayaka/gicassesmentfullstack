using Moq;
using FluentAssertions;
using Xunit;
using CafeEmployeeAPI.Application.Interfaces.Repositories;
using CafeEmployeeAPI.Domain.Entities;
using CafeEmployeeAPI.Infrastructure.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CafeEmployeeAPI.Tests.Repositories
{
    public class EmployeeRepositoryTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepoMock;

        public EmployeeRepositoryTests()
        {
            _employeeRepoMock = new Mock<IEmployeeRepository>();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEmployee_WhenExists()
        {
            // Arrange
            var employee = new Employee { Id = "UI1234567", Name = "John Doe" };
            _employeeRepoMock.Setup(repo => repo.GetByIdAsync("UI1234567")).ReturnsAsync(employee);

            // Act
            var result = await _employeeRepoMock.Object.GetByIdAsync("UI1234567");

            // Assert
            result.Should().BeEquivalentTo(employee);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmployees()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { Id = "UI1234567", Name = "John Doe" },
                new Employee { Id = "UI1234568", Name = "Jane Doe" }
            };
            _employeeRepoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(employees);

            // Act
            var result = await _employeeRepoMock.Object.GetAllAsync();

            // Assert
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(employees);
        }
    }
}
