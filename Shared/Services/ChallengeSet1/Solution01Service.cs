namespace Codyssi.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/01.txt
    public class Solution01Service : ISolutionDayService
    {
        public string RunPart1Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(1, example);
            List<int> prices = lines.ToInts();

            int answer = prices.Sum();

            return answer.ToString();
        }

        public string RunPart2Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(1, example);
            List<int> prices = lines.ToInts();
            prices.Sort();

            int answer = prices.SkipLast(example ? 2 : 20).Sum();

            return answer.ToString();
        }

        public string RunPart3Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(1, example);
            List<int> prices = lines.ToInts();
            int answer = prices.Select((price, index) => index % 2 == 0 ? price : -price).Sum();

            return answer.ToString();
        }
    }
}