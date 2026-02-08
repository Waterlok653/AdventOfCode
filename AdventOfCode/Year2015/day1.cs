
using System;
using System.IO;
using System.Numerics;

namespace AdventOfCode.Year2015
{
    public class Day1
    {
        public BigInteger SolveOne()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2015\\inputs\\day1.txt");
            string input = File.ReadAllText(configFile);
            
            return input.Count('(') - input.Count(')');
        }

        public BigInteger SolveTwo()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2015\\inputs\\day1.txt");
            string input = File.ReadAllText(configFile);
            int currentFloor = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    currentFloor++;
                }
                else
                {
                    currentFloor--;
                }
                if (currentFloor == -1)
                {
                    return i + 1;
                }
            }
            return 0;
        }
    }
}
