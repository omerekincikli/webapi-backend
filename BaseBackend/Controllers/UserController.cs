using BaseBackend.Models;
using BaseBackend.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _user;

        public UserController(IUserRepository user)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
        }

        [HttpPost]
        [Route("Authenticate")]
        public IActionResult Post(User user)
        {
            var result = _user.Authenticate(user);
            if (result == null)
            {
                return BadRequest(new { message = "Username or password invalid." });
            }
            return Ok(result);
        }
    }
}
