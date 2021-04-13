using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonalTreker.Domain;
using PersonalTreker.DataAccess.Entities;

namespace PersonalTreker.DataAccess
{
    public class BoardDataAccess : IBoardDataAccess
    {
        private IssueBoardContext Context { get; }
        private IMapper Mapper { get; }
        
        public BoardDataAccess(IssueBoardContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public async Task<Board> InsertAsync(BoardUpdateModel board)
        {
            var result = await this.Context.AddAsync(Mapper.Map<Board>(board));
            await Context.SaveChangesAsync();
            return Mapper.Map<Board>(result.Entity);
        }

        public async Task<IEnumerable<Board>> GetAsync()
        {
            return Mapper.Map<IEnumerable<Board>>(await Context.Board.ToListAsync());
        }

        public async Task<Board> GetAsync(IBoardIdentity board)
        {
            var result = await Get(board);
            return Mapper.Map<Board>(result);
        }

        private async Task<BoardEntity> Get(IBoardIdentity board)
        {
            if (board == null)
                throw new ArgumentNullException(nameof(board));
            
            return await Context.Board     //.Include(x => x.Id) // need to check x.Id
                .FirstOrDefaultAsync(x => x.Id == board.Id);
        }

        public async Task<Board> UpdateAsync(BoardUpdateModel board)
        {
            var existing = await Get(board);
            var result = Mapper.Map(board, existing);
            Context.Update(result);
            await Context.SaveChangesAsync();
            return Mapper.Map<Board>(result);
        }

        public async Task<Board> GetByAsync(IBoardContainer board)
        {
            return board.BoardId.HasValue ? 
                Mapper.Map<Board>(await Context.Board.FirstOrDefaultAsync(x => x.Id == board.BoardId))
                : null;
        }
    }
}