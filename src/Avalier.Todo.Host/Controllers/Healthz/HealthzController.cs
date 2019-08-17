using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Avalier.Todo.Host.Controllers.Healthz
{
    [ApiController]
    [Route("api/health")]
    [Route("api/healthz")]
    public class HealthzController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}