using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2025
{
    public static class Day2
    {
        public static Int128 SolveOne()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day2.txt");
            string[] inputs = input.Split(",");
            (long, long)[] ranges = inputs.Select(s =>
            {
                var split = s.Split("-");
                (long, long) newValue = (long.Parse(split[0]), long.Parse(split[1]));
                return newValue;
            }).ToArray();
            List<long> NbNotOk = new List<long>();

            foreach (var range in ranges)
            {
                for (long i = range.Item1; i <= range.Item2; i++)
                {
                    string nbInString = i.ToString();
                    //si pas modulo 2 il peut pas se repeter
                    if (nbInString.Length % 2 == 1)
                    {
                        continue;
                    }

                    int lenghtOfPart = nbInString.Length / 2;
                    //prend la premier et dernier partie du mot
                    if (nbInString[lenghtOfPart..] == nbInString[..^lenghtOfPart])
                    {
                        NbNotOk.Add(i);
                    }

                }
            }
            return NbNotOk.Sum();
        }


        public static Int128 SolveTwo()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day2.txt");
            string[] inputs = input.Split(",");
            (long, long)[] ranges = inputs.Select(s =>
            {
                var split = s.Split("-");
                (long, long) newValue = (long.Parse(split[0]), long.Parse(split[1]));
                return newValue;
            }).ToArray();
            List<long> NbNotOk = new List<long>();

            foreach (var range in ranges)
            {
                for (long i = range.Item1; i <= range.Item2; i++)
                {
                    string nbInString = i.ToString();
                    bool isAdd = false;
                    for (int j = 2; j <= nbInString.Length; j++)
                    {
                        if (isAdd)
                        {
                            break;
                        }
                        if (nbInString.Length % j == 0)
                        {
                            int lenghtOfPart = nbInString.Length / j;
                            List<string> parts = new List<string>();
                            for (int k = 1; k <= j; k++)
                            {
                                int start = (lenghtOfPart * k - lenghtOfPart);
                                int end = (lenghtOfPart * k);
                                parts.Add(nbInString[start..end]);
                            }

                            string first = parts.First();
                            bool isAllTheSame = true;
                            foreach (var part in parts)
                            {
                                if (!part.Equals(first))
                                {
                                    isAllTheSame = false;
                                }
                            }
                            if (isAllTheSame)
                            {
                                NbNotOk.Add(i);
                                isAdd = true;
                            }

                        }
                    }
                }
            }
            return NbNotOk.Sum();
        }

    }
}