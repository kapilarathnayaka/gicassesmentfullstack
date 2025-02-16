using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using CafeEmployee.Application.Features.Employees.Commands;
using CafeEmployee.Application.Features.Employees.Handlers;
using CafeEmployee.Domain.Interfaces;

namespace CafeEmployee.Tests.UnitTests
{
    public class CreateEmployeeCommandHandlerTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly CreateEmployeeHandler _handler;

        public CreateEmployeeCommandHandlerTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _handler = new CreateEmployeeHandler(_employeeRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsEmployeeId()
        {
            // Arrange
            var command = new CreateEmployeeCommand
            {
                Name = "John Doe",
                Position = "Manager",
                Age = 30
            };

            var employeeId = Guid.NewGuid();
            _employeeRepositoryMock.Setup(repo => repo.CreateEmployeeAsync(command.Name, command.Position, command.Age))
                .ReturnsAsync(employeeId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(employeeId, result);
            _employeeRepositoryMock.Verify(repo => repo.CreateEmployeeAsync(command.Name, command.Position, command.Age), Times.Once);
        }
    }
}
