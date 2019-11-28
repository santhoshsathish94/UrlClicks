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
    public class ModuleClickController : ControllerBase
    {
        private IUnitOfWork _uow;
        private ILogger<ModuleClickController> _logger;

        public ModuleClickController(IUnitOfWork uow,
                                  ILogger<ModuleClickController> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        // GET api/ModuleClick/5
        [HttpGet("{id}")]
        public ActionResult<ModuleClick> Get(string id)
        {
            var data = _uow.ModuleClickRepo.Find(c => c.Id == new Guid(id));
            return data;
        }
    }
}