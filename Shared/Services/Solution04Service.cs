namespace Codyssi.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/04.txt
    public class Solution04Service : ISolutionDayService
    {
        public string RunSolution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(4, example);

            int answer = 0;

            foreach (string line in lines)
            {

            }

            return answer.ToString();
        }
    }
}