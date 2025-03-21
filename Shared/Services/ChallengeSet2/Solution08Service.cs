namespace Codyssi.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/08.txt
    public class Solution08Service : ISolutionDayService
    {
        public string RunPart1Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(8, example);
            int answer = 0;

            foreach (string line in lines)
            {
                int memoryTotal = line.Select(c => c - 'A' + 1).Sum();
                answer += memoryTotal;
            }

            // 44330
            return answer.ToString();
        }

        public string RunPart2Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(8, example);

            int answer = 0;

            foreach (string line in lines)
            {
                int factor = line.Length / 10;
                string compressed = line[..factor] + (line.Length - 2 * factor).ToString() + line.Substring(line.Length - factor, factor);
                
                int memoryTotal = compressed.Select(c => char.IsLetter(c) ? c - 'A' + 1 : c - '0').Sum();
                answer += memoryTotal;
            }

            return answer.ToString();
        }

        public string RunPart3Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(8, example);

            int answer = 0;

            foreach (string line in lines)
            {
                int currentCount = 0;
                char currentLetter = line[0];

                string compressed = string.Empty;

                foreach (char c in line) {
                    if (c == currentLetter) {
                        currentCount++;
                    }
                    else {
                        compressed += $"{currentCount}{currentLetter}";
                        currentLetter = c;
                        currentCount = 1;
                    }
                }

                compressed += $"{currentCount}{currentLetter}";

                int memoryTotal = compressed.Select(c => char.IsLetter(c) ? c - 'A' + 1 : c - '0').Sum();
                answer += memoryTotal;
            }

            return answer.ToString();
        }
    }
}