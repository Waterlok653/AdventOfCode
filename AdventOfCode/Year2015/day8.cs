
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015
{
    public class Day8
    {
        public BigInteger SolveOne()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2015\\inputs\\day8.txt");
            string input = File.ReadAllText(configFile);
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);

            var sum = 0;
            foreach (var line in inputs)
            {
                sum += 2;
                var match = Regex.Matches(line, @"(\\\\)|(\\"")|(\\x)");
                sum += match.Count;
                sum += match.Count(t => t.Value.Equals("\\x")) * 2;
            }

            return sum;
        }

        public BigInteger SolveTwo()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2015\\inputs\\day8.txt");
            string input = File.ReadAllText(configFile);
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);

            var sum = 0;
            foreach (var line in inputs)
            {
                sum += 4;
                var match = Regex.Matches(line, @"(\\\\)|(\\"")|(\\x)");
                sum += match.Count * 2;
                sum -= match.Count(t => t.Value.Equals("\\x"));
            }

            return sum;
        }
    }
}
