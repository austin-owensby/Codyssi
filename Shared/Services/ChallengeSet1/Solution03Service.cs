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
            int fromBase = 65;

            List<(int, char)> digits = [];
            string answer = string.Empty;

            List<char> digitMapping = Enumerable.Range('0', '9' - '0' + 1).Concat(Enumerable.Range('A', 'Z' - 'A' + 1)).Concat(Enumerable.Range('a', 'z' - 'a' + 1)).Concat(['!', '@', '#']).Select(c => (char)c).ToList();

            while (remainder != 0) {
                int magnitude = (int)Math.Floor(Math.Log(remainder)/Math.Log(fromBase));
                int digitValue = (int)Math.Floor(remainder / Math.Pow(fromBase, magnitude));

                remainder -= (long)Math.Pow(fromBase, magnitude);
                char digitChar = digitMapping[digitValue];
                digits.Add((magnitude, digitChar));
            }

            for (int digit = digits.Max(d => d.Item1); digit >= 0; digit--) {
                if (digits.Any(d => d.Item1 == digit)) {
                    answer += digits.First(d => d.Item1 == digit).Item2;
                }
                else {
                    answer += '0';
                }
            }

            return answer;
        }
    }
}