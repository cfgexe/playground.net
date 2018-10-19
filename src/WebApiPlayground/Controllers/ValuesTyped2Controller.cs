using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebApiPlayground.HttpClientDemo;

namespace WebApiPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesTyped2Controller : ControllerBase
    {
        private readonly IMyJobClient _myJobClient;

        public ValuesTyped2Controller(IMyJobClient myJobClient)
        {
            _myJobClient = myJobClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            IEnumerable<string> documents = await _myJobClient.GetDocuments();

            return Ok(documents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            string document = await _myJobClient.GetDocument(id);

            return Ok(document);
        }

        [HttpPost]
        public async Task<ActionResult> Post(string document)
        {
            HttpStatusCode statusCode = await _myJobClient.AddDocument(document);
            return StatusCode((int)statusCode);
        }
    }
}
