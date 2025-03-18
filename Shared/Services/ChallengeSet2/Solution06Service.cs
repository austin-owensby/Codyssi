namespace Codyssi.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/06.txt
    public class Solution06Service : ISolutionDayService
    {
        public string RunPart1Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(6, example);
            long add = long.Parse(lines[0].Split(" ").Last());
            long mult = long.Parse(lines[1].Split(" ").Last());
            long power = long.Parse(lines[2].Split(" ").Last());

            List<long> prices = lines.Skip(4).ToLongs();
            prices.Sort();

            long answer = prices[(prices.Count - 1) / 2];

            answer = (long)Math.Pow(answer, power);
            answer *= mult;
            answer += add;

            return answer.ToString();
        }

        public string RunPart2Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(6, example);
            long add = long.Parse(lines[0].Split(" ").Last());
            long mult = long.Parse(lines[1].Split(" ").Last());
            long power = long.Parse(lines[2].Split(" ").Last());

            List<long> prices = lines.Skip(4).ToLongs().Where(p => p % 2 == 0).ToList();

            long answer = prices.Sum();

            answer = (long)Math.Pow(answer, power);
            answer *= mult;
            answer += add;

            return answer.ToString();
        }

        public string RunPart3Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(6, example);
            long add = long.Parse(lines[0].Split(" ").Last());
            long mult = long.Parse(lines[1].Split(" ").Last());
            long power = long.Parse(lines[2].Split(" ").Last());

            List<long> prices = lines.Skip(4).ToLongs().ToList();

            // 5298

            long max = 15000000000000;

            long answer = 0;

            long bestQuantity = 0;

            foreach (long price in prices)
            {
                long quantity = price;
                quantity = (long)Math.Pow(quantity, power);
                quantity *= mult;
                quantity += add;

                if (quantity <= max && quantity > bestQuantity) {
                    bestQuantity = quantity;
                    answer = price;
                }
            }

            return answer.ToString();
        }
    }
}