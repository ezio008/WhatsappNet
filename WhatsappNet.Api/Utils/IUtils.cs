using static WhatsappNet.Api.Utils.Utils;

namespace WhatsappNet.Api.Utils
{
    public interface IUtils
    {
        object TextMessage(string number, string message);
        object ImageMessage(string number, string url);
        object AudioMessage(string number, string url);
        object VideoMessage(string number, string url);
        object DocumentMessage(string number, string url);
        object LocationMessage(string number, Location location);
        object ButtonsMessage(string number);
    }
}
