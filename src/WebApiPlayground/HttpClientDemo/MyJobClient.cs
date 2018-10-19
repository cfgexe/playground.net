using System.Net.Http;

namespace WebApiPlayground.HttpClientDemo
{
    public class MyJobClient
    {
        public MyJobClient(HttpClient client)
        {
            Client = client;
        }

        public HttpClient Client { get; }
    }
}
