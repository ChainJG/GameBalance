using System.Net.Http;
using System.Net.Http.Headers;

namespace GameBalance.Infrastructure.Http
{
    public static class HttpClientProvider
    {
        private const int DefaultClientTimeoutSeconds = 15;

        private static readonly Lazy<HttpClient> SharedClient = new(CreateClient);

        public static HttpClient Client => SharedClient.Value;

        private static HttpClient CreateClient()
        {
            var client = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(DefaultClientTimeoutSeconds)
            };

            client.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue("GameBalance", "1.0"));

            return client;
        }
    }
}
