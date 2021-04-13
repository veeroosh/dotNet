using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonalTreker.DataAccess.Entities;
using PersonalTreker.Domain;
using Issue = PersonalTreker.Domain.Issue;

namespace PersonalTreker.DataAccess
{
    public class IssueDataAccess : IIssueDataAccess
    {
        private IssueBoardContext Context { get; }
        private IMapper Mapper { get; }

        public IssueDataAccess(IssueBoardContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public async Task<Issue> InsertAsync(IssueUpdateModel issue)
        {
            var result = await Context.AddAsync(Mapper.Map<Issue>(issue));
            await Context.SaveChangesAsync();
            return Mapper.Map<Issue>(result.Entity);
        }
        
        public async Task<IEnumerable<Issue>> GetAsync()
        {
            return Mapper.Map<IEnumerable<Issue>>(
                await Context.Issue.Include(x => x.Board).ToListAsync());
        }

        public async Task<Issue> GetAsync(IIssueIdentity issue)
        {
            var result = await Get(issue);
            return Mapper.Map<Issue>(result);
        }

        private async Task<IssueEntity> Get(IIssueIdentity issue)
        {
            if (issue == null)
                throw new ArgumentNullException(nameof(Issue));

            return await Context.Issue.FirstOrDefaultAsync(x => x.Id == issue.Id);
        }

        public async Task<Issue> UpdateAsync(IssueUpdateModel issue)
        {
            var existing = await Get(issue);
            var result = Mapper.Map(issue, existing);
            Context.Update(result);
            await Context.SaveChangesAsync();
            return Mapper.Map<Issue>(result);
        }

        public async Task<IEnumerable<Issue>> GetByAsync(IBoardContainer board)
        {
            return Mapper.Map<IEnumerable<Issue>>(
                await Context.Issue.Include(x => x.BoardId == board.BoardId)
                    .ToListAsync());
        }
    }
}