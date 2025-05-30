using Microsoft.VisualBasic;
using Mscc.GenerativeAI;

namespace BankingChatbot.Contexts
{
    public class GenAIContext
    {

        static string system_prompt_str = "You are a banking assistant. Handle every client with professionalism and solve their queries";
        static Content system_prompt = new Content(system_prompt_str);
        static GoogleAI googleAI = new GoogleAI(apiKey: Environment.GetEnvironmentVariable("GEMINI_API_KEY"));
        static GenerativeModel model = googleAI.GenerativeModel(Model.Gemini20Flash, systemInstruction: system_prompt);
        ChatSession chat = model.StartChat();
        // GenerateContentRequest? request;
        // GenerateContentResponse? response;

        // public async Task<string?> Chat(string input)
        // {
        //     this.request = new GenerateContentRequest(input);

        //     this.response = await model.GenerateContent(request);
        //     return response.Text;

        // }
        public GenerateContentRequest CreateRequest(string input)
        {
            return new GenerateContentRequest(input);
        }

        public ChatSession CreateChatSession(List<ContentResponse>? chatHistory)
        {
            return new ChatSession(model, chatHistory);

        }

    }
}