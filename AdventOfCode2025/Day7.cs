using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode2025
{
    public static class Day7
    {
        public static Int128 SolveOne()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day7.txt");
            string[] row = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);
            var inputs = row.Select(t => t.ToCharArray()).ToArray();

            int total = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[0].Length; j++)
                {
                    if (inputs[i][j] == 'S')
                    {
                        inputs[i + 1][j] = '|';
                    }
                    if (inputs[i][j] == '^' && inputs[i - 1][j] == '|')
                    {
                        inputs[i][j + 1] = '|';
                        inputs[i][j - 1] = '|';
                        inputs[i + 1][j + 1] = '|';
                        inputs[i + 1][j - 1] = '|';
                        total++;
                    }
                    if (inputs[i][j] == '|' && i < inputs.Length - 1 && inputs[i + 1][j] != '^')
                    {
                        inputs[i + 1][j] = '|';
                    }
                    if (inputs[i][j] == ' ' && inputs[i - 1][j] == '|')
                    {
                        inputs[i + 1][j] = '|';
                    }
                }
            }

            return total;


        }


        public static Int128 SolveTwo()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day7.txt");
            string[] row = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);
            var inputs = row.Select(t => t.ToCharArray()).ToArray();
            long[,] currentAtPos = new long[inputs.Length, inputs[0].Length];

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[0].Length; j++)
                {
                    if (inputs[i][j] == 'S')
                    {
                        inputs[i + 1][j] = '|';
                        currentAtPos[i + 1, j]++;
                    }
                    if (inputs[i][j] == '^' && inputs[i - 1][j] == '|')
                    {
                        inputs[i][j + 1] = '|';
                        inputs[i][j - 1] = '|';
                        currentAtPos[i, j + 1] += currentAtPos[i-1, j];
                        currentAtPos[i, j - 1] += currentAtPos[i-1, j];
                    }
                    if (inputs[i][j] == '.' && i > 0 && inputs[i - 1][j] == '|')
                    {
                        inputs[i][j] = '|';
                        currentAtPos[i, j] += currentAtPos[i - 1, j];
                    }
                    else if (inputs[i][j] == '|' && i < inputs.Length - 1 && inputs[i -1][j] == '|')
                    {
                        currentAtPos[i, j] += currentAtPos[i-1, j];
                    }
                }
            }
            ulong total = 0;
            for (int i = 0; i < currentAtPos.GetLength(1); i++)
            {
                total = (ulong)currentAtPos[currentAtPos.GetLength(0) - 2, i] + total;
            }
            

            return total;


        }
    }
}


