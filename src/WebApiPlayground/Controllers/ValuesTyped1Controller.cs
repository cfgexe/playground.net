using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiPlayground.HttpClientDemo;

namespace WebApiPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesTyped1Controller : ControllerBase
    {
        private readonly MyJobClient _myJobClient;

        public ValuesTyped1Controller(MyJobClient myJobClient)
        {
            _myJobClient = myJobClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var result = await _myJobClient.Client.GetStringAsync("/api/v2/People");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            var result = await _myJobClient.Client.GetStringAsync($"/api/v2/People/{id}");

            return Ok(result);
        }
    }
}
