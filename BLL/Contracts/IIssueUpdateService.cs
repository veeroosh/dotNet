using System.Threading.Tasks;
using PersonalTreker.Domain;

namespace PersonalTreker.BLL.Contracts
{
    public interface IIssueUpdateService
    {
        Task<Issue> UpdateAsync(IssueUpdateModel issue);
    }
}