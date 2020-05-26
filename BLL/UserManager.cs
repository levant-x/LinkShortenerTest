using LinkShortener.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace LinkShortener.BLL
{
    public class UserManager : ManagerBase
    {
        public UserManager(IRepository repository) : base(repository)
        {

        }

        public QueryResult RegisterUser(UserRegisterModel user)
        {
            var res = new QueryResult();
            var existingUser = repository.Users.FirstOrDefault(u => u.Name == user.Name);
            if (existingUser != null) res.Errors = new string[] { "This user name is already registered!" };
            else
            {
                repository.Users.AddItem(user);
                res = repository.SaveChanges();
            }
            return res;
        }

        public bool CheckPass(UserBaseModel user)
        {
            return repository.CheckUserPass(user);
        }

        public string CreateAuthToken(UserBaseModel user)
        {
            var now = DateTime.UtcNow;
            var identity = new ClaimsIdentity(new Claim[] 
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name)
            });
            var jwt = new JwtSecurityToken(AuthOptions.ISSUER,
                AuthOptions.AUDIENCE, identity.Claims, now, now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                new SigningCredentials(AuthOptions.CreateSymmSecurityKey(), SecurityAlgorithms.HmacSha256));
            var res = new JwtSecurityTokenHandler().WriteToken(jwt);
            return res;
        }
    }
}
