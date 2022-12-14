using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects.ForAuth;

namespace Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;


        #region Ctor

        public AuthenticationController(IServiceManager service)
        {
            _service = service;
        }

        #endregion

        #region Registration

        [HttpPost("registration")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var result = await _service.AuthenticationService.RegisterUser(userForRegistration);
            
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
            
                return BadRequest(new RegistrationResponseDto { Errors = errors });
            }

            return StatusCode(201);
        }

        #endregion

        #region Login
        
        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _service.AuthenticationService.ValidateUser(user))
                return Unauthorized();
        
            var token = await _service
                .AuthenticationService.CreateToken();
            return Ok(new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }
        
        #endregion

        #region Privacy

        [HttpGet("privacy")]
        // [Authorize]
        [Authorize(Roles = "Manager")]
        public IActionResult Privacy()
        {
            var claims = User.Claims
                .Select(c => new { c.Type, c.Value })
                .ToList();
            return Ok(claims);
        }

        #endregion

        #region Get Auth User
        
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetAuthUser()
        {
            var userName = GetIdFromToken();
            var result = await _service.AuthenticationService.GetAuthUser(userName);
        
            return Ok(result);
        }
        
        private string GetIdFromToken()
        {
            var identity = User.Identity as ClaimsIdentity;
            var userName = "";
            if (identity != null)
            {
                var claims = identity.Claims;
               // id = claims.FirstOrDefault(p => p.Type == "id")?.Value;
               userName = claims.FirstOrDefault(p => p.Type.Contains("name"))?.Value;
               
            }
        
            return userName;
        }
        
        #endregion
        
        
         #region CheckEmail
        
         [HttpPost("checkEmail")]
         public async Task<IActionResult> CheckEmail([FromBody] CheckEmail email)
         {
             var result = await _service.AuthenticationService.ValidateEmail(email.Email);
        
             return Ok(result);
         }
        
         #endregion
        
    }
}