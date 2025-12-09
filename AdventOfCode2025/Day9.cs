using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day9.txt");
            string[] rows = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);
            List<int[]> flore = rows.Select(t => t.Split(",").Select(c => int.Parse(c)).ToArray()).ToList();
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

            char[][] pavement = new char[maxX + 2][];
            for (int i = 0; i <= maxX + 1; i++)
            {
                pavement[i] = new char[maxY + 2];
            }

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
            
            List<(int, int, int, int, long)> AllRectengle = new List<(int, int, int, int, long)>();
            foreach (var tile1 in flore)
            {
                foreach (var tile2 in flore)
                {
                    var lenght = Math.Abs(tile2[0] - tile1[0]) + 1;
                    var height = Math.Abs(tile2[1] - tile1[1]) + 1;
                    var size = lenght * height;
                    AllRectengle.Add((tile1[0], tile1[1], tile2[0], tile2[1], size));
                }
            }

            AllRectengle = AllRectengle.Distinct().OrderByDescending(x => x.Item5).ToList();

            foreach (var item in AllRectengle)
            {
                if (IsAValidRectangle((item.Item1, item.Item2), (item.Item3, item.Item4), pavement))
                {
                    Console.WriteLine(item.Item1 + " " + item.Item2 + " " + item.Item3 + " " + item.Item4);
                    Console.WriteLine("the bigest one it of size " + item.Item5);
                    break;
                }
            }

        }

        public static bool IsAValidRectangle((int x, int y) point1, (int x, int y) point2, char[][] table)
        {
            return DontContaineARedTile(point1, point2, table) && IsAValidRectangleForPointOnTop(point1, point2, table) && IsAValidRectangleForPointOnBottom(point1, point2, table);
        }
        public static bool DontContaineARedTile((int x, int y) point1, (int x, int y) point2, char[][] table)
        {
            return true;
        }
        public static bool IsAValidRectangleForPointOnTop((int x, int y) point1, (int x, int y) point2, char[][] table)
        {

            return IsAValidRectangleForX(point1, point2, table) && IsAValidRectangleForY(point1, point2, table);
        }
        public static bool IsAValidRectangleForPointOnBottom((int x, int y) point1, (int x, int y) point2, char[][] table)
        {
            return IsAValidRectangleForX(point2, point1, table) && IsAValidRectangleForY(point2, point1, table);
        }
        public static bool IsAValidRectangleForY((int x, int y) point1, (int x, int y) point2, char[][] table)
        {
            bool shouldIMoveLeftToRight = point1.y < point2.y;
            bool theRectengleIsOver = point1.x < point2.x;
            char lastSimbol = table[point1.x][point1.y];
            int startPosY = point1.y;
            int limit = point2.y;

            int i = startPosY;
            bool isFirstTime = true;

            while ((shouldIMoveLeftToRight) ? i < limit : i > limit)
            {
                if (isFirstTime)
                {
                    isFirstTime = false;
                }
                else
                {
                    char currentChar = table[point1.x][i];
                    if (lastSimbol == '\0')
                    {
                        if (currentChar == 'V')
                        {
                            //on repasse devant nous donc on sort de la limite
                            return false;
                        }
                    }
                    if (lastSimbol == 'V')
                    {
                        if (currentChar == '\0')
                        {
                            return false;
                        }
                    }
                    if (currentChar == 'R')
                    {
                        if (theRectengleIsOver && table[point1.x + 1][i] != '\0')
                        {
                            //on revient a notre hauteur mais on vient de enbas
                            return false;
                        }
                        if (!theRectengleIsOver && table[point1.x + 1][i] == '\0')
                        {
                            //on revient a notre hauteur mais on vient d'en haut
                            return false;
                        }
                    }
                    lastSimbol = currentChar;
                }


                if (shouldIMoveLeftToRight)
                {
                    i++;
                }
                else
                {
                    i--;
                }
            }
            do
            {
                char currentChar = table[point1.x][i];

                if (currentChar == 'R')
                {
                    return true;
                }
                if (currentChar == 'V')
                {
                    return true;
                }

                if (shouldIMoveLeftToRight)
                {
                    i++;
                }
                else
                {
                    i--;
                }
            } while ((shouldIMoveLeftToRight) ? i < table[0].Length : i >= 0);
            return false;

        }
        public static bool IsAValidRectangleForX((int x, int y) point1, (int x, int y) point2, char[][] table)
        {
            bool shouldIMoveFromTopToBottom = point1.x < point2.x;
            bool theRectengleIsOnTheRight = point1.y < point2.y;
            char lastSimbol = table[point1.x][point1.y];
            int startPosX = point1.x;
            int limit = point2.x;

            int i = startPosX;
            bool isFirstTime = true;

            while ((shouldIMoveFromTopToBottom) ? i < limit : i > limit)
            {
                if (isFirstTime)
                {
                    isFirstTime = false;
                }
                else
                {
                    char currentChar = table[i][point1.y];
                    if (lastSimbol == '\0')
                    {
                        if (currentChar == 'V')
                        {
                            //on repasse devant nous donc on sort de la limite
                            return false;
                        }
                    }
                    if (lastSimbol == 'V')
                    {
                        if (currentChar == '\0')
                        {
                            return false;
                        }
                    }
                    if (currentChar == 'R')
                    {
                        if (theRectengleIsOnTheRight && table[i][point1.y + 1] != '\0')
                        {
                            //on revient a notre hauteur mais on vient de la droite
                            return false;
                        }
                        if (!theRectengleIsOnTheRight && table[i][point1.y + 1] == '\0')
                        {
                            //on revient a notre hauteur mais on vient de la gauche
                            return false;
                        }
                    }
                    lastSimbol = currentChar;
                }

                if (shouldIMoveFromTopToBottom)
                {
                    i++;
                }
                else
                {
                    i--;
                }
            }
            do
            {
                char currentChar = table[i][point1.y];

                if (currentChar == 'R')
                {
                    return true;
                }
                if (currentChar == 'V')
                {
                    return true;
                }

                if (shouldIMoveFromTopToBottom)
                {
                    i++;
                }
                else
                {
                    i--;
                }
            } while ((shouldIMoveFromTopToBottom) ? i < table.Length : i >= 0);
            return false;
        }

    }
}