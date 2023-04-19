using OpenAI_API;
using OpenAI_API.Completions;

namespace WhatsappNet.Api.Services.OpenAI.ChatGPT
{
    public class ChatGPTService : IChatGPTService
    {
        private readonly string apiKey = "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";
        private readonly OpenAIAPI _openAIAPI;

        public ChatGPTService()
        {
            _openAIAPI = new OpenAIAPI(apiKey);
        }
        public async Task<string> Execute(string messageUser)
        {
            string response;
            try
            {
                var completion = new CompletionRequest
                {
                    Prompt = messageUser,
                    Model = "text-davinci-003",
                    NumChoicesPerPrompt = 1,
                    MaxTokens = 200,
                };

                var result = await _openAIAPI.Completions.CreateCompletionAsync(completion);

                if(result is not null && result.Completions.Count > 0)
                {
                    response = result.Completions[0].Text;
                } else
                {
                    response = "Disculpa, no he encontrado una respuesta a tu pregunta 🥸";
                }

            } catch (Exception ex)
            {
                response = "Lo siento, he tenido un fallo inesperado 😫";
            }

            return response;
        }
    }
}
