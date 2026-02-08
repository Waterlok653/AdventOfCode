
using System;
using System.IO;
using System.Numerics;

namespace AdventOfCode.Year2015
{
    public class Day2
    {
        public BigInteger SolveOne()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2015\\inputs\\day2.txt");
            string input = File.ReadAllText(configFile);
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);

            long sumOfPapper = 0;

            foreach (var gift in inputs)
            {
                var lenght = gift.Split('x');
                int sideOne = int.Parse(lenght[0]);
                int sideTwo = int.Parse(lenght[1]);
                int sideThree = int.Parse(lenght[2]);

                var surfaceOne = sideOne * sideTwo;
                var surfaceTwo = sideTwo * sideThree;
                var sufaceThree = sideThree * sideOne;

                var smalest = Math.Min(Math.Min(surfaceOne, surfaceTwo), sufaceThree);

                sumOfPapper += smalest + 2 * surfaceTwo + 2 * surfaceOne + 2 * sufaceThree;
            }

            return sumOfPapper;
        }

        public BigInteger SolveTwo()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2015\\inputs\\day2.txt");
            string input = File.ReadAllText(configFile);
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);

            long sumOfRibon = 0;

            foreach (var gift in inputs)
            {
                var lenght = gift.Split('x');
                int sideOne = int.Parse(lenght[0]);
                int sideTwo = int.Parse(lenght[1]);
                int sideThree = int.Parse(lenght[2]);

                var surfaceOne = 2 * sideOne + 2* sideTwo;
                var surfaceTwo = 2 * sideTwo + 2 * sideThree;
                var sufaceThree = 2 * sideThree + 2 * sideOne;

                var smalest = Math.Min(Math.Min(surfaceOne, surfaceTwo), sufaceThree);

                sumOfRibon += smalest + sideOne*sideTwo*sideThree;
            }

            return sumOfRibon;
        }
    }
}
