using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;

namespace MVC_Client.Helper
{
    public static class ApiHelper
    {
        /**
         * [GET]
         * @param {string} api
         * @returns {T}
        */
        public static async Task<T> GetApi<T>(this HttpClient client, string api)
        {
            HttpResponseMessage res = await client.GetAsync(api);
            var data = await res.Content.ReadAsStringAsync();

            var jsonData = JObject.Parse(data);
            string? valueData;

            if (jsonData.ContainsKey("value"))
                valueData = jsonData["value"]!.ToString();
            else
                valueData = jsonData.ToString();

            var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var obj = JsonSerializer.Deserialize<T>(valueData, opt);
            return obj!;
        }

        /**
         * [POST]
         * @param {Object} obj
         * @param {string} api
         * @param {string} method
         * @returns {HttpResponseMessage}
        */
        public static async Task<HttpResponseMessage> PostOrPutApi<T>(this HttpClient client, T obj, string api, string method)
        {
            string data = JsonSerializer.Serialize(obj);
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage res;
            if (method == "POST")
                res = await client.PostAsync(api, content);
            else if (method == "PUT")
                res = await client.PutAsync(api, content);
            else
                throw new Exception("Accept POST or PUT only");

            return res;
        }
    }
}
