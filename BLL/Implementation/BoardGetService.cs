using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalTreker.BLL.Contracts;
using PersonalTreker.DataAccess;
using PersonalTreker.Domain;

namespace PersonalTreker.BLL.Implementation
{
    public class BoardGetService : IBoardGetService
    {
        private IBoardDataAccess BoardDataAccess { get; set; }

        public BoardGetService(IBoardDataAccess boardDataAccess)
        {
            BoardDataAccess = boardDataAccess;
        }

        public Task<IEnumerable<Board>> GetAsync()
        {
            return BoardDataAccess.GetAsync();
        }

        public Task<Board> GetAsync(IBoardIdentity board)
        {
            return BoardDataAccess.GetAsync(board);
        }
        
        public async Task ValidateAsync(IBoardContainer boardContainer)
        {
            if (boardContainer == null)
                throw new ArgumentNullException(nameof(boardContainer));
            
            var department = await GetBy(boardContainer);

            if (boardContainer.BoardId.HasValue && department == null)
                throw new InvalidOperationException($"Board not found by id {boardContainer.BoardId}");
        }

        private async Task<Board> GetBy(IBoardContainer boardContainer) 
        {
            return await BoardDataAccess.GetByAsync(boardContainer);
        }
    }
}