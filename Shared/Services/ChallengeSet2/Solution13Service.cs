using System.Security.Cryptography.X509Certificates;

namespace Codyssi.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/13.txt
    public class Solution13Service : ISolutionDayService
    {
        public string RunPart1Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(13, example);
            List<List<string>> parts = lines.ChunkByExclusive(string.IsNullOrWhiteSpace);
            Dictionary<string, int> balances = parts[0].QuickRegex(@"([-\w]+) HAS (\d+)").ToDictionary(x => x[0], x => int.Parse(x[1]));
            List<(string from, string to, int amount)> transactions = parts[1].QuickRegex(@"FROM ([-\w]+) TO ([-\w]+) AMT (\d+)").Select(x => (x[0], x[1], int.Parse(x[2]))).ToList();

            foreach ((string from, string to, int amount) in transactions)
            {
                balances[from] -= amount;
                balances[to] += amount;
            }

            int answer = balances.Select(b => b.Value).OrderDescending().Take(3).Sum();

            return answer.ToString();
        }

        public string RunPart2Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(13, example);
            List<List<string>> parts = lines.ChunkByExclusive(string.IsNullOrWhiteSpace);
            Dictionary<string, int> balances = parts[0].QuickRegex(@"([-\w]+) HAS (\d+)").ToDictionary(x => x[0], x => int.Parse(x[1]));
            List<(string from, string to, int amount)> transactions = parts[1].QuickRegex(@"FROM ([-\w]+) TO ([-\w]+) AMT (\d+)").Select(x => (x[0], x[1], int.Parse(x[2]))).ToList();

            foreach ((string from, string to, int amount) in transactions)
            {
                int trueAmount = Math.Min(balances[from], amount);
                balances[from] -= trueAmount;
                balances[to] += trueAmount;
            }

            int answer = balances.Select(b => b.Value).OrderDescending().Take(3).Sum();

            return answer.ToString();
        }

        public string RunPart3Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(13, example);
            List<List<string>> parts = lines.ChunkByExclusive(string.IsNullOrWhiteSpace);
            Dictionary<string, int> balances = parts[0].QuickRegex(@"([-\w]+) HAS (\d+)").ToDictionary(x => x[0], x => int.Parse(x[1]));
            List<(string from, string to, int amount)> transactions = parts[1].QuickRegex(@"FROM ([-\w]+) TO ([-\w]+) AMT (\d+)").Select(x => (x[0], x[1], int.Parse(x[2]))).ToList();

            List<string> names = balances.Select(x => x.Key).ToList();

            Dictionary<string, List<(string, int)>> owedAmounts = names.ToDictionary(x => x, x => new List<(string, int)>());

            foreach ((string from, string to, int amount) in transactions)
            {
                int trueAmount = Math.Min(balances[from], amount);

                if (trueAmount < amount) {
                    owedAmounts[from].Add((to, amount - trueAmount));
                }

                balances[from] -= trueAmount;
                balances[to] += trueAmount;

                while (names.Any(x => balances[x] > 0 && owedAmounts[x].Count > 0)) {
                    string nameTo = names.First(x => balances[x] > 0 && owedAmounts[x].Count > 0);

                    (string owedTo, int owedAmount) = owedAmounts[nameTo][0];

                    if (owedAmount > balances[nameTo]) {
                        owedAmounts[nameTo][0] = (owedTo, owedAmount - balances[nameTo]);
                        balances[owedTo] += balances[nameTo];
                        balances[nameTo] = 0;
                    }
                    else {
                        balances[nameTo] -= owedAmount;
                        balances[owedTo] += owedAmount;
                        owedAmounts[nameTo].RemoveAt(0);
                    }
                }
            }

            int answer = balances.Select(b => b.Value).OrderDescending().Take(3).Sum();

            return answer.ToString();
        }
    }
}