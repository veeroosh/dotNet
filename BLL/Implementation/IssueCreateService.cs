using System.Threading.Tasks;
using PersonalTreker.BLL.Contracts;
using PersonalTreker.Domain;
using PersonalTreker.DataAccess;

namespace PersonalTreker.BLL.Implementation
{
    public class IssueCreateService : IIssueCreateService
    {
        private IBoardGetService BoardGetService { get; }
        private IIssueDataAccess IssueDataAccess { get; }
        
        public IssueCreateService(IBoardGetService boardGetService, IIssueDataAccess issueDataAccess)
        {
            BoardGetService = boardGetService;
            IssueDataAccess = issueDataAccess;
        }

        public async Task<Issue> CreateAsync(IssueUpdateModel issue)
        {
            await BoardGetService.ValidateAsync(issue);
            return await IssueDataAccess.InsertAsync(issue);
        }
    }
}