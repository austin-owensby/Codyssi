namespace Codyssi.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/10.txt
    public class Solution10Service : ISolutionDayService
    {
        public string RunPart1Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(10, example);

            int answer = lines[0].Count(char.IsLetter);

            return answer.ToString();
        }

        public string RunPart2Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(10, example);

            int answer = lines[0].Sum(c => char.IsLetter(c) ? c.GetCharValue() : 0);

            return answer.ToString();
        }

        public string RunPart3Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(10, example);
            List<char> chars = lines[0].ToList();

            foreach (int i in chars.Count) {
                if (!char.IsLetter(chars[i])) {
                    int value = Utility.Mod(chars[i - 1].GetCharValue() * 2 - 5 - 1, 52) + 1;
                    chars[i] = (char)(value <= 26 ? value + 'a' - 1 : value - 26 + 'A' - 1); 
                }
            }

            int answer = chars.Sum(c => c.GetCharValue());

            return answer.ToString();
        }
    }
}