using PersonalTreker.Domain;
using System.Threading.Tasks;

namespace PersonalTreker.BLL.Contracts
{
    public interface IBoardCreateService
    {
        Task<Board> CreateAsync(BoardUpdateModel board);
    }
}