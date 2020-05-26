using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.BLL;
using LinkShortener.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Controllers
{
    //[Route("/")]
    [Authorize]
    [ApiController]
    public class LinkController : LocalApiControllerBaseController
    {
        public LinkController(IRepository repository) : base(repository)
        {

        }

        [HttpGet("/{shortURL}")]
        [AllowAnonymous]
        public IActionResult Get(string shortURL)
        {
            var linkToReroute = repository.Links.FirstOrDefault(link => link.ShortURL == shortURL);
            if (linkToReroute == null) return BadRequest("Invalid link");
            else return Redirect(linkToReroute.URL);
        }

        [HttpGet("/")]
        public IActionResult Get()
        {
            var userID = repository.Users
                .First(user => user.Name == User.Identity.Name).ID;
            var userLinks = repository.Links.Where(link => link.UserID == userID);
            return Ok(new JsonResult(userLinks));
        }

        //[HttpPost]
        public IActionResult Post()
        {                        
            string url = Request.ReadRawBodyString();
            var linksManager = new LinkManager(repository);

            var userID = repository.Users
                .First(user => user.Name == User.Identity.Name).ID;
            var actionRes = linksManager.ShortenURL(url, Request.Host.ToString(), userID);      

            Response.ContentType = "application/json";
            if (!actionRes.IsSuccessful) return BadRequestResult(actionRes);
            else return CreatedResult(actionRes.Data);
        }
    }
}