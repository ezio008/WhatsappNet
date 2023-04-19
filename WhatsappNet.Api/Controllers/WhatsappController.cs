using Microsoft.AspNetCore.Mvc;
using WhatsappNet.Api.Models.WhatsappCloud;
using WhatsappNet.Api.Services.WhatsappCloud;
using WhatsappNet.Api.Utils;

namespace WhatsappNet.Api.Controllers
{
    [ApiController]
    [Route("api/whatsapp")]
    public class WhatsappController : Controller
    {
        private readonly IWhasappCloudService _whasappService;
        private readonly IUtils _utils;

        public WhatsappController(IWhasappCloudService whasappService, IUtils utils)
        {
            _whasappService = whasappService;
            _utils = utils;
        }

        [HttpGet("sample")]
        public async Task<IActionResult> Sample()
        {
            var message = new MessageModel();
            message.To = "34605865281";
            message.Text.Body = "Yeep";

            var result = await _whasappService.Execute(message);
            return Ok("Ok sample");
        }

        [HttpGet]
        public IActionResult VerifyToken()
        {
            string accessToken = "8de1bc4f-3a8e-4de7-b4e8-c9d0362071f7";

            var token = Request.Query["hub.verify_token"].ToString();
            var challenge = Request.Query["hub.challenge"].ToString();

            if (challenge is not null && token is not null && token == accessToken)
            {
                return Ok(challenge);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> ReceivedMessage([FromBody] WhatsAppCloudModel body)
        {
            try
            {
                var message = body.Entry[0]?.Changes?[0].Value?.Messages?[0];

                if (message is not null)
                {
                    var userNamber = message.From;
                    var userText = GetUserText(message);

                    object objectMessage;

                    switch (userText.ToUpper())
                    {
                        case "TEXT":
                            objectMessage = _utils.TextMessage(userNamber, "Este es un ejemplo de texto");
                            break;
                        case "IMAGE":
                            objectMessage = _utils.ImageMessage(userNamber, "https://biostoragecloud.blob.core.windows.net/resource-udemy-whatsapp-node/image_whatsapp.png");
                            break;
                        case "AUDIO":
                            objectMessage = _utils.AudioMessage(userNamber, "https://biostoragecloud.blob.core.windows.net/resource-udemy-whatsapp-node/audio_whatsapp.mp3");
                            break;
                        case "VIDEO":
                            objectMessage = _utils.VideoMessage(userNamber, "https://biostoragecloud.blob.core.windows.net/resource-udemy-whatsapp-node/video_whatsapp.mp4");
                            break;
                        case "DOCUMENT":
                            objectMessage = _utils.DocumentMessage(userNamber, "https://biostoragecloud.blob.core.windows.net/resource-udemy-whatsapp-node/document_whatsapp.pdf");
                            break;
                        case "LOCATION":
                            objectMessage = _utils.LocationMessage(userNamber, new());
                            break;
                        case "BUTTON":
                            objectMessage = _utils.ButtonsMessage(userNamber);
                            break;
                        default:
                            objectMessage = _utils.TextMessage(userNamber, "I dont understand Nya :3");
                            break;
                    }

                    await _whasappService.Execute(objectMessage);
                }

                return Ok("EVENT_RECEIVED");
            }
            catch (Exception ex)
            {
                return Ok("EVENT_RECEIVED");
            }
        }

        private string GetUserText(Message message)
        {
            string type = message.Type;

            if (type.ToUpper() == "TEXT")
            {
                return message.Text.Body;
            }
            else if (type.ToUpper() == "INTERACTIVE")
            {
                string interactiveType = message.Interactive.Type;

                if (interactiveType.ToUpper() == "LIST_REPLY")
                {
                    return message.Interactive.List_Reply.Title;
                }
                else if (interactiveType.ToUpper() == "BUTTON_REPLY")
                {
                    return message.Interactive.Button_Reply.Title;
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }

        }
    }
}
