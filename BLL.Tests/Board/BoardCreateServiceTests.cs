using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PersonalTreker.BLL.Contracts;
using PersonalTreker.BLL.Implementation;
using PersonalTreker.DataAccess;
using PersonalTreker.Domain;

namespace PersonalTreker.BLL.Tests
{
    [TestFixture]
    public class BoardCreateServiceTests
    {
        [Test]
        public async Task CreateAsync_InsertEmptyBoardSuccess()
        {
            // Arrange
            var expected = new Board();
            var board = new BoardUpdateModel();
            
            var boardDataAccess = new Mock<IBoardDataAccess>();
            boardDataAccess.Setup(x => x.InsertAsync(board)).ReturnsAsync(expected);

            var boardCreateService = new BoardCreateService(boardDataAccess.Object);
            
            // Act
            var result = await boardCreateService.CreateAsync(board);
            
            // Assert
            result.Should().Be(expected);
        }
    }
}