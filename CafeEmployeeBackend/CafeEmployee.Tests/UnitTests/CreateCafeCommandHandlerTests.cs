using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using CafeEmployee.Application.Features.Cafes.Commands;
using CafeEmployee.Application.Features.Cafes.Handlers;
using CafeEmployee.Domain.Entities;
using CafeEmployee.Domain.Interfaces;
using AutoMapper;
using CafeEmployee.Dtos;

namespace CafeEmployee.Tests.UnitTests
{
    public class CreateCafeCommandHandlerTests
    {
        private readonly Mock<ICafeRepository> _cafeRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CreateCafeHandler _handler;

        public CreateCafeCommandHandlerTests()
        {
            _cafeRepositoryMock = new Mock<ICafeRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new CreateCafeHandler(_cafeRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsCafeDto()
        {
            // Arrange
            var command = new CreateCafeCommand
            {
                Name = "Test Cafe",
                Location = "Test Location",
                Description = "Test Description"
            };

            var cafe = new Cafe
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Location = command.Location,
                Description = command.Description
            };

            var cafeDto = new CafeDto
            {
                Id = cafe.Id,
                Name = cafe.Name,
                Location = cafe.Location,
                Description = cafe.Description
            };

            _cafeRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Cafe>())).ReturnsAsync(cafe);
            _mapperMock.Setup(mapper => mapper.Map<CafeDto>(It.IsAny<Cafe>())).Returns(cafeDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cafeDto.Id, result.Id);
            Assert.Equal(cafeDto.Name, result.Name);
            Assert.Equal(cafeDto.Location, result.Location);
            Assert.Equal(cafeDto.Description, result.Description);
        }
    }
}
