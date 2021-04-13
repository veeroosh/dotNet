using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalTreker.BLL.Contracts;
using PersonalTreker.DataAccess;
using PersonalTreker.Domain;

namespace PersonalTreker.BLL.Implementation
{
    public class IssueGetService : IIssueGetService
    {
        private IIssueDataAccess IssueDataAccess { get; }
        private IBoardGetService BoardGetService { get; }

        public IssueGetService(IIssueDataAccess issueDataAccess, IBoardGetService boardGetService)
        {
            IssueDataAccess = issueDataAccess;
            BoardGetService = boardGetService;
        }

        public Task<IEnumerable<Issue>> GetAsync() 
        {
            return IssueDataAccess.GetAsync();
        }

        public Task<IEnumerable<Issue>> GetByAsync(IBoardContainer board)
        {
            BoardGetService.ValidateAsync(board);
            return IssueDataAccess.GetByAsync(board);
        }

        public Task<Issue> GetAsync(IIssueIdentity issue)
        {
            return IssueDataAccess.GetAsync(issue);
        }
    }
}