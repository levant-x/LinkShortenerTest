using LinkShortener.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Security.Principal;

namespace LinkShortener.BLL
{
    public class UserManager : ManagerBase
    {
        IConfiguration configuration;

        public UserManager(IRepository repository, IConfiguration configuration) : base(repository)
        {
            this.configuration = configuration;
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

        public string CreateAuthToken(UserBaseModel user)
        {
            var now = DateTime.UtcNow;
            var lifetime = int.Parse(configuration.GetSection("AuthOptions")["LIFETIME"]);
            var jwt = new JwtSecurityToken(configuration.GetSection("AuthOptions")["ISSUER"],
                configuration.GetSection("AuthOptions")["Audience"], null, now, now.Add(TimeSpan.FromMinutes(lifetime)),
                new SigningCredentials(Helpers.CreateSymmSecurityKey(), SecurityAlgorithms.Aes128Encryption));
            var res = new JwtSecurityTokenHandler().WriteToken(jwt);
            return res;
        }
    }
}
