using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace WebApiPlayground.HttpClientDemo
{
    public interface IMyJobClient
    {
        Task<IEnumerable<string>> GetDocuments();
        Task<string> GetDocument(int documentId);
        Task<HttpStatusCode> AddDocument(string document);
    }
}
