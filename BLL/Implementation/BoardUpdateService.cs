using System.Threading.Tasks;
using PersonalTreker.BLL.Contracts;
using PersonalTreker.DataAccess;
using PersonalTreker.Domain;

namespace PersonalTreker.BLL.Implementation
{
    public class BoardUpdateService : IBoardUpdateService
    {
        private IBoardDataAccess BoardDataAccess { get; }
        private IBoardGetService BoardGetService { get;  }
        
        public BoardUpdateService(IBoardDataAccess boardDataAccess, IBoardGetService boardGetService)
        {
            BoardDataAccess = boardDataAccess;
            BoardGetService = boardGetService;
        }

        public async Task<Board> UpdateAsync(BoardUpdateModel board)
        {
            await BoardGetService.ValidateAsync(board);
            return await BoardDataAccess.UpdateAsync(board);
        }
    }
}