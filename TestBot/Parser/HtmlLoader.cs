using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TestBot.Parser.Interface;

namespace TestBot.Parser
{
    class HtmlLoader
    {
        readonly HttpClient client;
        readonly string url;
        public HtmlLoader(IParserSettings settings)
        {
            client = new HttpClient();
            url = settings.BaseUrl;
        }

        public async Task<string> GetSourcePageAsync()
        {
            var responce = await client.GetAsync(url);

            if (responce != null && responce.StatusCode == HttpStatusCode.OK)
            {
                string source = await responce.Content.ReadAsStringAsync();
                return source;
            }
            else
            {
                throw new NullReferenceException($"Website not found: {url}");
            }
        }
    }
}
