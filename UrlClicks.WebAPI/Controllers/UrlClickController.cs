using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UrlClicks.Domain.Models;
using UrlClicks.Persistence.Interface;

namespace UrlClicks.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlClickController : ControllerBase
    {        
        private IUnitOfWork _uow;
        private ILogger<UrlClickController> _logger;        

        public UrlClickController(IUnitOfWork uow,
                                  ILogger<UrlClickController> logger)
        {            
            _uow = uow;
            _logger = logger;            
        }

        // GET api/UrlClick/5
        [HttpGet("{id}")]
        public ActionResult<UrlClick> Get(string id)
        {
            var data = _uow.UrlClickRepo.Find(c=>c.Id == new Guid(id));
            return data;
        }
    }
}