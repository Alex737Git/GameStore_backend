using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Service.Contracts;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Shared.DataTransferObjects.ForCreation;
using Shared.DataTransferObjects.ForShow;
using Shared.DataTransferObjects.ForUpdate;
using Shared.RequestFeatures;

namespace Presentation.Controllers
{
     [Route("api/game")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IServiceManager _service;
        
        public GameController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetGames([FromQuery] GameParameters gameParameters)
        {
            var pagedGames = await _service.GameService.GetAllGamesAsync(gameParameters,false);
         
           var response=   new GameDtoWithPagination{Games = pagedGames.games, Data = pagedGames.metaData};
            
          
              return Ok(response);
        }

        [HttpGet("{id:guid}", Name = "GameById")]
      
        public async Task<IActionResult> GetGame(Guid id)
        {
            var game = await _service.GameService.GetGameAsync(id, false);
            return Ok(game);
        }

        [HttpGet("user")]
         [Authorize(Roles = "Authenticated")]
        public async Task<IActionResult> GetGamesByUserName()
        {
            var game = await _service.GameService.GetGamesByUserName(User.Identity.Name, false);
            return Ok(game);
        }
        

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Authorize]
        public async Task<IActionResult> CreateGame([FromBody] GameForCreationDto game)
        {
            var createdGame = await _service.GameService.CreateGameAsync(game, User.Identity.Name);

            return CreatedAtAction("CreateGame", new { id = createdGame.Id }, createdGame);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteGame(Guid id)
        {
            await _service.GameService.DeleteGameAsync(id, false);

            return NoContent();
        }

        [HttpPut]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Authorize]
        public async Task<IActionResult> UpdateGame( [FromBody] GameForUpdateDto game)
        {
            await _service.GameService.UpdateGameAsync(game.Id, game, true);

            return NoContent();
        }
        
        [HttpPost("uploadPhoto")]
        [Authorize]
        public async Task<IActionResult> Upload()
        {
           

            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.First();

            Regex reg = new Regex(".(gif|jpe?g|tiff?|png|webp|bmp)$",RegexOptions.IgnoreCase);
            if (file.Length<2097152&&reg.IsMatch(file.ContentType))
            {
                string path = await _service.UserService.UploadPhoto(file);
                return Ok(new { path });   
               
            }

            return BadRequest("The file more then 2MB or the file is not image.");

        }
    }
}