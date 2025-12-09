using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2025
{
    public static class Day9
    {
        public static void SolveOne()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day9.txt");
            string[] rows = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);
            long[][] flore = rows.Select(t => t.Split(",").Select(c => long.Parse(c)).ToArray()).ToArray();

            long maxSize = 0;
            for (long i = 0; i < flore.Length; i++)
            {
                for (long k = 0; k < flore.Length; k++)
                {
                    var lenght = (flore[k][0] - flore[i][0] + 1);
                    var height = (flore[k][1] - flore[i][1] + 1);
                    var size = lenght * height;
                    if (size > maxSize)
                    {
                        maxSize = size;
                    }
                }

            }
            Console.WriteLine("the bigest one it of size " + maxSize);
        }


        public static void SolveTwo()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day9.test.txt");
            string[] rows = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);
            List<long[]> flore = rows.Select(t => t.Split(",").Select(c => long.Parse(c)).ToArray()).ToList();
            long maxX = 0;
            long maxY = 0;
            for (int i = 0; i < flore.Count; i++)
            {
                if (flore[i][0] > maxX)
                {
                    maxX = flore[i][0];
                }
                if (flore[i][1] > maxY)
                {
                    maxY = flore[i][1];
                }
            }

            char[][] pavement = new char[maxX + 1][];
            for (int i = 0; i <= maxX; i++)
            {
                pavement[i] = new char[maxY + 1];
            }


            //for (int i = 0; i < pavement.Length; i++)
            //{
            //    for (int j = 0; j < pavement[0].Length; j++)
            //    {
            //        pavement[i][j] = '.';
            //    }
            //}

            long lastX = 0;
            long lastY = 0;
            bool isFirst = true;
            flore.Add(flore.First());
            foreach (var tile in flore)
            {
                if (isFirst)
                {
                    lastX = tile[0];
                    lastY = tile[1];
                    isFirst = false;
                    pavement[lastX][lastY] = 'R';

                }
                else
                {
                    var newX = tile[0];
                    var newY = tile[1];
                    if (lastY == newY)
                    {
                        if (lastX < newX)
                        {
                            for (long i = lastX + 1; i < newX; i++)
                            {
                                pavement[i][lastY] = 'V';
                            }
                        }
                        else
                        {
                            for (long i = newX + 1; i < lastX; i++)
                            {
                                pavement[i][lastY] = 'V';
                            }
                        }
                    }
                    else if (lastX == newX)
                    {

                        if (lastY < newY)
                        {
                            for (long i = lastY + 1; i < newY; i++)
                            {
                                pavement[lastX][i] = 'V';
                            }
                        }
                        else
                        {
                            for (long i = newY + 1; i < lastY; i++)
                            {
                                pavement[lastX][i] = 'V';
                            }
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                    lastX = newX;
                    lastY = newY;
                    pavement[lastX][lastY] = 'R';

                }
            }

            //drawInside
            /*bool isFinish = true;
            pavement[pavement.Length - 2][pavement[0].Length - 2] = 'I';
            do
            {
                isFinish = true;
                for (int i = pavement.Length - 1; i >= 0; i--)
                {
                    for (int j = pavement[0].Length - 1; j >= 0; j--)
                    {
                        if (pavement[i][j].Equals("."))
                        {
                            if (i != 0)
                            {
                                if (j != 0 && pavement[i - 1][j - 1].Equals('I'))
                                {
                                    pavement[i][j] = 'I';
                                    isFinish = false;
                                    continue;
                                }
                                if (pavement[i - 1][j].Equals('I'))
                                {
                                    pavement[i][j] = 'I';
                                    isFinish = false;
                                    continue;
                                }
                                if (j != pavement[0].Length - 1 && pavement[i - 1][j + 1].Equals('I'))
                                {
                                    pavement[i][j] = 'I';
                                    isFinish = false;
                                    continue;
                                }
                            }
                            if (j != 0 && pavement[i][j - 1].Equals('I'))
                            {
                                pavement[i][j] = 'I';
                                isFinish = false;
                                continue;
                            }
                            if (j != pavement[0].Length - 1 && pavement[i][j + 1].Equals('I'))
                            {
                                pavement[i][j] = 'I';
                                isFinish = false;
                                continue;
                            }
                            if (i != pavement.Length - 1)
                            {
                                if (j != 0 && pavement[i + 1][j - 1].Equals('I'))
                                {
                                    pavement[i][j] = 'I';
                                    isFinish = false;
                                    continue;
                                }
                                if (pavement[i + 1][j].Equals('I'))
                                {
                                    pavement[i][j] = 'I';
                                    isFinish = false;
                                    continue;
                                }
                                if (j != pavement[0].Length - 1 && pavement[i + 1][j + 1].Equals('I'))
                                {
                                    pavement[i][j] = 'I';
                                    isFinish = false;
                                    continue;
                                }
                            }
                        }
                    }
                }

            } while (!isFinish);*/



            long maxSize = 0;
            for (int i = 0; i < pavement.Length; i++)
            {
                for (int j = 0; j < pavement[0].Length; j++)
                {
                    if (pavement[i][j]=='R')
                    {
                        for (int m = i + 1; m < pavement.Length; m++)
                        {
                            for (int n = j + 1; n < pavement[0].Length; n++)
                            {
                                if (!(pavement[m][n]=='R'))
                                {
                                    continue;
                                }
                                var lenght = (m - i + 1);
                                var height = (n - j + 1);
                                var size = lenght * height;
                                if (size > maxSize)
                                {
                                    bool isCorrect = true;
                                    for (long k = ((i < m) ? i : m) + 1; k < ((i > m) ? i : m); k++)
                                    {
                                        for (long l = ((j < n) ? j : n) + 1; l < ((j > n) ? j : n); l++)
                                        {
                                            if (pavement[k][l]=='R')
                                            {
                                                isCorrect = false;
                                                break;
                                            }
                                        }
                                        if (!isCorrect)
                                        {
                                            break;
                                        }
                                    }
                                    if (isCorrect)
                                    {
                                        isCorrect = false;
                                        if (lenght - 1 > 0)
                                        {
                                            lenght = 1;
                                        }
                                        else if (lenght - 1 < 0)
                                        {
                                            lenght = -1;
                                        }
                                        else
                                        {
                                            lenght = 0;
                                        }

                                        if (height - 1 > 0)
                                        {
                                            height = 1;
                                        }
                                        else if (height - 1 < 0)
                                        {
                                            height = -1;
                                        }
                                        else
                                        {
                                            height = 0;
                                        }

                                        for (long l = ((j < n) ? j : n); ((height == 1) ? l < pavement[0].Length : l >= 0); l += height)
                                        {
                                            if (pavement[i][l]=='V')
                                            {
                                                isCorrect = true;
                                                break;
                                            }
                                        }
                                        if (isCorrect)
                                        {
                                            isCorrect = false;

                                            for (long k = ((i < m) ? i : m); ((lenght == 1) ? k < pavement.Length : k >= 0); k += lenght)
                                            {
                                                if (pavement[k][j]=='V')
                                                {
                                                    isCorrect = true;
                                                    break;
                                                }
                                            }
                                            if (isCorrect)
                                            {
                                                isCorrect=false;
                                                for (long l = ((j > n) ? j : n); ((height == -1) ? l < pavement[0].Length : l >= 0); l -= height)
                                                {
                                                    if (pavement[m][l]=='V')
                                                    {
                                                        isCorrect = true;
                                                        break;
                                                    }
                                                }
                                                if (isCorrect)
                                                {
                                                    isCorrect = false;

                                                    for (long k = ((i > m) ? i : m); ((lenght == -1) ? k < pavement.Length : k >= 0); k -= lenght)
                                                    {
                                                        if (pavement[k][n]=='V')
                                                        {
                                                            isCorrect = true;
                                                            break;
                                                        }
                                                    }
                                                    if (isCorrect)
                                                    {
                                                        maxSize = size;
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }
                            }


                        }
                    }
                }
            }

            Console.WriteLine("the bigest one it of size " + maxSize);
        }

    }
}