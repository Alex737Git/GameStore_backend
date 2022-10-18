using AutoMapper;
using AWSCloudService;
using Entities.Exceptions.NotFoundExceptions;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IAwsServices _aws;


        public UserService(IMapper mapper, UserManager<User> userManager, IConfiguration configuration,
            IAwsServices aws)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _aws = aws;
        }

        // public async Task<IdentityResult> UpdateUserAsync(string name, UserForUpdateDto userForUpdate,
        //     bool trackChanges)
        // {
        //     User user = await _userManager.FindByNameAsync(name);
        //
        //     if (user is null)
        //         throw new UserNotFoundException(new Guid(name));
        //
        //
        //     
        //   
        //     user.FirstName = userForUpdate.FirstName;
        //     user.LastName = userForUpdate.LastName;
        //
        //     var result = await _userManager.UpdateAsync(user);
        //     if (result.Succeeded)
        //     {
        //        result =  await _userManager.SetTwoFactorEnabledAsync(user, userForUpdate.IsTwoFactorAuthorizationEnabled);
        //     }
        //
        //     return result;
        // }

        // public async Task<UserForUpdateMe> GetUser(string name, bool trackChanges)
        // {
        //     User user = await _userManager.FindByNameAsync(name);   
        //
        //     if (user is null)
        //         throw new UserNotFoundException(new Guid(name));
        //
        //     var result = _mapper.Map<UserForUpdateMe>(user);
        //     return result;
        // }

        public async Task<string> UploadPhoto(IFormFile file)
        {
            #region Init AwsConfig

            AwsConfig config = new AwsConfig
            {
                Region = GetValueFromAppJson("AWS", "Region"),
                BucketName = GetValueFromAppJson("AWS", "bucketName"),
                AccessKey = GetValueFromAppJson("AWS", "awsAccessKey"),
                SecretKey = GetValueFromAppJson("AWS", "awsSecretAccessKey")
            };

            #endregion

            string photo = await _aws.UploadPhoto(config, file);
            return photo;
        }

        public string GetValueFromAppJson(string section, string value)
        {
            var sec = _configuration.GetSection(section);
            return sec.GetSection(value).Value;
        }

        public async Task UpdateUserAvatar(string name, string avatarUrl)
        {
            User user = await _userManager.FindByNameAsync(name);

            if (user is null)
                throw new UserNotFoundException(new Guid(name));

            user.AvatarUrl = avatarUrl;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new Exception("Could not update user avatar url.");
            }
        }
    }
}