namespace Codyssi.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/12.txt
    public class Solution12Service : ISolutionDayService
    {
        public string RunPart1Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(12, example);

            int answer = lines.SelectMany().Count(char.IsLetter);

            return answer.ToString();
        }

        public string RunPart2Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(12, example);

            int answer = 0;

            foreach (string line in lines)
            {
                string prevLine = line;

                bool keepLooping = true;
                while (keepLooping) {
                    keepLooping = false;
                    string newLine = string.Empty;

                    for (int i = 0; i < prevLine.Length; i++) {
                        if (keepLooping) {
                            newLine += prevLine[i];
                        }
                        else if (i != prevLine.Length - 1 && char.IsNumber(prevLine[i]) != char.IsNumber(prevLine[i + 1])) {
                            keepLooping = true;
                            i++;
                        }
                        else {
                            newLine += prevLine[i];
                        }
                    }

                    prevLine = newLine;
                }
                
                answer += prevLine.Length;
            }

            return answer.ToString();
        }

        public string RunPart3Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(12, example);

            int answer = 0;

            foreach (string line in lines)
            {
                string prevLine = line;

                bool keepLooping = true;
                while (keepLooping) {
                    keepLooping = false;
                    string newLine = string.Empty;

                    for (int i = 0; i < prevLine.Length; i++) {
                        if (keepLooping) {
                            newLine += prevLine[i];
                        }
                        else if (i != prevLine.Length - 1 && (char.IsNumber(prevLine[i]) && char.IsLetter(prevLine[i + 1]) || char.IsLetter(prevLine[i]) && char.IsNumber(prevLine[i + 1]))) {
                            keepLooping = true;
                            i++;
                        }
                        else {
                            newLine += prevLine[i];
                        }
                    }

                    prevLine = newLine;
                }
                
                answer += prevLine.Length;
            }

            return answer.ToString();
        }
    }
}