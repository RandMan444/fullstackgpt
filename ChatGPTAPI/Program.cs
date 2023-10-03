
// See https://aka.ms/new-console-template for more information
using ChatGPTAPI;
using System.Data;
using System.Text;
using System.Text.Json;

StreamReader sr = new StreamReader("ascii.txt");
string line = sr.ReadLine();
while (line != null)
{
    Console.WriteLine(line);
    line = sr.ReadLine();
}
sr.Close();


Console.WriteLine("I'm ready to ship some websites! What do you want to deploy?");
Console.WriteLine();


async void RunAgain()
{

    string openAIPrompt = Console.ReadLine();

    CompletionRequest completionRequest = new CompletionRequest
    {
        Model = "gpt-3.5-turbo",
        Messages = new List<CompletionMessage>()
        {
            new CompletionMessage
            {
                Role= "system",
                Content= "You're a web developer"
            },
            new CompletionMessage
            {
                Role= "user",
                Content= "Create the html with CSS included inline for " + openAIPrompt
            }
        }
    };
    CompletionResponse completionResponse = null;

    using (HttpClient httpClient = new HttpClient())
    {
        using (var httpReq = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions")) // This is the current (2023) URL
        {
            try
            {
                httpReq.Headers.Add("Authorization", String.Format("Bearer {0}", System.Environment.GetEnvironmentVariable("OpenAIKey")));
                httpReq.Headers.Add("OpenAI-Organization", "{YOUROPENAIORG}");


                string requestString = JsonSerializer.Serialize(completionRequest);
                httpReq.Content = new StringContent(requestString, Encoding.UTF8, "application/json");
                using (HttpResponseMessage? httpResponse = httpClient.Send(httpReq))
                {
                    if (httpResponse is not null)
                    {
                        if (httpResponse.IsSuccessStatusCode)
                        {
                            string responseString = await httpResponse.Content.ReadAsStringAsync();
                            {
                                if (!string.IsNullOrWhiteSpace(responseString))
                                {
                                    completionResponse = JsonSerializer.Deserialize<CompletionResponse>(responseString);
                                }
                            }
                        }
                    }
                    if (completionResponse is not null)
                    {
                        string? completionText = completionResponse.Choices?[0]?.Message.Content;

                        var uploader = new AmazonUploader();
                        uploader.sendMyFileToS3(completionText);

                        Console.WriteLine("Shipped!! What's next?");
                    }
                }
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }
        }
    }

    Console.WriteLine();

    RunAgain();
}

RunAgain();

