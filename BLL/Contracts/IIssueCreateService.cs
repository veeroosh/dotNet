using System.Threading.Tasks;
using PersonalTreker.Domain;

namespace PersonalTreker.BLL.Contracts
{
    public interface IIssueCreateService
    {
        Task<Issue> CreateAsync(IssueUpdateModel issue);
    }
}