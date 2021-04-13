using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PersonalTreker.BLL.Implementation;
using PersonalTreker.DataAccess;
using PersonalTreker.Domain;

namespace PersonalTreker.BLL.Tests
{
    [TestFixture]
    public class BoardGetServiceTests
    {
        [Test]
        public async Task GetAsync_GetEmptyBoardList()
        {
            // Arrange
            var expected = new List<Board>();

            var issueDataAccess = new Mock<IBoardDataAccess>();
            issueDataAccess.Setup(x => x.GetAsync()).ReturnsAsync(expected);

            var issueGetService = new BoardGetService(issueDataAccess.Object);

            // Act
            var result = await issueGetService.GetAsync();

            // Assert
            result.Should().Equal(expected);
        }
        
        [Test]
        public async Task GetAsync_GetNotEmptyBoardList()
        {
            // Arrange
            var expected = new List<Board> { new Board() };

            var boardDataAccess = new Mock<IBoardDataAccess>();
            boardDataAccess.Setup(x => x.GetAsync()).ReturnsAsync(expected);
            
            var boardGetService = new BoardGetService(boardDataAccess.Object);

            // Act
            var result = await boardGetService.GetAsync();

            // Assert
            result.Should().Equal(expected);
        }
        
        [Test]
        public async Task GetAsync_GetBoard()
        {
            // Arrange
            var board = new Board();

            var boardIdentity = new Mock<IBoardIdentity>();

            var boardDataAccess = new Mock<IBoardDataAccess>();
            boardDataAccess.Setup(x => x.GetAsync(boardIdentity.Object)).ReturnsAsync(board);

            var boardGetService = new BoardGetService(boardDataAccess.Object);

            // Act
            var result = await boardGetService.GetAsync(boardIdentity.Object);

            // Assert
            result.Should().Equals(board);
        }
        
        [Test]
        public async Task GetAsync_BoardDoesntExist()
        {
            // Arrange
            var fixture = new Fixture();
            var id = fixture.Create<int>();

            var boardIdentity = new Mock<IBoardIdentity>();
            boardIdentity.Setup(x => x.Id).Returns(id);

            var board = new Board();
            var boardDataAccess = new Mock<IBoardDataAccess>();

            var boardGetService = new BoardGetService(boardDataAccess.Object);

            // Act
            var result = boardGetService.GetAsync(boardIdentity.Object);

            // Assert
            result.Should().Equals(board);
        }

        [Test]
        public async Task ValidateAsync_BoardExist()
        {
            // Arrange
            var boardContainer = new Mock<IBoardContainer>();

            var board = new Board();
            var boardDataAccess = new Mock<IBoardDataAccess>();
            boardDataAccess.Setup(x => x.GetByAsync(boardContainer.Object)).ReturnsAsync(board);

            var boardGetService = new BoardGetService(boardDataAccess.Object);
            
            // Act
            var action = new Func<Task>(() => boardGetService.ValidateAsync(boardContainer.Object));
            
            // Assert
            await action.Should().NotThrowAsync<Exception>();
        }

        [Test]
        public async Task ValidateAsync_BoardNotExist()
        {
            // Arrange
            var fixture = new Fixture();
            var id = fixture.Create<int>();
            
            var boardContainer = new Mock<IBoardContainer>();
            boardContainer.Setup(x => x.BoardId).Returns(id);

            var board = new Board();
            var boardDataAccess = new Mock<IBoardDataAccess>();
            boardDataAccess.Setup(x => x.GetByAsync(boardContainer.Object))
                .ReturnsAsync((Board)null);

            var boardGetService = new BoardGetService(boardDataAccess.Object);
            
            // Act
            var action = new Func<Task>(() => boardGetService.ValidateAsync(boardContainer.Object));
            
            // Assert
            await action.Should().ThrowAsync<InvalidOperationException>($"Board not found by id {id}");
        }
    }
}