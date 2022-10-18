using AutoMapper;
using Contracts;
using Entities.Exceptions.BadRequestExceptions;
using Entities.Exceptions.NotFoundExceptions;
using Entities.Models;
using LoggingService;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DataTransferObjects.ForCreation;
using Shared.DataTransferObjects.ForShow;
using Shared.DataTransferObjects.ForUpdate;
using Shared.RequestFeatures;

namespace Service;

internal sealed class GameService : IGameService
{
    private readonly ILoggerManager _logger;
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private User? _user;

    public GameService(ILoggerManager logger, IRepositoryManager repository, IMapper mapper,
        UserManager<User> userManager)
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<(IEnumerable<GameDto> games, MetaData metaData)> GetAllGamesAsync(GameParameters gameParameters,
        bool trackChanges)
    {
        if (!gameParameters.ValidDateTimeRange)
            throw new MaxDateRangeBadRequestException();

        var gamesWithMetaData = await _repository.Game.GetAllGamesWithDetailsAsync(gameParameters, trackChanges);


        var gamesDto = _mapper.Map<IEnumerable<GameDto>>(gamesWithMetaData);


        return (games: gamesDto, metaData: gamesWithMetaData.MetaData);
    }

    public async Task<GameDto> GetGameAsync(Guid id, bool trackChanges)
    {
        var game = await GetGameAndCheckIfItExists(id, trackChanges);
        var gameDto = _mapper.Map<GameDto>(game);

        return gameDto;
    }

    public async Task<GameDto> CreateGameAsync(GameForCreationDto game, string name)
    {
        var gameEntity = await ConvertGameForCreationDtoToGame(game, name);
        
        
        // var gameEntity = _mapper.Map<Game>(game);
        // gameEntity.User = await _userManager.FindByNameAsync(name);
        // gameEntity.GameDate = DateTime.Now;
        //   // todo convert all categories stirng ids to Category entity
        //   var categories = await _repository.Category.GetByIdsAsync(game.Categories, true);
        //   gameEntity.Categories = categories;
        _repository.Game.CreateGame(gameEntity);
        await _repository.SaveAsync();

        var gameToReturn = _mapper.Map<GameDto>(gameEntity);

        return gameToReturn;
    }

    private async Task<Game> ConvertGameForCreationDtoToGame(GameForCreationDto game, string name)
    {
        IEnumerable<Category>? categories = null;
        if (game.Categories != null)
        {
         categories =   await _repository.Category.GetByIdsAsync(game.Categories,true);
        }
        return new Game
        {
            Id = default,
            Title = game.Title,
            Body = game.Body,
            Price = game.Price,
            PhotoUrl = game.PhotoUrl,
            GameDate = DateTime.Now,
            Categories = categories,
            User = await _userManager.FindByNameAsync(name)
        };
    }


    public async Task<IEnumerable<GameDto>> GetGamesByUserName(string name, bool trackChanges)
    {
        _user = await _userManager.FindByNameAsync(name);
        if (_user is null)
            throw new IdParametersBadRequestException();

        var game = await _repository.Game.GetGamesByUserNameWithDetailsAsync(name, trackChanges);
        var gamesToReturn = _mapper.Map<IEnumerable<GameDto>>(game);

        return gamesToReturn;
    }


    public async Task DeleteGameAsync(Guid gameId, bool trackChanges)
    {
        var game = await GetGameAndCheckIfItExists(gameId, trackChanges);

        _repository.Game.DeleteGame(game);
        await _repository.SaveAsync();
    }

    public async Task UpdateGameAsync(Guid gameId,
        GameForUpdateDto gameForUpdate, bool trackChanges)
    {
        IEnumerable<Category>? categories = null;
        if (gameForUpdate.Categories != null)
        {
            categories =   await _repository.Category.GetByIdsAsync(gameForUpdate.Categories,true);
        }
        
        var game = await GetGameAndCheckIfItExists(gameId, trackChanges);
        game.User = await _userManager.FindByIdAsync(game.User?.Id);
        game.GameDate = DateTime.Now;
        game.Categories = categories;
        game.Body = gameForUpdate.Body;
        game.Price = gameForUpdate.Price;
        game.Title = gameForUpdate.Title;
        game.PhotoUrl = game.PhotoUrl;
        
        await _repository.SaveAsync();
    }

    
    private async Task<Game> GetGameAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var game = await _repository.Game.GetGameWithDetailsAsync(id, trackChanges);
        if (game is null)
            throw new GameNotFoundException(id);

        game.User = await GetUserAndCheckIfItExists(Guid.Parse(game.User.Id));
        return game;
    }

    private async Task<User> GetUserAndCheckIfItExists(Guid userid)
    {
        _user = await _userManager.FindByIdAsync(userid.ToString());

        if (_user is null)
            throw new UserNotFoundException(userid);

        return _user;
    }
}