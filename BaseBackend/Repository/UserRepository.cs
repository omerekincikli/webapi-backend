using BaseBackend.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BaseBackend.Repository
{
    public interface IUserRepository
    {
        User Authenticate(User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly Context _appDBContext;

        public UserRepository(Context context)
        {
            _appDBContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public User Authenticate(User user)
        {
            var userObj = _appDBContext.Users.SingleOrDefault(x => x.Username == user.Username && x.Password == user.Password);
            if (userObj == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Secret key for JWT");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userObj.ID.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userObj.Token = tokenHandler.WriteToken(token);
            userObj.Password = null;

            return userObj;
        }

    }
}
