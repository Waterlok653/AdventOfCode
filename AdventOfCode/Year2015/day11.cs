
using System;
using System.IO;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015
{
    public class Day11
    {
        public string SolveOne()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2015\\inputs\\day11.txt");
            char[] input = File.ReadAllText(configFile).ToCharArray();
            input = GetNextPassword(input);

            return new string(input);
        }

        private char[] GetNextPassword(char[] input)
        {
            bool isPasswordCorrect = false;
            do
            {
                var indexOfInvalid = input.IndexOfAny(new char[] { 'i', 'o', 'l' });
                if (indexOfInvalid != -1)
                {
                    input[indexOfInvalid]++;
                    for (int i = indexOfInvalid + 1; i < input.Length; i++)
                    {
                        input[i] = 'a';
                    }
                    continue;
                }
                bool hasASuite = false;
                for (int i = 0; i < input.Length - 2; i++)
                {
                    if (input[i] + 1 == input[i + 1] && input[i] + 2 == input[i + 2])
                    {
                        hasASuite = true;
                        break;
                    }
                }
                if (!hasASuite)
                {
                    input = IncrementPasswordOne(input);
                    continue;
                }

                var match = Regex.Matches(new string(input), @"([a-zA-Z])\1");

                if (match.GroupBy(t => t.Value).Count() < 2)
                {
                    input = IncrementPasswordOne(input);
                    continue;
                }
                isPasswordCorrect = true;
            } while (!isPasswordCorrect);
            return input;
        }

        private char[] IncrementPasswordOne(char[] mdp)
        {
            for (int i = mdp.Length - 1; i >= 0; i--)
            {
                if (mdp[i] == 'z')
                {
                    mdp[i] = 'a';
                    continue;
                }
                else
                {
                    mdp[i]++;
                    break;
                }
            }
            return mdp;
        }
        public string SolveTwo()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2015\\inputs\\day11.txt");
            char[] input = File.ReadAllText(configFile).ToCharArray();

            input = GetNextPassword(input);
            input = IncrementPasswordOne(input);
            input = GetNextPassword(input);

            return new string(input);
        }
    }
}
