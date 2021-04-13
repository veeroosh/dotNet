using System.Threading.Tasks;
using PersonalTreker.Domain;

namespace PersonalTreker.BLL.Contracts
{
    public interface IBoardUpdateService
    {
        Task<Board> UpdateAsync(BoardUpdateModel board);
    }
}