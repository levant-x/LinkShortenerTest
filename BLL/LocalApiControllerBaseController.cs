using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.BLL
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class LocalApiControllerBaseController : ControllerBase
    {
        protected IRepository repository;


        public LocalApiControllerBaseController(IRepository repository)
        {
            this.repository = repository;
        }


        protected IActionResult BadRequestResult(QueryResult result)
        {
            return BadRequest(new JsonResult(result.Errors));
        }

        protected IActionResult CreatedResult(object dataCreated)
        {
            return Created(Request.Path, new JsonResult(dataCreated));
        }
    }
}