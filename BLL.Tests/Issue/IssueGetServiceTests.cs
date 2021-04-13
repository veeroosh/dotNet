using System;
using System.Collections.Generic;
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
    public class IssueGetServiceTests
    {
        [Test]
        public async Task GetAsync_GetEmptyIssueList()
        {
            // Arrange
            var expected = new List<Issue>();

            var issueDataAccess = new Mock<IIssueDataAccess>();
            issueDataAccess.Setup(x => x.GetAsync()).ReturnsAsync(expected);

            var boardGetService = new Mock<IBoardGetService>();

            var issueGetService = new IssueGetService(issueDataAccess.Object, boardGetService.Object);

            // Act
            var result = await issueGetService.GetAsync();

            // Assert
            result.Should().Equal(expected);
        }

        [Test]
        public async Task GetAsync_GetNotEmptyIssueList()
        {
            // Arrange
            var expected = new List<Issue> { new Issue() };

            var issueDataAccess = new Mock<IIssueDataAccess>();
            issueDataAccess.Setup(x => x.GetAsync()).ReturnsAsync(expected);

            var boardGetService = new Mock<IBoardGetService>();

            var issueGetService = new IssueGetService(issueDataAccess.Object, boardGetService.Object);

            // Act
            var result = await issueGetService.GetAsync();

            // Assert
            result.Should().Equal(expected);
        }

        [Test]
        public async Task GetAsync_GetIssue()
        {
            // Arrange
            var issue = new Issue();

            var issueIdentity = new Mock<IIssueIdentity>();

            var issueDataAccess = new Mock<IIssueDataAccess>();
            issueDataAccess.Setup(x => x.GetAsync(issueIdentity.Object)).ReturnsAsync(issue);

            var boardGetService = new Mock<IBoardGetService>();
            var issueGetService = new IssueGetService(issueDataAccess.Object, boardGetService.Object);

            // Act
            var result = await issueGetService.GetAsync(issueIdentity.Object);

            // Assert
            result.Should().Equals(issue);
        }

        [Test]
        public async Task GetAsync_IssueDoesntExist()
        {
            // Arrange
            var fixture = new Fixture();
            var id = fixture.Create<int>();

            var issueIdentity = new Mock<IIssueIdentity>();
            issueIdentity.Setup(x => x.Id).Returns(id);

            var issue = new Issue();
            var issueDataAccess = new Mock<IIssueDataAccess>();
            var boardGetService = new Mock<IBoardGetService>();

            var issueGetService = new IssueGetService(issueDataAccess.Object, boardGetService.Object);

            // Act
            var result = issueGetService.GetAsync(issueIdentity.Object);

            // Assert
            result.Should().Equals(issue);
        }

        [Test]
        public async Task GetByAsync_GetEmptyListForBoard()
        {
            // Arrange
            var expected = new List<Issue>();
            var fixture = new Fixture();
            var id = fixture.Create<int>();

            var boardContainer = new Mock<IBoardContainer>();
            boardContainer.Setup(x => x.BoardId).Returns(id);

            var issueDataAccess = new Mock<IIssueDataAccess>();
            issueDataAccess.Setup(x => x.GetByAsync(boardContainer.Object)).ReturnsAsync(expected);

            var boardGetService = new Mock<IBoardGetService>();

            var issueGetService = new IssueGetService(issueDataAccess.Object, boardGetService.Object);

            // Act
            var result = await issueGetService.GetByAsync(boardContainer.Object);

            // Assert
            result.Should().Equal(expected);
        }

        [Test]
        public async Task GetByAsync_BoardValidationFailure_ThrowsError()
        {
            // Arrange
            var fixture = new Fixture();
            var expected = fixture.Create<string>();
        
            var boardContainer = new IssueUpdateModel();
        
            var boardGetService = new Mock<IBoardGetService>();
            boardGetService.Setup(x => x.ValidateAsync(boardContainer))
              .Throws(new InvalidOperationException(expected));
            
            var issueDataAccess = new Mock<IIssueDataAccess>();
        
            var issueGetService = new IssueGetService(issueDataAccess.Object, boardGetService.Object);
            
            // Act
            var action = new Func<Task>(() => issueGetService.GetByAsync(boardContainer));
            
            // Assert
            await action.Should().ThrowAsync<InvalidOperationException>().WithMessage(expected);
        }
    }
}