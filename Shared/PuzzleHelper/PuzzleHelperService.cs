using Codyssi.Gateways;

namespace Codyssi.PuzzleHelper
{
    public class PuzzleHelperService(CodyssiGateway codyssiGateway)
    {
        private readonly CodyssiGateway codyssiGateway = codyssiGateway;

        /// <summary>
        /// Imports the day's input file.
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public async Task<string> ImportInputFile(int day)
        {
            string output = string.Empty;

            bool update = await WriteInputFile(day);

            if (update)
            {
                output = $"Created input file for Day: {day}.";
            }
            else
            {
                System.Console.WriteLine("No updates applied.");
                output += "No updates applied.\n ";
            }

            return output;
        }

        /// <summary>
        /// Fetch and write the input file if it doesn't exist
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        private async Task<bool> WriteInputFile(int day)
        {
            bool update = false;

            string directoryPath = Directory.GetParent(Environment.CurrentDirectory)!.FullName;
            string folderPath = Path.Combine(directoryPath, "Inputs");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string inputFilePath = Path.Combine(directoryPath, "Inputs", $"{day:D2}.txt");

            if (!File.Exists(inputFilePath))
            {
                string response;
                try
                {
                    response = await codyssiGateway.ImportInput(day);
                }
                catch (Exception)
                {
                    System.Console.WriteLine("An error occurred while getting the puzzle input from Codyssi");
                    throw;
                }

                using StreamWriter inputFile = new(inputFilePath);
                await inputFile.WriteAsync(response);

                System.Console.WriteLine($"Created input file for Day: {day}.");
                update = true;
            }

            string inputExampleFilePath = Path.Combine(directoryPath, "Inputs", $"{day:D2}_example.txt");

            if (!File.Exists(inputExampleFilePath))
            {
                string response;
                try
                {
                    response = await codyssiGateway.ImportInputExample(day);
                }
                catch (Exception)
                {
                    System.Console.WriteLine("An error occurred while getting the puzzle input example from Codyssi");
                    throw;
                }

                using StreamWriter inputExampleFile = new(inputExampleFilePath);
                await inputExampleFile.WriteAsync(response);

                System.Console.WriteLine($"Created input example file for Day: {day}.");
                update = true;
            }

            return update;
        }
    }
}