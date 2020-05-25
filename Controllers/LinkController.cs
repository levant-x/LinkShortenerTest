using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.BLL;
using LinkShortener.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Controllers
{
    [Route("/")]
    [ApiController]
    public class LinkController : LocalApiControllerBaseController
    {
        public LinkController(IRepository repository) : base(repository)
        {

        }

        [HttpGet("{shortURL}")]
        public IActionResult Get(string shortURL)
        {
            var linkToReroute = repository.Links.FirstOrDefault(link => link.ShortURL == shortURL);
            if (linkToReroute == null) return BadRequest("Invalid link");
            else return Redirect(linkToReroute.URL);
        }

        public IActionResult Post()
        {                        
            string url = Request.ReadRawBodyString();
            var linksManager = new LinkManager(repository);
            var actionRes = linksManager.ShortenURL(url, Request.Host.ToString());      

            Response.ContentType = "application/json";
            if (!actionRes.IsSuccessful) return BadRequestResult(actionRes);
            else return CreatedResult(actionRes.Data);
        }
    }
}