using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalTreker.Domain;

namespace PersonalTreker.BLL.Contracts
{
    public interface IIssueGetService
    {
        Task<IEnumerable<Issue>> GetAsync();
        Task<IEnumerable<Issue>> GetByAsync(IBoardContainer board);
        Task<Issue> GetAsync(IIssueIdentity issue);
    }
}