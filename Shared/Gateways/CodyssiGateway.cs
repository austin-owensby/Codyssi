using System.Net;
using HtmlAgilityPack;

namespace Codyssi.Gateways
{
    public class CodyssiGateway
    {
        private HttpClient? client;
        private readonly int throttleInMinutes = 3;
        private DateTimeOffset? lastCall = null;

        /// <summary>
        /// For a given day, get the user's puzzle input
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public async Task<string> ImportInput(int day)
        {
            ThrottleCall();

            HttpRequestMessage message = new(HttpMethod.Get, $"/view_problem_{day}_input");

            if (client == null)
            {
                try
                {
                    InitializeClient();
                }
                catch
                {
                    throw new Exception("Unable to read Cookie.txt. Make sure that it exists in the PuzzleHelper folder. See the ReadMe for more.");
                }
            }

            HttpResponseMessage result = await client!.SendAsync(message);
            string response = await GetSuccessfulResponseContent(result);

            try
            {
                // Display the response
                HtmlDocument doc = new();
                doc.LoadHtml(response);
                HtmlNode code = doc.DocumentNode.SelectSingleNode("//body");
                response = code.InnerHtml;
                response = response.Replace("<br>", string.Empty);
                response = string.Join("\n", response.Split("\n").Where(r => !string.IsNullOrEmpty(r)).Select(r => r.Trim()));
                response = response.Replace("\n\n", "\n");
                response = response.Trim('\n');
            }
            catch (Exception)
            {
                System.Console.WriteLine("Error parsing html response.");
            }

            return response;
        }

        /// <summary>
        /// For a given day, get the user's puzzle test input
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public async Task<string> ImportInputExample(int day)
        {
            HttpRequestMessage message = new(HttpMethod.Get, $"https://www.codyssi.com/view_problem_{day}");

            if (client == null)
            {
                try
                {
                    InitializeClient();
                }
                catch
                {
                    throw new Exception("Unable to read Cookie.txt. Make sure that it exists in the PuzzleHelper folder. See the ReadMe for more.");
                }
            }

            HttpResponseMessage result = await client!.SendAsync(message);
            string response = await GetSuccessfulResponseContent(result);

            try
            {
                // Display the response
                HtmlDocument doc = new();
                doc.LoadHtml(response);
                HtmlNode code = doc.DocumentNode.SelectSingleNode("//code");
                response = code.InnerHtml;
                response = response.Replace("<br>", string.Empty);
                response = string.Join("\n", response.Split("\n").Where(r => !string.IsNullOrEmpty(r)).Select(r => r.Trim()));
                response = response.Replace("\n\n", "\n");
                response = response.Trim('\n');
            }
            catch (Exception)
            {
                System.Console.WriteLine("Error parsing html response.");
            }

            return response;
        }

        /// <summary>
        /// Send the user's answer to the specific question
        /// </summary>
        /// <param name="day"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
        public async Task<string> SubmitAnswer(int day, int part, string answer)
        {
            ThrottleCall();

            Dictionary<string, string> data = new()
            {
                { "part_answer_input", answer }
            };

            HttpContent request = new FormUrlEncodedContent(data);

            if (client == null)
            {
                try
                {
                    InitializeClient();
                }
                catch
                {
                    return "Unable to read Cookie.txt. Make sure that it exists in the PuzzleHelper folder. See the ReadMe for more.";
                }
            }

            HttpResponseMessage result = await client!.PostAsync($"/submit_problem_{day}_part_{part}", request);

            string response = await GetSuccessfulResponseContent(result);

            try
            {
                // Display the response
                HtmlDocument doc = new();
                doc.LoadHtml(response);
                HtmlNode h3 = doc.DocumentNode.SelectSingleNode("//h3");
                response = h3.InnerHtml.Trim();
            }
            catch (Exception)
            {
                System.Console.WriteLine("Error parsing html response.");
            }

            return response;
        }

        /// <summary>
        /// Ensure that the response was successful and return the parsed response if it was
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private static async Task<string> GetSuccessfulResponseContent(HttpResponseMessage result)
        {
            if (result.StatusCode == HttpStatusCode.Unauthorized) {
                throw new Exception("Your Cookie has expired, please update it. See the ReadMe for more info.");
            }

            result.EnsureSuccessStatusCode();
            return await result.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Tracks the last API call and prevents another call from being made until after the configured limit
        /// </summary>
        private void ThrottleCall()
        {
            if (lastCall != null && DateTimeOffset.Now < lastCall.Value.AddMinutes(throttleInMinutes))
            {
                throw new Exception($"Unable to make another API call to Codyssi Server because we are attempting to throttle calls according to their specifications (See more in the ReadMe). Please try again after {lastCall.Value.AddMinutes(throttleInMinutes)}.");
            }
            else
            {
                lastCall = DateTimeOffset.Now;
            }
        }

        /// <summary>
        /// Initialize the Http Client using the user's cookie
        /// </summary>
        private void InitializeClient()
        {
            // We're waiting to do this until the last moment in case someone want's to use the code base without setting up the cookie
            client = new HttpClient
            {
                BaseAddress = new Uri("https://www.codyssi.com/")
            };

            client.DefaultRequestHeaders.UserAgent.ParseAdd(".NET 8.0 (+via https://github.com/austin-owensby/Codyssi by austin_owensby@hotmail.com)");

            string[] fileData;

            try
            {
                string directoryPath = Directory.GetParent(Environment.CurrentDirectory)!.FullName;
                string filePath = Path.Combine(directoryPath, "Shared", "PuzzleHelper", "Cookie.txt");
                fileData = File.ReadAllLines(filePath);
            }
            catch (Exception)
            {
                throw new Exception("Unable to read Cookie.txt. Make sure that it exists in the PuzzleHelper folder. See the ReadMe for more.");
            }

            if (fileData.Length == 0 || string.IsNullOrWhiteSpace(fileData[0]))
            {
                throw new Exception("Cookie.txt is empty. Please ensure it is properly populated and saved. See the ReadMe for more.");
            }
            if (fileData.Length > 1)
            {
                throw new Exception("Detected multiple lines in Cookie.txt, ensure that the whole cookie is on 1 line.");
            }

            string cookie = fileData[0];
            client.DefaultRequestHeaders.Add("Cookie", cookie);
        }
    }
}