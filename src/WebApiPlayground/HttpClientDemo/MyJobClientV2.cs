using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebApiPlayground.HttpClientDemo
{
    public class MyJobClientV2 : IMyJobClient
    {
        HttpClient _client;

        public MyJobClientV2(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<string>> GetDocuments()
        {
            string documents = await _client.GetStringAsync("/api/v2/People");
            return documents.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public async Task<string> GetDocument(int documentId)
            => await _client.GetStringAsync($"/api/v2/People/{documentId}");

        public async Task<HttpStatusCode> AddDocument(string document)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("/documents", document);
            return response.StatusCode;
        }
    }
}
