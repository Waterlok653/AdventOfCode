
using System;
using System.IO;
using System.Numerics;

namespace AdventOfCode.Year2021
{
    public class Day3
    {
        public BigInteger SolveOne()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2021\\inputs\\day3.txt");
            string input = File.ReadAllText(configFile);
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);

            return 0;
        }

        public BigInteger SolveTwo()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2021\\inputs\\day3.txt");
            string input = File.ReadAllText(configFile);
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);

            return 0;
        }
    }
}
