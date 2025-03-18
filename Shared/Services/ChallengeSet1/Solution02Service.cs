namespace Codyssi.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/02.txt
    public class Solution02Service : ISolutionDayService
    {
        public string RunPart1Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(2, example);
            List<bool> outputs = lines.Select(l => l == "TRUE").ToList();
            int answer = 0;

            for (int i = 0; i < outputs.Count; i++) {
                if (outputs[i]) {
                    answer += i + 1;
                }
            }

            return answer.ToString();
        }

        public string RunPart2Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(2, example);
            List<bool> outputs = lines.Select(l => l == "TRUE").ToList();
            List<bool[]> gates = outputs.Chunk(2).ToList();

            int answer = 0;

            for (int i = 0; i < gates.Count; i++)
            {
                bool[] gate = gates[i];

                bool result = i % 2 == 0 ? gate[0] && gate[1] : gate[0] || gate[1];

                if (result) {
                    answer++;
                }
            }

            return answer.ToString();
        }

        public string RunPart3Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(2, example);
            List<bool> outputs = lines.Select(l => l == "TRUE").ToList();

            int answer = outputs.Count(x => x);

            while (outputs.Count > 1) {
                List<bool[]> gates = outputs.Chunk(2).ToList();
                List<bool> nextOutputs = [];

                for (int i = 0; i < gates.Count; i++)
                {
                    bool[] gate = gates[i];

                    bool result = i % 2 == 0 ? gate[0] && gate[1] : gate[0] || gate[1];

                    if (result) {
                        answer++;
                    }

                    nextOutputs.Add(result);
                }

                outputs = nextOutputs;
            }

            return answer.ToString();
        }
    }
}