using BaseBackend.Models;
using BaseBackend.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BaseBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _user;
        private readonly IWebHostEnvironment _env;

        public UserController(IUserRepository user, IWebHostEnvironment env)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _env = env;
        }

        [HttpPost]
        [Route("Authenticate")]
        public IActionResult Authenticate(User user)
        {
            var result = _user.Authenticate(user);
            if (result == null)
            {
                return BadRequest(new { message = "Username or password invalid." });
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("UpdatePicture")]
        public IActionResult UpdatePicture(User user)
        {
            var result = _user.UpdatePicture(user);
            if (result)
                return Ok();

            return BadRequest(new { message = "User not found." });
        }

        [HttpPost]
        [Route("SaveFile")]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "\\Photos\\" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception e)
            {
                return new JsonResult("Fail: " + e.Message);
            }
        }
    }
}
