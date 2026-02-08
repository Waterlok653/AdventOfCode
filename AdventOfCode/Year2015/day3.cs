
using System;
using System.IO;
using System.Numerics;

namespace AdventOfCode.Year2015
{
    public class Day3
    {
        public BigInteger SolveOne()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2015\\inputs\\day3.txt");
            string input = File.ReadAllText(configFile);

            List<(int,int)> visited = new List<(int, int)>();
            var posX = 0;
            var posY = 0;

            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '<': posX--; break;
                    case '>': posX++; break;
                    case 'v': posY--; break;
                    case '^': posY++; break;
                }
                visited.Add((posX,posY));
            }

            return visited.GroupBy(t => t).Count();
        }

        public BigInteger SolveTwo()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2015\\inputs\\day3.txt");
            string input = File.ReadAllText(configFile);

            List<(int, int)> visited = new List<(int, int)>();
            var posX = 0;
            var posY = 0;
            var posXR = 0;
            var posYR = 0;
            visited.Add((posX, posY));

            for (int i = 0; i < input.Length; i+=2)
            {
                switch (input[i])
                {
                    case '<': posX--; break;
                    case '>': posX++; break;
                    case 'v': posY--; break;
                    case '^': posY++; break;
                }
                visited.Add((posX, posY));

                switch (input[i+1])
                {
                    case '<': posXR--; break;
                    case '>': posXR++; break;
                    case 'v': posYR--; break;
                    case '^': posYR++; break;
                }
                visited.Add((posXR, posYR));
            }

            return visited.GroupBy(t => t).Count();
        }
    }
}
