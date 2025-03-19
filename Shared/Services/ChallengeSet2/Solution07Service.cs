namespace Codyssi.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/07.txt
    public class Solution07Service : ISolutionDayService
    {
        public string RunPart1Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(7, example);
            List<List<int>> inventory = lines.QuickRegex(@"(\d+)-(\d+) (\d+)-(\d+)").Select(l => l.ToInts()).ToList();

            int answer = 0;

            foreach (List<int> line in inventory)
            {
                answer += line[1] - line[0] + 1 + line[3] - line[2] + 1;
            }

            return answer.ToString();
        }

        public string RunPart2Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(7, example);
            List<List<int>> inventory = lines.QuickRegex(@"(\d+)-(\d+) (\d+)-(\d+)").Select(l => l.ToInts()).ToList();

            int answer = 0;

            foreach (List<int> line in inventory)
            {
                answer += Enumerable.Range(line[0], line[1] - line[0] + 1).Concat(Enumerable.Range(line[2], line[3] - line[2] + 1)).Distinct().Count();
            }

            return answer.ToString();
        }

        public string RunPart3Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(7, example);
            List<List<int>> inventory = lines.QuickRegex(@"(\d+)-(\d+) (\d+)-(\d+)").Select(l => l.ToInts()).ToList();

            int answer = 0;

            for (int i = 0; i < inventory.Count - 1; i++) {
                List<int> group1 = inventory[i];
                List<int> group2 = inventory[i + 1];
                List<int> group1Boxes = Enumerable.Range(group1[0], group1[1] - group1[0] + 1).Concat(Enumerable.Range(group1[2], group1[3] - group1[2] + 1)).ToList();
                List<int> group2Boxes = Enumerable.Range(group2[0], group2[1] - group2[0] + 1).Concat(Enumerable.Range(group2[2], group2[3] - group2[2] + 1)).ToList();

                int uniqueBoxes = group1Boxes.Concat(group2Boxes).Distinct().Count();
                
                if (uniqueBoxes > answer) {
                    answer = uniqueBoxes;
                }
            }

            return answer.ToString();
        }
    }
}