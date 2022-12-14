using System.Net;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Presentation.Controllers
{
    [Route("api/me")]
    [Authorize(Roles = "Authenticated")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _service;

        public UserController(IServiceManager service)
        {
            _service = service;
        }

        

      

        [HttpPost]
        public async Task<IActionResult> Upload()
        {
           

            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.First();

            Regex reg = new Regex(".(gif|jpe?g|tiff?|png|webp|bmp)$",RegexOptions.IgnoreCase);
            if (file.Length<2097152&&reg.IsMatch(file.ContentType))
            {
                string photo = await _service.UserService.UploadPhoto(file);
                await _service.UserService.UpdateUserAvatar(User.Identity.Name, photo);
                return Ok(new { photo });   
               
            }

            return BadRequest("The file more then 2MB or the file is not image.");

        }
    }
}

// Upload the file if less than 2 MB
// memoryStream.Length < 2097152