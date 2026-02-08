namespace AdventOfCode.Year2024
{
    public class Day1
    {
        public Int128 SolveOne()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2024\\inputs\\day1.txt");
            string input = System.IO.File.ReadAllText(configFile);
            string[] inputs = input.Split(["\r\n", "\n\r", "\r", "\n"], StringSplitOptions.None);

            return 0;
        }
        public Int128 SolveTwo()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2024\\inputs\\day1.txt");
            string input = System.IO.File.ReadAllText(configFile);
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);

            return 0;
        }
    }
}
