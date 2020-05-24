using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Controllers
{
    [Route("/")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        IRepository repository;

        public LinksController(IRepository repository)
        {            
            this.repository = repository;
        }

        [HttpGet("{shortURL}")]
        public IActionResult Get(string shortURL)
        {
            var linkToReroute = repository.Links.FirstOrDefault(link => link.ShortURL == shortURL);
            if (linkToReroute == null) return BadRequest("Invalid link");
            else return Redirect(linkToReroute.URL);
        }
    }
}