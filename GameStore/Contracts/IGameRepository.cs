using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IGameRepository
{
    // Task<IEnumerable<Game>> GetAllGamesAsync(bool trackChanges);
    Task<Game?> GetGameAsync(Guid gameId, bool trackChanges);
    void CreateGame(Game game);
    Task<IEnumerable<Game>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    Task<IEnumerable<Game>> GetGamesByUserNameWithDetailsAsync(string name, bool trackChanges);
    Task<IEnumerable<Game>> GetGamesByUserIdWithDetailsAsync(Guid userId, bool trackChanges);

    void DeleteGame(Game game);

    #region Pagination
     Task<PagedList<Game>> GetAllGamesWithDetailsAsync(GameParameters gameParameters,bool trackChanges);
  

        #endregion 
    Task<Game?> GetGameWithDetailsAsync(Guid gameId,bool trackChanges);
}