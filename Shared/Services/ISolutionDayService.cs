namespace Codyssi.Services
{
    public interface ISolutionDayService
    {
        /// <summary>
        /// Execute this day's part 1 solution
        /// </summary>
        /// <param name="example"></param>
        /// <returns></returns>
        string RunPart1Solution(bool example);

        /// <summary>
        /// Execute this day's part 2 solution
        /// </summary>
        /// <param name="example"></param>
        /// <returns></returns>
        string RunPart2Solution(bool example);

        /// <summary>
        /// Execute this day's part 3 solution
        /// </summary>
        /// <param name="example"></param>
        /// <returns></returns>
        string RunPart3Solution(bool example);
    }
}