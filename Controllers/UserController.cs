using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.BLL;
using LinkShortener.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LinkShortener.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : LocalApiControllerBaseController
    {
        IConfiguration configuration

        public UserController(IRepository repository, IConfiguration configuration) : base(repository)
        {
            this.configuration = configuration;
        }

        public IActionResult Post(UserRegisterModel user)
        {
            var manager = new UserManager(repository, configuration);
            var res = manager.RegisterUser(user);
            if (!res.IsSuccessful) return BadRequestResult(res);

            manager.CreateAuthToken(user);
            return CreatedResult(res.Data);
        }
    }
}