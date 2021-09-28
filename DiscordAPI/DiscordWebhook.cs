using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DiscordAPI
{
    public class DiscordWebhook
    {
        private int buttonId = 0;
        private readonly string webhookUrl;

        public DiscordWebhook(string webhookUrl)
        {
            this.webhookUrl = webhookUrl;
        }

        public async Task<bool> Push(string text, string url)
        {
            var client = new HttpClient();


            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(CreatePackage("Ny post", text, url), serializerSettings);
            

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await client.PostAsync(webhookUrl, content);
            client.Dispose();
            return result.IsSuccessStatusCode;
        }

        private DiscordContent CreatePackage(string title, string text, string url)
        {
            return new DiscordContent
            {               
               Embeds = new List<DiscordEmbedObject>
               {
                   new DiscordEmbedObject
                   {
                       Title = title,
                       Url = url,
                       Description = text
                   }
               }                
            };
        }

        private DiscordComponent CreateButton(string text, string url)
        {
            return new DiscordButtonComponent()
            {
                Type = 2,
                Url = url,
                CustomId = "button-id-" + buttonId++,
                Label = text,
                Style = 5
            };
        }
    }   

}
