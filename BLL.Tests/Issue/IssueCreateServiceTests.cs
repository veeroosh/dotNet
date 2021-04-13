using System;
using System.Threading.Tasks;
using AutoFixture;
using NUnit.Framework;
using PersonalTreker.BLL.Contracts;
using PersonalTreker.BLL.Implementation;
using PersonalTreker.DataAccess;
using PersonalTreker.Domain;
using Moq;
using FluentAssertions;

namespace PersonalTreker.BLL.Tests
{
    [TestFixture]
    public class IssueCreateServiceTests
    {
        private IssueUpdateModel issue;
     
        [SetUp]
        public void Setup()
        {
            issue = new IssueUpdateModel();
        }

        [Test]
        public async Task CreateAsync_BoardValidationSucceed_CreateIssue()
        {
            // Arrange
            var expected = new Issue();
            
            var boardGetService = new Mock<IBoardGetService>();
            boardGetService.Setup(x => x.ValidateAsync(issue));

            var issueDataAccess = new Mock<IIssueDataAccess>();
            issueDataAccess.Setup(x => x.InsertAsync(issue)).ReturnsAsync(expected);

            var issueCreateService = new IssueCreateService(boardGetService.Object, issueDataAccess.Object);
            
            // Act
            var result = await issueCreateService.CreateAsync(issue);
            
            // Assert
            result.Should().Be(expected);
        }
        
        [Test]
        public async Task CreateAsync_BoardValidationFailed_ThrowsError()
        {
            // Arrange
            var fixture = new Fixture();
            var expected = fixture.Create<string>();
            
            var boardGetService = new Mock<IBoardGetService>();
            boardGetService.Setup(x => x.ValidateAsync(issue))
                .Throws(new InvalidOperationException(expected));

            var issueDataAccess = new Mock<IIssueDataAccess>();

            var issueCreateService = new IssueCreateService(boardGetService.Object, issueDataAccess.Object);
            
            // Act
            var action = new Func<Task>(() => issueCreateService.CreateAsync(issue));
            
            // Assert
            await action.Should().ThrowAsync<InvalidOperationException>().WithMessage(expected);
            issueDataAccess.Verify(x => x.InsertAsync(issue), Times.Never);
        }
    }
}