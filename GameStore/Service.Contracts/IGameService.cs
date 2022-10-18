using Shared.DataTransferObjects.ForCreation;
using Shared.DataTransferObjects.ForShow;
using Shared.DataTransferObjects.ForUpdate;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IGameService
{
    Task<(IEnumerable<GameDto> games, MetaData metaData)> GetAllGamesAsync(GameParameters gameParameters,bool trackChanges);
    Task<GameDto> GetGameAsync(Guid gameId, bool trackChanges);
    Task<GameDto> CreateGameAsync(GameForCreationDto game, string name);
    // Task<IEnumerable<GameDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        

    Task DeleteGameAsync(Guid gameId, bool trackChanges);
    Task UpdateGameAsync(Guid gameId, GameForUpdateDto gameForUpdate, bool trackChanges);

    Task<IEnumerable<GameDto>> GetGamesByUserName(string name, bool trackChanges);
}