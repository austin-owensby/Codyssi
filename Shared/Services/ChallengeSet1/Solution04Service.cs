using System.Formats.Asn1;

namespace Codyssi.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/04.txt
    public class Solution04Service : ISolutionDayService
    {
        public string RunPart1Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(4, example);
            List<List<string>> paths = lines.Select(l => l.Split(" <-> ").ToList()).ToList();

            int answer = paths.SelectMany().Distinct().Count();

            return answer.ToString();
        }

        public string RunPart2Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(4, example);
            List<List<string>> paths = lines.Select(l => l.Split(" <-> ").ToList()).ToList();

            List<string> locations = ["STT"];

            foreach (int i in 3) {
                locations = paths.Where(p => locations.Contains(p[0]) || locations.Contains(p[1])).SelectMany().Distinct().ToList();
            }

            int answer = locations.Count();

            return answer.ToString();
        }

        public string RunPart3Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(4, example);
            List<List<string>> paths = lines.Select(l => l.Split(" <-> ").ToList()).ToList();

            List<string> uniquePaths = paths.SelectMany().Distinct().ToList();

            List<string> locations = ["STT"];

            int answer = 0;

            foreach (int i in uniquePaths.Count) {
                List<string> nextLocations = paths.Where(p => locations.Contains(p[0]) || locations.Contains(p[1])).SelectMany().Distinct().ToList();

                List<string> newLocations = nextLocations.Except(locations).ToList();

                answer += (i + 1) * newLocations.Count;

                locations = nextLocations;
            }

            return answer.ToString();
        }
    }
}