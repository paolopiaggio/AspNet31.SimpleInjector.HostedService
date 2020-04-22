using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace AspNet31.SimpleInjector.HostedService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value", "value2" };
        }
    }
}
