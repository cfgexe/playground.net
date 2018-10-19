using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebApiPlayground.HttpClientDemo;

namespace WebApiPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ValuesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        #region Basic Client

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var client = _httpClientFactory.CreateClient();
            var result = await client.GetStringAsync($"http://sampleaspnetcorewebapi.azurewebsites.net/api/v2/People/");

            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var result = await client.GetStringAsync($"http://sampleaspnetcorewebapi.azurewebsites.net/api/v2/People/{id}");

            return Ok(result);
        }

        #endregion

        #region Named Client

        [HttpGet]
        [Route("named")]
        public async Task<ActionResult<IEnumerable<string>>> Named()
        {
            var client = _httpClientFactory.CreateClient(NamedHttpClients.MyJob);
            var result = await client.GetStringAsync("/");

            return Ok(result);
        }

        [HttpGet("{id}")]
        [Route("named/{id}")]
        public async Task<ActionResult<string>> Named(int id)
        {
            var client = _httpClientFactory.CreateClient(NamedHttpClients.MyJob);
            var result = await client.GetStringAsync($"/{id}");

            return Ok(result);
        }

        #endregion
    }
}
