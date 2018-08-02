using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Tarjem.AI
{
    /// <summary>
    /// a class used to handl the translation 
    /// </summary>
    public class Translator
    {
        /// <summary>
        /// Translate the text based on the selected targed
        /// </summary>
        /// <param name="message">Message to translate</param>
        /// <param name="to">Targted language</param>
        /// <returns></returns>
        public async Task<string> TranslateAsync(string message,string to="ar")
        {
            string host = "https://api.cognitive.microsofttranslator.com";
            string path = "/translate?api-version=3.0";
            // Translate to German and Italian.
            string params_ = $"&to={to}";

            string uri = host + path + params_;

            //Azure Trnaslattion key
            string key = "6feb4d3bc36240a08d35e89d99fe6522";


            string text = message;
            System.Object[] body = new System.Object[] { new { Text = text } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(uri);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);

                var response = await client.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(responseBody), Newtonsoft.Json.Formatting.Indented);
                return result;
            }

        }
    }
}
