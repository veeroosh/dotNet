using System.Threading.Tasks;
using PersonalTreker.BLL.Contracts;
using PersonalTreker.DataAccess;
using PersonalTreker.Domain;

namespace PersonalTreker.BLL.Implementation
{
    public class IssueUpdateService : IIssueUpdateService
    {
        private IIssueDataAccess IssueDataAccess { get; }
        private IBoardGetService BoardGetService { get; }

        public IssueUpdateService(IIssueDataAccess issueDataAccess, IBoardGetService boardGetService)
        {
            IssueDataAccess = issueDataAccess;
            BoardGetService = boardGetService;
        }

        public async Task<Issue> UpdateAsync(IssueUpdateModel issue)
        {
            await BoardGetService.ValidateAsync(issue);
            return await IssueDataAccess.UpdateAsync(issue);
        }
    }
}