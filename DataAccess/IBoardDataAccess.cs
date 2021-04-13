using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalTreker.Domain;

namespace PersonalTreker.DataAccess
{
    public interface IBoardDataAccess
    {
        Task<Board> InsertAsync(BoardUpdateModel board);
        Task<IEnumerable<Board>> GetAsync();
        Task<Board> GetAsync(IBoardIdentity boardId);
        Task<Board> UpdateAsync(BoardUpdateModel board);
        Task<Board> GetByAsync(IBoardContainer boardContainer);
    }
}