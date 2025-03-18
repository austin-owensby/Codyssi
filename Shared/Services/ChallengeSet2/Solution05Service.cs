namespace Codyssi.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/05.txt
    public class Solution05Service : ISolutionDayService
    {
        public string RunPart1Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(5, example);
            int start = int.Parse(lines[0]);
            List<int> corrections = lines.Skip(1).SkipLast(1).ToInts();
            List<char> operations = lines.Last().ToList();

            int answer = start;

            for (int i = 0; i < corrections.Count; i++)
            {
                answer += corrections[i] * (operations[i] == '+' ? 1 : -1);
            }

            return answer.ToString();
        }

        public string RunPart2Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(5, example);
            int start = int.Parse(lines[0]);
            List<int> corrections = lines.Skip(1).SkipLast(1).ToInts();
            List<char> operations = lines.Last().ToList().ReverseInPlace();

            int answer = start;

            for (int i = 0; i < corrections.Count; i++)
            {
                answer += corrections[i] * (operations[i] == '+' ? 1 : -1);
            }

            return answer.ToString();
        }

        public string RunPart3Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(5, example);
            List<int> numbers = lines.SkipLast(1).Chunk(2).Select(c => string.Join(string.Empty, c)).ToInts();
            int start = numbers.First();
            List<int> corrections = numbers.Skip(1).ToList();
            List<char> operations = lines.Last().ToList().ReverseInPlace();

            int answer = start;

            for (int i = 0; i < corrections.Count; i++)
            {
                answer += corrections[i] * (operations[i] == '+' ? 1 : -1);
            }

            return answer.ToString();
        }
    }
}