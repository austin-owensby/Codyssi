namespace Codyssi.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/03.txt
    public class Solution03Service : ISolutionDayService
    {
        public string RunPart1Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(3, example);
            int answer = lines.Select(l => l.Split(' ')).Select(l => l[1]).ToInts().Sum();

            return answer.ToString();
        }

        public string RunPart2Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(3, example);
            List<(string, int)> readings = lines.Select(l => l.Split(' ')).Select(l => (l[0], int.Parse(l[1]))).ToList();

            long answer = 0;

            foreach ((string number, int numberBase) in readings)
            {
                answer += Convert.ToInt32(number, numberBase);
            }

            return answer.ToString();
        }

        public string RunPart3Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(3, example);
            List<(string, int)> readings = lines.Select(l => l.Split(' ')).Select(l => (l[0], int.Parse(l[1]))).ToList();

            long sum = 0;

            foreach ((string number, int numberBase) in readings)
            {
                sum += Convert.ToInt32(number, numberBase);
            }

            long remainder = sum;
            int digit = 0;
            int fromBase = 65;
            string answer = string.Empty;

            while (remainder != 0) {
                //int digitValue = Math.Floor(remainder / Math.Pow(fromBase, digit));
                char digitChar = 'a';
                answer = $"{digitChar}answer";

                digit++;
            }

            return answer;
        }
    }
}