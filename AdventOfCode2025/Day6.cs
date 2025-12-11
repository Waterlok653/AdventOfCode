using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2025
{
    public static class Day6
    {
        public static Int128 SolveOne()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day6.txt");
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);
            string[] cleanInputs = new string[inputs.Length];
            List<List<string>> inputs2D = new List<List<string>>();

            int posY = 0;
            foreach (string input1 in inputs)
            {
                var clean = input1.Trim();
                bool didClean = false;
                do
                {
                    didClean = false;
                    if (clean.Contains("  "))
                    {
                        didClean = true;
                        clean = clean.Replace("  ", " ");
                    }

                } while (didClean);

                cleanInputs[posY++] = clean;
            }
            string[][] clean2D = cleanInputs.Select(i => i.Split(' ')).ToArray();

            long sum = 0;

            for (int i = 0; i < clean2D[0].Length; i++)
            {
                long result = 0;
                string stringOperator = clean2D[clean2D.Length - 1][i];
                for (int j = 0; j < clean2D.Length - 1; j++)
                {
                    if (stringOperator.Equals("+"))
                    {
                        result += int.Parse(clean2D[j][i]);
                    }
                    else
                    {
                        if (result == 0)
                        {
                            result = 1;
                        }
                        result *= int.Parse(clean2D[j][i]);
                    }
                }
                sum += result;
            }
            return sum;
        }


        public static Int128 SolveTwo()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day6.txt");
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);
            var cleanInputs = inputs.Select(t => t.ToCharArray()).ToArray();

            var correctNumber = new List<string>();

            for (int i = 0; i < cleanInputs[0].Length; i++)
            {
                var number = "";
                for (int j = 0; j < cleanInputs.Length - 1; j++)
                {
                    number += cleanInputs[j][i];
                }
                correctNumber.Add(number);
            }
            List<string> mathOperator = new List<string>();


            for (int i = 0; i < cleanInputs[cleanInputs.Length - 1].Length; i++)
            {
                mathOperator.Add(cleanInputs[cleanInputs.Length - 1][i].ToString());
            }
            bool didRm = false;
            do
            {
                didRm = false;
                if (mathOperator.Where(g => g.Trim().Equals("")).Count() != 0)
                {
                    mathOperator.Remove(mathOperator.Where(g => g.Trim().Equals("")).First());
                    didRm = true;
                }

            } while (didRm);

            int nbClac = 0;
            long sum = 0;
            long result = 0;
            foreach (var number in correctNumber)
            {
                if (string.IsNullOrWhiteSpace(number))
                {
                    sum += result;
                    nbClac++;
                    result = 0;
                    continue;
                }
                if (mathOperator[nbClac].Equals("*"))
                {
                    if (result == 0)
                    {
                        result = 1;
                    }
                    result *= int.Parse(number);
                }
                else
                {
                    result += int.Parse(number);
                }
            }
            sum += result;

            return sum;
        }
    }
}


