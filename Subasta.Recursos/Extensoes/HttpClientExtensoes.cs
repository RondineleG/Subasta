using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Subasta.Recursos.Extensoes
{
    public static class HttpClientExtensoes
    {
        public static async Task<HttpResponseMessage> PostAsJsonAsync<TBody>(this HttpClient httpClient, Uri uri, TBody body) where TBody : class
        {
            var corpoRequisicaoJson = JsonConvert.SerializeObject(body);
            var content = new StringContent(corpoRequisicaoJson, Encoding.UTF8, "application/json");

            return await httpClient.PostAsync(uri, content);
        }
    }
}
