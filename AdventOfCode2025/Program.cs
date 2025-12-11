using AdventOfCode2025;

namespace AdventOfCode2025
{
    internal class Program
    {
        static long baselineMemory;
        static void Main(string[] args)
        {
            baselineMemory = GC.GetTotalMemory(true);
            RunPart("Day 10", "Part 2", () => Day10Part2NoNuget.Solve());

            RunPart("Day 1", "Part 1", () => Day1.SolveOne());
            RunPart("Day 1", "Part 2", () => Day1.SolveTwo());

            RunPart("Day 2", "Part 1", () => Day2.SolveOne());
            RunPart("Day 2", "Part 2", () => Day2.SolveTwo());

            RunPart("Day 3", "Part 1", () => Day3.SolveOne());
            RunPart("Day 3", "Part 2", () => Day3.SolveTwo());

            RunPart("Day 4", "Part 1", () => Day4.SolveOne());
            RunPart("Day 4", "Part 2", () => Day4.SolveTwo());

            RunPart("Day 5", "Part 1", () => Day5.SolveOne());
            RunPart("Day 5", "Part 2", () => Day5.SolveTwo());

            RunPart("Day 6", "Part 1", () => Day6.SolveOne());
            RunPart("Day 6", "Part 2", () => Day6.SolveTwo());

            RunPart("Day 7", "Part 1", () => Day7.SolveOne());
            RunPart("Day 7", "Part 2", () => Day7.SolveTwo());

            RunPart("Day 8", "Part 1", () => Day8.SolveOne());
            RunPart("Day 8", "Part 2", () => Day8.SolveTwo());

            RunPart("Day 9", "Part 1", () => Day9.SolveOne());
            RunPart("Day 9", "Part 2", () => Day9.SolveTwo());

            RunPart("Day 10", "Part 1", () => Day10.SolveOne());
            RunPart("Day 10", "Part 2", () => Day10Part2Z3.Solve());

            RunPart("Day 11", "Part 1", () => Day11.SolveOne());
            RunPart("Day 11", "Part 2", () => Day11.SolveTwo());
        }
        static void RunPart(string day, string part, Func<object?> action)
        {
            Console.WriteLine("+--------------------------------------------+");
            Console.WriteLine($"| {day} - {part} ");
            Console.WriteLine("+--------------------------------------------+");

            long memoryBefore = GC.GetTotalMemory(true);

            var start = DateTime.Now;
            object? result = action();
            var duration = DateTime.Now - start;

            long memoryAfter = GC.GetTotalMemory(false);
            long ramUsed = memoryAfter - memoryBefore;          // RAM used by this action
            long ramUsedFromStart = memoryAfter - baselineMemory; // RAM used from program start

            // Format duration
            string formattedTime;
            if (duration.TotalSeconds < 1)
                formattedTime = $"{duration.TotalMilliseconds:F2} ms";
            else if (duration.TotalMinutes < 1)
                formattedTime = $"{duration.TotalSeconds:F2} s";
            else
                formattedTime = $"{duration.TotalMinutes:F2} min";

            // Format RAM usage
            string FormatBytes(long bytes)
            {
                if (bytes < 1024) return $"{bytes} B";
                if (bytes < 1024L * 1024) return $"{bytes / 1024.0:F2} KB";
                if (bytes < 1024L * 1024 * 1024) return $"{bytes / (1024.0 * 1024):F2} MB";
                return $"{bytes / (1024.0 * 1024 * 1024):F2} GB";
            }

            Console.WriteLine($"Duration       : {formattedTime}");
            Console.WriteLine($"RAM Used Part  : {FormatBytes(ramUsed)}");
            Console.WriteLine($"RAM Used Total : {FormatBytes(ramUsedFromStart)}");
            Console.WriteLine($"Result         : {result}");
            Console.WriteLine();
        }

    }
}
