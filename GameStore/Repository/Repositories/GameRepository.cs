using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository.Repositories;

public class GameRepository:RepositoryBase<Game>,IGameRepository
{
    public GameRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }
    
     public async Task<IEnumerable<Game>> GetAllGamesAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .Include(c => c.Categories)
                .OrderBy(c => c.GameDate)
                .ToListAsync();

        public async Task<Game?> GetGameAsync(Guid gameId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(gameId), trackChanges)
                .SingleOrDefaultAsync();

        public void CreateGame(Game game) => Create(game);

        public async Task<IEnumerable<Game>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges)
                .ToListAsync();

        public async Task<IEnumerable<Game>> GetGamesByUserIdAsync(Guid userId, bool trackChanges) =>
            await FindByCondition(x => x.User != null && x.User.Id.Equals(userId), trackChanges)
                .ToListAsync();


        public async Task<IEnumerable<Game>> GetGamesByUserIdWithDetailsAsync(Guid userId, bool trackChanges) =>
            await FindByCondition(x => x.User.Id.Equals(userId), trackChanges).Include(u => u.User)
                .Include(c => c.Categories)
                .OrderBy(c => c.GameDate)
                .ToListAsync();

        public async Task<IEnumerable<Game>> GetGamesByUserNameWithDetailsAsync(string name, bool trackChanges) =>
            await FindByCondition(x => x.User.UserName.Equals(name), trackChanges).Include(u => u.User)
                 .Include(c => c.Categories)
                .OrderBy(c => c.GameDate)
                .ToListAsync();


        public void DeleteGame(Game game) => Delete(game);


        public async Task<PagedList<Game>> GetAllGamesWithDetailsAsync(GameParameters gameParameters, bool trackChanges)
        {
            
            var games = await FindAll(trackChanges)
                 .FilterGamesByCategory(gameParameters.CategoryName)
                // .FilterGamesByDate(gameParameters.GameFrom, gameParameters.GameTo)
                .Search(gameParameters.SearchTerm)
                .Include(u => u.User)
                .Include(c => c.Categories)
                .Sort(gameParameters.OrderBy)
                .ToListAsync();
           
            return  PagedList<Game>.ToPagedList(games,  gameParameters.PageNumber, gameParameters.PageSize);    
            
        }
        public async Task<Game?> GetGameWithDetailsAsync(Guid gameId, bool trackChanges)
        {
            return await FindByCondition(c => c.Id.Equals(gameId), trackChanges)
                .Include(u => u.User)
                .Include(c => c.Categories)
                .SingleOrDefaultAsync();
        }
}