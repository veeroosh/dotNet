using System.Threading.Tasks;
using PersonalTreker.BLL.Contracts;
using PersonalTreker.DataAccess;
using PersonalTreker.Domain;

namespace PersonalTreker.BLL.Implementation
{
    public class BoardCreateService : IBoardCreateService
    {
        private IBoardDataAccess BoardDataAccess { get; }

        public BoardCreateService(IBoardDataAccess boardDataAccess)
        {
            BoardDataAccess = boardDataAccess;
        }
        
        public async Task<Board> CreateAsync(BoardUpdateModel board)
        {
            return await BoardDataAccess.InsertAsync(board);
        }
    }
}