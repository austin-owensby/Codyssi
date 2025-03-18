using Microsoft.Extensions.DependencyInjection; // This is needed for the Console App
using System.Diagnostics;
using Codyssi.Gateways;

namespace Codyssi.Services
{
    public class SolutionService(IServiceProvider serviceProvider, CodyssiGateway codyssiGateway)
    {
        private readonly IServiceProvider serviceProvider = serviceProvider;
        private readonly CodyssiGateway codyssiGateway = codyssiGateway;

        /// <summary>
        /// Execute the specific solution based on the passed in parameters
        /// </summary>
        /// <param name="day"></param>
        /// <param name="send"></param>
        /// <param name="example"></param>
        /// <returns></returns>
        /// <exception cref="SolutionNotFoundException"></exception>
        public async Task<string> GetSolution(int day, bool send, bool example)
        {
            System.Console.WriteLine($"Running solution for day: {day}, example: {(example ? "yes" : "no")}, submit: {(send ? "yes" : "no")}");
            ISolutionDayService service = FindSolutionService(day);

            Stopwatch sw = Stopwatch.StartNew();
            // Run the specific solution
            string answer = service.RunSolution(example);
            sw.Stop();
            System.Console.WriteLine($"Elapsed time: {sw.Elapsed}");

            // Optionally submit the answer to Codyssi
            if (send)
            {
                try
                {
                    string response = await codyssiGateway.SubmitAnswer(day, answer);
                    answer = $"Submitted answer: {answer}.\nCodyssi response: {response}";
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("An error occurred while submitting the answer to Codyssi");
                    answer = $"Submitted answer: {answer}.\nCodyssi response: {e.Message}";
                }
            }

            return answer;
        }

        /// <summary>
        /// Fetch the specific service for the specified day
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        private ISolutionDayService FindSolutionService(int day)
        {
            IEnumerable<ISolutionDayService> services = serviceProvider.GetServices<ISolutionDayService>();

            // Use ':D2' to front pad 0s to single digit days to match the formatting
            string serviceName = $"Codyssi.Services.Solution{day:D2}Service";
            ISolutionDayService? service = services.FirstOrDefault(s => s.GetType().ToString() == serviceName);

            // If the service was not found, throw an exception
            if (service == null)
            {
                throw new SolutionNotFoundException($"No solutions found for day {day}.");
            }

            return service;
        }
    }
}