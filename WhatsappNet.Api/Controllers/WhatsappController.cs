using Microsoft.AspNetCore.Mvc;
using WhatsappNet.Api.Models.WhatsappCloud;
using WhatsappNet.Api.Services.OpenAI.ChatGPT;
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
        private readonly IChatGPTService _chatGPTService;

        public WhatsappController(IWhasappCloudService whasappService, IUtils utils, IChatGPTService chatGPTService)
        {
            _whasappService = whasappService;
            _utils = utils;
            _chatGPTService = chatGPTService;
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

                    List<object> objectMessage = new();
                    
                    var responseChatGPT = await _chatGPTService.Execute(userText);
                    objectMessage.Add(_utils.TextMessage(userNamber, responseChatGPT));

                    foreach (var item in objectMessage)
                    {
                        await _whasappService.Execute(item);
                    }
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
