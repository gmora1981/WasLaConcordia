using System.ComponentModel.Design;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace LaConcordia.Helpers
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient httpClient;

        private JsonSerializerOptions defaultJsonSerializerOptions =>
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public HttpService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            var responseHTTP = await httpClient.GetAsync(url);

            if (responseHTTP.IsSuccessStatusCode)
            {
                var response = await Deserialize<T>(responseHTTP, defaultJsonSerializerOptions);
                return new HttpResponseWrapper<T>(response, true, responseHTTP);
            }
            else
            {
                return new HttpResponseWrapper<T>(default!, false, responseHTTP); // Fixed nullability issue with default!
            }
        }
        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T data)
        {
            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, stringContent);
            if (response.IsSuccessStatusCode)
            {
                var responseDeserialized = await Deserialize<TResponse>(response, defaultJsonSerializerOptions);
                return new HttpResponseWrapper<TResponse>(responseDeserialized, true, response);
            }
            else
            {
                return new HttpResponseWrapper<TResponse>(default!, false, response); // Fixed nullability issue with default!
            }
        }
        public async Task<HttpResponseWrapper<TResponse>> Put<T, TResponse>(string url, T data)
        {
            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(url, stringContent);

            if (response.IsSuccessStatusCode)
            {
                // Para PUT que retorna 204 No Content
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return new HttpResponseWrapper<TResponse>(default!, true, response); // Fixed nullability issue with default!
                }

                var responseDeserialized = await Deserialize<TResponse>(response, defaultJsonSerializerOptions);
                return new HttpResponseWrapper<TResponse>(responseDeserialized, true, response);
            }
            else
            {
                return new HttpResponseWrapper<TResponse>(default!, false, response); // Fixed nullability issue with default!
            }
        }

        public async Task<HttpResponseWrapper<object?>> Delete(string url)
        {
            var response = await httpClient.DeleteAsync(url);
            return new HttpResponseWrapper<object?>(null, response.IsSuccessStatusCode, response);
        }
        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseString, options) ?? throw new InvalidOperationException("Deserialization returned null.");
        }
    }
}
