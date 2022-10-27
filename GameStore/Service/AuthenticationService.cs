using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Entities.Models;
using LoggingService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using Shared.DataTransferObjects.ForAuth;
using Shared.DataTransferObjects.ForShow;

namespace Service
{
    internal sealed class AuthenticationService : IAuthenticationService
    {
        #region Properties

        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        private readonly IConfiguration _configuration;

        // private readonly IEmailSender _emailSender;
        private User? _user;

        #endregion

        #region Ctor

        public AuthenticationService(ILoggerManager logger, IMapper mapper,
            UserManager<User> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        #endregion


        #region Register User

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);


            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Authenticated");
            }


            return result;
        }

        #endregion

        #region Validate User

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
        {
            _user = await _userManager.FindByNameAsync(userForAuth.UserName);

            if (_user == null) return false;

            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));



            return result;
        }

        #endregion

        #region Validate Email

        public async Task<bool> ValidateEmail(string email) => await _userManager.FindByEmailAsync(email) != null;

        #endregion

        #region Get Auth User

        public async Task<UserDto> GetAuthUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var mappedUser = _mapper.Map<UserDto>(user);
            mappedUser.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            return mappedUser;
        }

        #endregion

        #region Create Token

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            _logger.LogInfo($"Creating Token (CreateToken)");
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }


        private SigningCredentials GetSigningCredentials()
        {
            // var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
            var securityKey = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(securityKey.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                // new Claim(ClaimTypes.Name, _user.Email)
                new Claim(ClaimTypes.Name, _user.UserName)
            };
            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expiryInMinutes"])),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }

        #endregion


        #region UserName Confirmation

        public async Task<IdentityResult> EmailConfirmation(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.ConfirmEmailAsync(user, token);

            return result;
        }

#endregion
    }
}