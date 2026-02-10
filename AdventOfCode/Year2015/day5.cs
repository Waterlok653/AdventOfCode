
using System;
using System.IO;
using System.Numerics;
using System.Threading;

namespace AdventOfCode.Year2015
{
    public class Day5
    {
        public BigInteger SolveOne()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2015\\inputs\\day5.txt");
            string input = File.ReadAllText(configFile);
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);


            return inputs.Count(t => IsItNice(t));
        }

        private bool IsItNice(string text)
        {
            return ContainsAtLeastThreeVowels(text) && ContainsDoubleLetter(text) && DontContainSpecial(text);
        }
        private bool ContainsAtLeastThreeVowels(string text)
        {
            return text.Count(c => c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u') >= 3;
        }
        private bool ContainsDoubleLetter(string text)
        {
            char lastLetter = ' ';
            foreach (char c in text)
            {
                if (lastLetter == c)
                {
                    return true;
                }
                lastLetter = c;
            }
            return false;
        }
        private bool DontContainSpecial(string text)
        {
            return !(text.Contains("ab") || text.Contains("cd") || text.Contains("pq") || text.Contains("xy"));
        }

        public BigInteger SolveTwo()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2015\\inputs\\day5.txt");
            string input = File.ReadAllText(configFile);
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);

            return inputs.Count(t => IsItNice2(t));
        }
        private bool IsItNice2(string text)
        {
            return TwoLetterPair(text) && DoubleCharSeparate(text);
        }

        private bool TwoLetterPair(string text)
        {
            for (int i = 0; i < text.Length - 3; i++) {
                for (int j = i+2; j < text.Length - 1; j++)
                {
                    if (text[i] == text[j] && text[i+1] == text[j+1])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool DoubleCharSeparate(string text)
        {
            for (int i = 2; i < text.Length; i++) { 
                if (text[i] == text[i - 2])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
