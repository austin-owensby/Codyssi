namespace Codyssi.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/11.txt
    public class Solution11Service : ISolutionDayService
    {
        public string RunPart1Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(11, example);
            List<List<string>> parts = lines.ChunkByExclusive(string.IsNullOrWhiteSpace);

            List<int> frequencies = parts[0].ToInts();
            List<(int, int)> swaps = parts[1].Select(p => p.Split('-').ToInts()).Select(x => (x[0], x[1])).ToList();
            int testIndex = int.Parse(parts[2][0]);

            foreach ((int, int) swap in swaps)
            {
                (frequencies[swap.Item2 - 1], frequencies[swap.Item1 - 1]) = (frequencies[swap.Item1 - 1], frequencies[swap.Item2 - 1]);
            }

            int answer = frequencies[testIndex - 1];

            return answer.ToString();
        }

        public string RunPart2Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(11, example);
            List<List<string>> parts = lines.ChunkByExclusive(string.IsNullOrWhiteSpace);

            List<int> frequencies = parts[0].ToInts();

            List<int> swapDigits = parts[1].Select(p => p.Split('-').ToInts()).SelectMany().ToList();
            swapDigits.Add(swapDigits[0]);
            List<(int, int, int)> swaps = [];

            for (int i = 0; i < swapDigits.Count - 2; i += 2) {
                swaps.Add((swapDigits[i], swapDigits[i + 1], swapDigits[i + 2]));
            }

            int testIndex = int.Parse(parts[2][0]);

            foreach ((int, int, int) swap in swaps)
            {
                (frequencies[swap.Item2 - 1], frequencies[swap.Item3 - 1], frequencies[swap.Item1 - 1]) = (frequencies[swap.Item1 - 1], frequencies[swap.Item2 - 1], frequencies[swap.Item3 - 1]);
            }

            int answer = frequencies[testIndex - 1];

            return answer.ToString();
        }

        public string RunPart3Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(11, example);
            List<List<string>> parts = lines.ChunkByExclusive(string.IsNullOrWhiteSpace);

            List<int> frequencies = parts[0].ToInts();
            List<(int, int)> swaps = parts[1].Select(p => p.Split('-').ToInts().Order().ToList()).Select(x => (x[0], x[1])).ToList();
            int testIndex = int.Parse(parts[2][0]);

            foreach ((int, int) swap in swaps)
            {
                int blockSize = Math.Min(swap.Item2 - swap.Item1, frequencies.Count - swap.Item2 + 1);

                foreach (int i in blockSize) {
                    (frequencies[swap.Item2 - 1 + i], frequencies[swap.Item1 - 1 + i]) = (frequencies[swap.Item1 - 1 + i], frequencies[swap.Item2 - 1 + i]);
                }

            }

            int answer = frequencies[testIndex - 1];

            return answer.ToString();
        }
    }
}