using AdventOfCode2025;

namespace AdventOfCode2025
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();

            var day1 = new Day1();
            var day2 = new Day2();
            var day3 = new Day3();
            var day4 = new Day4();
            var day5 = new Day5();
            var day6 = new Day6();
            var day7 = new Day7();
            var day8 = new Day8();
            var day9 = new Day9();
            var day10 = new Day10();
            var day10Z3 = new Day10Part2Z3();
            var day10NoNuget = new Day10Part2NoNuget();
            var day11 = new Day11();
            var day12 = new Day12();
            program.RunPart("Day 1", "Part 1", () => day1.SolveOne());
            program.RunPart("Day 1", "Part 2", () => day1.SolveTwo());

            program.RunPart("Day 2", "Part 1", () => day2.SolveOne());
            program.RunPart("Day 2", "Part 2", () => day2.SolveTwo());

            program.RunPart("Day 3", "Part 1", () => day3.SolveOne());
            program.RunPart("Day 3", "Part 2", () => day3.SolveTwo());

            program.RunPart("Day 4", "Part 1", () => day4.SolveOne());
            program.RunPart("Day 4", "Part 2", () => day4.SolveTwo());

            program.RunPart("Day 5", "Part 1", () => day5.SolveOne());
            program.RunPart("Day 5", "Part 2", () => day5.SolveTwo());

            program.RunPart("Day 6", "Part 1", () => day6.SolveOne());
            program.RunPart("Day 6", "Part 2", () => day6.SolveTwo());

            program.RunPart("Day 7", "Part 1", () => day7.SolveOne());
            program.RunPart("Day 7", "Part 2", () => day7.SolveTwo());

            program.RunPart("Day 8", "Part 1", () => day8.SolveOne());
            program.RunPart("Day 8", "Part 2", () => day8.SolveTwo());

            program.RunPart("Day 9", "Part 1", () => day9.SolveOne());
            program.RunPart("Day 9", "Part 2", () => day9.SolveTwo());

            program.RunPart("Day 10", "Part 1", () => day10.SolveOne());
            program.RunPart("Day 10", "Part 2 Z3", () => day10Z3.Solve());
            program.RunPart("Day 10", "Part 2 Made by Hand", () => day10NoNuget.Solve());

            program.RunPart("Day 11", "Part 1", () => day11.SolveOne());
            program.RunPart("Day 11", "Part 2", () => day11.SolveTwo());

            program.RunPart("Day 12", "Part 1", () => day12.SolveOne());

            Console.ReadKey();
        }
        void RunPart(string day, string part, Func<object?> action)
        {
            Console.WriteLine("+--------------------------------------------+");
            Console.WriteLine($"| {day} - {part} ");
            Console.WriteLine("+--------------------------------------------+");

            var start = DateTime.Now;
            object? result = action();
            var duration = DateTime.Now - start;

            // Format duration
            string formattedTime;
            if (duration.TotalSeconds < 1)
                formattedTime = $"{duration.TotalMilliseconds:F2} ms";
            else if (duration.TotalMinutes < 1)
                formattedTime = $"{duration.TotalSeconds:F2} s";
            else
                formattedTime = $"{duration.TotalMinutes:F2} min";
            formattedTime = $"{duration.TotalMilliseconds:F2} ms";

            Console.WriteLine($"Duration       : {formattedTime}");
            Console.WriteLine($"Result         : {result}");
            Console.WriteLine();
        }

    }
}
