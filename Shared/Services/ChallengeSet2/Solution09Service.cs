namespace Codyssi.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../../Inputs/09.txt
    public class Solution09Service : ISolutionDayService
    {
        public string RunPart1Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(9, example);
            List<Point> points = lines.QuickRegex(@"\((-?\d+), (-?\d+)\)").Select(x => x.ToInts()).Select(x => new Point(x[0], x[1])).ToList();
            List<int> distances = points.Select(Utility.ManhattanDistance).ToList();

            int min = distances.Min();
            int max = distances.Max();

            int answer = max - min;

            return answer.ToString();
        }

        public string RunPart2Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(9, example);
            List<Point> points = lines.QuickRegex(@"\((-?\d+), (-?\d+)\)").Select(x => x.ToInts()).Select(x => new Point(x[0], x[1])).ToList();

            Point closestPoint = points.OrderBy(Utility.ManhattanDistance).ThenBy(p => p.X).ThenBy(p => p.Y).First();
            points.Remove(closestPoint);

            int answer = points.Min(point => Utility.ManhattanDistance(point, closestPoint));

            return answer.ToString();
        }

        public string RunPart3Solution(bool example)
        {
            List<string> lines = FileUtility.GetInputLines(9, example);
            List<Point> points = lines.QuickRegex(@"\((-?\d+), (-?\d+)\)").Select(x => x.ToInts()).Select(x => new Point(x[0], x[1])).ToList();

            int answer = 0;

            Point currentPoint = new();

            while (points.Count > 0) {
                Point closestPoint = points.OrderBy(point => Utility.ManhattanDistance(point, currentPoint)).ThenBy(p => p.X).ThenBy(p => p.Y).First();
                points.Remove(closestPoint);

                answer += Utility.ManhattanDistance(currentPoint, closestPoint);
                currentPoint = closestPoint;
            }

            return answer.ToString();
        }
    }
}