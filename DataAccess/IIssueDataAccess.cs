using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalTreker.Domain;

namespace PersonalTreker.DataAccess
{
    public interface IIssueDataAccess
    {
        Task<Issue> InsertAsync(IssueUpdateModel issue);
        Task<IEnumerable<Issue>> GetAsync();
        Task<Issue> GetAsync(IIssueIdentity issueId);
        Task<Issue> UpdateAsync(IssueUpdateModel issue);
        Task<IEnumerable<Issue>> GetByAsync(IBoardContainer board);
    }
}