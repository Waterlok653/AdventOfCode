using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2025
{
    public static class Day1
    {
        public static void SolveOne()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day1.txt");
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);
            int position = 50;
            int total0 = 0;
            foreach (var line in inputs.Where(s => s.Length != 0).ToArray())
            {
                position = RotateOne(line, position);
                if (position == 0)
                {
                    total0++;
                }
            }
            Console.WriteLine($"Total times at 0: {total0}");
        }
        public static int RotateOne(string line, int position)
        {
            char direction = line[0];
            int distance = int.Parse(line.Substring(1));
            switch (direction)
            {
                case 'R':
                    position += distance;
                    break;
                case 'L':
                    position -= distance;
                    break;
            }

            return (position % 100 + 100) % 100;

        }
        public static void SolveTwo()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day1.txt");
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);
            int position = 50;
            int total0 = 0;
            foreach (var line in inputs.Where(s => s.Length != 0).ToArray())
            {
                var result = RotateTwo(line, position);
                position = result.Item1;
                total0 += result.Item2;
            }
            Console.WriteLine($"Total times it went by 0: {total0}");
        }
        public static (int, int) RotateTwo(string line, int position)
        {
            char direction = line[0];
            int distance = int.Parse(line.Substring(1));
            switch (direction)
            {
                case 'R':
                    position += distance;
                    if (position >= 100)
                    {
                        return ((position % 100 + 100) % 100, position / 100);
                    }
                    break;
                case 'L':
                    position -= distance;
                    if (position < 0)
                    {
                        return ((position % 100 + 100) % 100, 1 + (-position - 1) / 100);
                    }
                    break;
            }
            return ((position % 100 + 100) % 100, 0);
        }
    }
}
