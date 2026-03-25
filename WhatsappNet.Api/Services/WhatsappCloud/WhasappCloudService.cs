using Newtonsoft.Json;
using System.Text;
using WhatsappNet.Api.Controllers;

namespace WhatsappNet.Api.Services.WhatsappCloud
{
    public class WhasappCloudService : IWhasappCloudService
    {
        private readonly HttpClient _httpClient;
        private readonly string _phoneNumberId;

        public WhasappCloudService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            var accessToken = configuration["WhatsappCloud:AccessToken"];
            if (string.IsNullOrEmpty(accessToken))
                throw new InvalidOperationException("WhatsApp Cloud access token is not configured. Set the 'WhatsappCloud:AccessToken' configuration value.");
            _phoneNumberId = configuration["WhatsappCloud:PhoneNumberId"] ?? string.Empty;
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
        }

        public async Task<bool> Execute(Object model)
        {
            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            using var content = new ByteArrayContent(byteData);
            var endpoint = "https://graph.facebook.com";
            var uri = $"{endpoint}/v16.0/{_phoneNumberId}/messages";
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");            

            var response = await _httpClient.PostAsync(uri, content);

            return response.IsSuccessStatusCode;
        }
    }
}
