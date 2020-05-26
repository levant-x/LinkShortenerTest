using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.BLL;
using LinkShortener.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LinkShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : LocalApiControllerBaseController
    {
        IConfiguration configuration;

        public UserController(IRepository repository, IConfiguration configuration) : base(repository)
        {
            this.configuration = configuration;
        }

        [Route("Register")]
        public IActionResult Post(UserRegisterModel user)
        {
            var manager = new UserManager(repository);
            var res = manager.RegisterUser(user);
            if (!res.IsSuccessful) return BadRequestResult(res.Errors);
            else res.Data = user;
            return CreatedResult(res.Data);
        }

        [Route("Login")]
        public IActionResult Post([FromBody]UserBaseModel user)
        {
            var existingUser = repository.Users.FirstOrDefault(u => u.Name == user.Name);
            if (existingUser == null) return BadRequestResult("User not found");            
            var manager = new UserManager(repository);
            if (!manager.CheckPass(user)) return BadRequestResult("Incorrect password");

            var tokenString = manager.CreateAuthToken(user);
            var res = new
            {
                access_token = tokenString,
                username = user.Name
            };
            return new JsonResult(res);
        }

        [Authorize]
        public IActionResult Get()
        {
            return Ok($"Your name is {User.Identity.Name}");
        }
    }
}