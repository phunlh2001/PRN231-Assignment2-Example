using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;

namespace MVC_Client.Helper
{
    public static class ApiHelper
    {
        /**
         * [GET]
        */
        public static async Task<T> GetApi<T>(this HttpClient client, string api)
        {
            HttpResponseMessage res = await client.GetAsync(api);
            var data = await res.Content.ReadAsStringAsync();

            var jsonData = JObject.Parse(data);
            string? valueData;

            if (jsonData.ContainsKey("value"))
            {
                valueData = jsonData["value"]!.ToString();
            }
            else
            {
                valueData = jsonData.ToString();
            }

            var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var obj = JsonSerializer.Deserialize<T>(valueData, opt);
            return obj!;
        }

        /**
         * [POST]
        */
        public static async Task<HttpResponseMessage> PostApi<T>(this HttpClient client, T obj, string api)
        {
            string data = JsonSerializer.Serialize(obj);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage res = await client.PostAsync(api, content);

            return res;
        }

        /**
         * [PUT]
        */
        public static async Task<HttpResponseMessage> PutApi<T>(this HttpClient client, T obj, string api)
        {
            string data = JsonSerializer.Serialize(obj);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage res = await client.PutAsync(api, content);

            return res;
        }

        /**
         * [PATCH]
        */
        public static async Task<HttpResponseMessage> PatchApi<T>(this HttpClient client, T obj, string api)
        {
            string data = JsonSerializer.Serialize(obj);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage res = await client.PatchAsync(api, content);

            return res;
        }
    }
}
