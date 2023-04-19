using Newtonsoft.Json;
using System.Text;
using WhatsappNet.Api.Controllers;

namespace WhatsappNet.Api.Services.WhatsappCloud
{
    public class WhasappCloudService : IWhasappCloudService
    {
        private readonly HttpClient _httpClient;

        public WhasappCloudService()
        {
            _httpClient = new HttpClient();
            var accesToken = "EAAKw08ZCoLhIBABzkVuxgLCMZBA1ZBpVw6GQo1XgOqnK0itjLNJihkOM37q0nBSqVFZBZCpFAgUfovzojuZC1BQKOwPelaI4XZANDf6lA0Aqp7wtj4X7BcoZBNraAirgio2ZAyO2IH92cAqkwdS4cBYovnLYjZBe4ByGBZBNavKuA6ESlxEYHyj0f5Ane9T6HHOS7tvcJc4Oui3qQZDZD";
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accesToken}");
        }

        public async Task<bool> Execute(Object model)
        {
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            using var content = new ByteArrayContent(byteData);
            var endpoint = "https://graph.facebook.com";
            var phoneNumberId = "112445371812093";            
            var uri = $"{endpoint}/v16.0/{phoneNumberId}/messages";
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");            

            var response = await _httpClient.PostAsync(uri, content);

            return response.IsSuccessStatusCode;
        }
    }
}
