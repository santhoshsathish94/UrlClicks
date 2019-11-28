using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UrlClicks.Domain.Models.SMS;
using UrlClicks.Persistence.Interface;

namespace UrlClicks.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityClickController : ControllerBase
    {
        private IUnitOfWork _uow;
        private ILogger<ActivityClickController> _logger;

        public ActivityClickController(IUnitOfWork uow,
                                  ILogger<ActivityClickController> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        // GET api/ActivityClick/5
        [HttpGet("{id}")]
        public ActionResult<SmsActivityClick> Get(string id)
        {
            var data = _uow.SmsActivityClickRepo.Find(c => c.Id == new Guid(id));
            return data;
        }
    }
}