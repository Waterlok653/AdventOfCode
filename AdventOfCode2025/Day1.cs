using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2025
{
    public static class Day1
    {
        public static Int128 SolveOne()
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
            return total0;
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
        public static Int128 SolveTwo()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day1.txt");
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);
            int position = 50;
            int total0 = 0;
            foreach (var line in inputs.Where(s => s.Length != 0).ToArray())
            {
                var result = RotateTwo(line, position);
                //Console.WriteLine(line + " Old Position " + position + " new positioon " + result.Item1 + " Passe by 0 " + result.Item2);
                position = result.Item1;
                total0 += result.Item2;
            }
            return total0;
        }
        public static (int, int) RotateTwo(string line, int position)
        {
            int initialPosition = position;
            char direction = line[0];
            int distance = int.Parse(line.Substring(1));
            switch (direction)
            {
                case 'R':
                    position += distance;
                    if (position >= 100)
                    {
                        var newPos = (position % 100 + 100) % 100;
                        var timeByZero = position / 100;
                        return (newPos, timeByZero);
                    }
                    break;
                case 'L':
                    position -= distance;
                    if (position <= 0)
                    {
                        var newPos = (position % 100 + 100) % 100;
                        var timeByZero = 1 + (-position) / 100;
                        if (initialPosition == 0)
                        {
                            timeByZero--;
                        }
                        return (newPos, timeByZero);
                    }
                    break;
            }
            return ((position % 100 + 100) % 100, 0);
        }
    }
}
