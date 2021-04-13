using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalTreker.Domain;

namespace PersonalTreker.BLL.Contracts

{
    public interface IBoardGetService
    {
        Task ValidateAsync(IBoardContainer boardContainer);
        Task<IEnumerable<Board>> GetAsync();
        Task<Board> GetAsync(IBoardIdentity board);
    }
}