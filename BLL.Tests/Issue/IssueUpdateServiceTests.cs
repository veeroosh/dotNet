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
    public class IssueUpdateServiceTests
    {
        IssueUpdateModel issue;
        
        [SetUp]
        public void Setup()
        {
            issue = new IssueUpdateModel();
        }
        
        [Test]
        public async Task UpdateAsync_UpdateIssue_BoardValidationSuccess()
        {
            // Arrange
            var expected = new Issue();

            var issueDataAccess = new Mock<IIssueDataAccess>();
            issueDataAccess.Setup(x => x.UpdateAsync(issue))
                .ReturnsAsync(expected);

            var boardGetService = new Mock<IBoardGetService>();
            var issueUpdateService = new IssueUpdateService(issueDataAccess.Object, boardGetService.Object);

            // Act
            var result = await issueUpdateService.UpdateAsync(issue);

            // Assert
            result.Should().Equals(expected);
        }
        
        [Test]
        public async Task UpdateAsync_UpdateIssue_BoardValidationFailure()
        {
            // Arrange
            var fixture = new Fixture();
            var expected = fixture.Create<string>();
            
            var boardGetService = new Mock<IBoardGetService>();
            boardGetService.Setup(x => x.ValidateAsync(issue))
                .Throws(new InvalidOperationException(expected));

            var issueDataAccess = new Mock<IIssueDataAccess>();

            var issueUpdateService = new IssueUpdateService(issueDataAccess.Object, boardGetService.Object);
            
            // Act
            var action = new Func<Task>(() => issueUpdateService.UpdateAsync(issue));

            // Assert
            await action.Should().ThrowAsync<InvalidOperationException>().WithMessage(expected);
            issueDataAccess.Verify(x => x.UpdateAsync(issue), Times.Never);
        }
    }
}