
using System;
using System.IO;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015
{
    public class Day6
    {
        public BigInteger SolveOne()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2015\\inputs\\day6.txt");
            string input = File.ReadAllText(configFile);
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);

            bool[,] table = new bool[1000, 1000];
            foreach (string line in inputs)
            {
                var match = Regex.Match(line, @"(\d+,\d+).+\b(\d+,\d+)");
                var start = match.Groups[1].Value.Split(',');
                var end = match.Groups[2].Value.Split(",");
                for (int i = int.Parse(start[0]); i <= int.Parse(end[0]); i++)
                {
                    for (int j = int.Parse(start[1]); j <= int.Parse(end[1]); j++)
                    {
                        switch (line[6])
                        {
                            case ' ': table[i, j] = !table[i, j]; break;
                            case 'f': table[i, j] = false; break;
                            case 'n': table[i, j] = true; break;
                        }
                    }
                }
            }
            var count = 0;
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (table[i, j])
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public BigInteger SolveTwo()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2015\\inputs\\day6.txt");
            string input = File.ReadAllText(configFile);
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);

            int[,] table = new int[1000, 1000];
            foreach (string line in inputs)
            {
                var match = Regex.Match(line, @"(\d+,\d+).+\b(\d+,\d+)");
                var start = match.Groups[1].Value.Split(',');
                var end = match.Groups[2].Value.Split(",");
                for (int i = int.Parse(start[0]); i <= int.Parse(end[0]); i++)
                {
                    for (int j = int.Parse(start[1]); j <= int.Parse(end[1]); j++)
                    {
                        switch (line[6])
                        {
                            case ' ': table[i, j] += 2; break;
                            case 'f': if (table[i, j] != 0) table[i, j]--; break;
                            case 'n': table[i, j]++; break;
                        }
                    }
                }
            }
            long count = 0;
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    count += table[i, j];

                }
            }
            return count;
        }
    }
}
