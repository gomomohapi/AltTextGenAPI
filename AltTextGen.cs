using System.Web;
using OpenAI.Chat;

namespace AltTextGenAPI
{
    public class AltTextGen
    {
        public static async Task<GeneratedAltText> GetImageDescription(string imageUrl)
        {
            ChatClient client = new("gpt-4o", Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

            var decodededUrl = HttpUtility.UrlDecode(imageUrl);

            HttpClient httpClient = new HttpClient();
            Stream imageStream = await httpClient.GetStreamAsync(decodededUrl);
            BinaryData imageBytes = BinaryData.FromStream(imageStream);

            List<ChatMessage> messages = [
                new UserChatMessage(
                ChatMessageContentPart.CreateTextMessageContentPart(
                    "Please describe the following image in text that " +
                    "could be used for accessibility alt-text for the web."),
                ChatMessageContentPart.CreateImageMessageContentPart(imageBytes, "image/png"))
            ];

            ChatCompletion chatCompletion = await client.CompleteChatAsync(messages);

            var generatedAltText = new GeneratedAltText()
            {
                AltText = chatCompletion.ToString()
            };

            return generatedAltText;
        }

        public class GeneratedAltText
        {
            public string AltText { get; set; }
        }
    }
}
