
using System;
using System.IO;
using System.Numerics;
using System.Security.Cryptography;

namespace AdventOfCode.Year2015
{
    public class Day4
    {
        public BigInteger SolveOne()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2015\\inputs\\day4.txt");
            string input = File.ReadAllText(configFile);

            for (int i = 1;i < int.MaxValue; i++)
            {
                var value = $"{input}{i}";
                var hash = MD5.HashData(value.Select(t => (byte)t).ToArray());
                
                if (hash[0] == 0 && hash[1] == 0 && hash[2] <= 15)
                {
                    return i;
                }
            }

            return 0;
        }

        public BigInteger SolveTwo()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2015\\inputs\\day4.txt");
            string input = File.ReadAllText(configFile);

            for (int i = 1; i < int.MaxValue; i++)
            {
                var value = $"{input}{i}";
                var hash = MD5.HashData(value.Select(t => (byte)t).ToArray());

                if (hash[0] == 0 && hash[1] == 0 && hash[2] == 0)
                {
                    return i;
                }
            }
            return 0;
        }
    }
}
