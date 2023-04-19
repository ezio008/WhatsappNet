using WhatsappNet.Api.Controllers;

namespace WhatsappNet.Api.Services.WhatsappCloud
{
    public interface IWhasappCloudService
    {
        public Task<bool> Execute(Object model);
    }
}
