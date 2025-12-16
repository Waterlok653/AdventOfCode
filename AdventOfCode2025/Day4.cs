using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2025
{
    public class Day4
    {
        public Int128 SolveOne()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day4.txt");
            string[] inputs1D = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);
            char[,] inputs2D = new char[inputs1D.Length, inputs1D[0].Length];

            int pos = 0;
            foreach (var item in inputs1D)
            {
                int posy = 0;
                foreach (var item2 in item.ToCharArray())
                {

                    inputs2D[pos, posy++] = item2;
                }
                pos++;
            }
            int canAccsess = 0;
            for (int i = 0; i < inputs2D.GetLength(0); i++)
            {
                for (int j = 0; j < inputs2D.GetLength(1); j++)
                {
                    if (inputs2D[i, j] != '@')
                    {
                        continue;
                    }
                    int total = 0;
                    if (i != 0)
                    {
                        if (j != 0 && inputs2D[i - 1, j - 1] == '@')
                        {
                            total++;
                        }
                        if (inputs2D[i - 1, j] == '@')
                        {
                            total++;
                        }
                        if (j != inputs2D.GetLength(1) - 1 && inputs2D[i - 1, j + 1] == '@')
                        {
                            total++;
                        }
                    }
                    if (j != 0 && inputs2D[i, j - 1] == '@')
                    {
                        total++;
                    }
                    if (j != inputs2D.GetLength(1) - 1 && inputs2D[i, j + 1] == '@')
                    {
                        total++;
                    }
                    if (i != inputs2D.GetLength(0) - 1)
                    {
                        if (j != 0 && inputs2D[i + 1, j - 1] == '@')
                        {
                            total++;
                        }
                        if (inputs2D[i + 1, j] == '@')
                        {
                            total++;
                        }
                        if (j != inputs2D.GetLength(1) - 1 && inputs2D[i + 1, j + 1] == '@')
                        {
                            total++;
                        }
                    }
                    if (total < 4)
                    {
                        canAccsess++;
                    }

                }

            }
            return canAccsess;

        }


        public Int128 SolveTwo()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day4.txt");
            string[] inputs1D = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);
            char[,] inputs2D = new char[inputs1D.Length, inputs1D[0].Length];

            int pos = 0;
            foreach (var item in inputs1D)
            {
                int posy = 0;
                foreach (var item2 in item.ToCharArray())
                {

                    inputs2D[pos, posy++] = item2;
                }
                pos++;
            }
            int canAccsess = 0;
            bool didRemove;
            do
            {
                didRemove = false;
                for (int i = 0; i < inputs2D.GetLength(0); i++)
                {
                    for (int j = 0; j < inputs2D.GetLength(1); j++)
                    {
                        if (inputs2D[i, j] != '@')
                        {
                            continue;
                        }
                        int total = 0;
                        if (i != 0)
                        {
                            if (j != 0 && inputs2D[i - 1, j - 1] == '@')
                            {
                                total++;
                            }
                            if (inputs2D[i - 1, j] == '@')
                            {
                                total++;
                            }
                            if (j != inputs2D.GetLength(1) - 1 && inputs2D[i - 1, j + 1] == '@')
                            {
                                total++;
                            }
                        }
                        if (j != 0 && inputs2D[i, j - 1] == '@')
                        {
                            total++;
                        }
                        if (j != inputs2D.GetLength(1) - 1 && inputs2D[i, j + 1] == '@')
                        {
                            total++;
                        }
                        if (i != inputs2D.GetLength(0) - 1)
                        {
                            if (j != 0 && inputs2D[i + 1, j - 1] == '@')
                            {
                                total++;
                            }
                            if (inputs2D[i + 1, j] == '@')
                            {
                                total++;
                            }
                            if (j != inputs2D.GetLength(1) - 1 && inputs2D[i + 1, j + 1] == '@')
                            {
                                total++;
                            }
                        }
                        if (total < 4)
                        {
                            canAccsess++;
                            inputs2D[i, j] = ' ';
                            didRemove = true;
                        }

                    }

                }

            } while (didRemove);
            return canAccsess;

        }

    }
}