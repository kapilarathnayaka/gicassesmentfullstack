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
    public class CafeRepositoryTests
    {
        private readonly Mock<ICafeRepository> _cafeRepoMock;

        public CafeRepositoryTests()
        {
            _cafeRepoMock = new Mock<ICafeRepository>();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCafe_WhenExists()
        {
            // Arrange
            var cafe = new Cafe { Id = "123", Name = "Cafe One", Location = "Downtown" };
            _cafeRepoMock.Setup(repo => repo.GetByIdAsync("123")).ReturnsAsync(cafe);

            // Act
            var result = await _cafeRepoMock.Object.GetByIdAsync("123");

            // Assert
            result.Should().BeEquivalentTo(cafe);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnCafes()
        {
            // Arrange
            var cafes = new List<Cafe>
            {
                new Cafe { Id = "123", Name = "Cafe One", Location = "Downtown" },
                new Cafe { Id = "124", Name = "Cafe Two", Location = "Uptown" }
            };
            _cafeRepoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(cafes);

            // Act
            var result = await _cafeRepoMock.Object.GetAllAsync();

            // Assert
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(cafes);
        }

        [Fact]
        public async Task AddAsync_ShouldAddCafeSuccessfully()
        {
            // Arrange
            var cafe = new Cafe { Id = "125", Name = "Cafe Three", Location = "Midtown" };
            _cafeRepoMock.Setup(repo => repo.AddAsync(cafe)).ReturnsAsync(cafe);

            // Act
            var result 
