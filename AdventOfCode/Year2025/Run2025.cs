using System;

namespace AdventOfCode.Year2025
{
    public static class Run2025
    {
        // ===== ENTRY POINT =====
        public static void Run(string dayInput, string partInput)
        {
            if (dayInput == "A")
            {
                for (int day = 1; day <= 12; day++)
                    RunDay(day, partInput);
            }
            else
            {
                RunDay(int.Parse(dayInput), partInput);
            }
        }

        // ===== DAY DISPATCHER =====
        private static void RunDay(int day, string partInput)
        {
            if (partInput == "A")
            {
                RunPart(day, 1);
                RunPart(day, 2);
            }
            else
            {
                RunPart(day, int.Parse(partInput));
            }
        }

        // ===== PART DISPATCHER =====
        private static void RunPart(int day, int part)
        {
            switch (day)
            {
                case 1: if (part == 1) RunD1P1(); else RunD1P2(); break;
                case 2: if (part == 1) RunD2P1(); else RunD2P2(); break;
                case 3: if (part == 1) RunD3P1(); else RunD3P2(); break;
                case 4: if (part == 1) RunD4P1(); else RunD4P2(); break;
                case 5: if (part == 1) RunD5P1(); else RunD5P2(); break;
                case 6: if (part == 1) RunD6P1(); else RunD6P2(); break;
                case 7: if (part == 1) RunD7P1(); else RunD7P2(); break;
                case 8: if (part == 1) RunD8P1(); else RunD8P2(); break;
                case 9: if (part == 1) RunD9P1(); else RunD9P2(); break;
                case 10: if (part == 1) RunD10P1(); else RunD10P2(); break;
                case 11: if (part == 1) RunD11P1(); else RunD11P2(); break;
                case 12: if (part == 1) RunD12P1(); break;

            }
        }

        // ===== EXISTING METHODS (UNCHANGED) =====

        public static void RunD1P1()
        {
            var day1 = new Day1();
            RunDayStatistique.RunPart("Day 1", "Part 1", () => day1.SolveOne());
        }
        public static void RunD1P2()
        {
            var day1 = new Day1();
            RunDayStatistique.RunPart("Day 1", "Part 2", () => day1.SolveTwo());
        }

        public static void RunD2P1()
        {
            var day2 = new Day2();
            RunDayStatistique.RunPart("Day 2", "Part 1", () => day2.SolveOne());
        }
        public static void RunD2P2()
        {
            var day2 = new Day2();
            RunDayStatistique.RunPart("Day 2", "Part 2", () => day2.SolveTwo());
        }

        public static void RunD3P1()
        {
            var day3 = new Day3();
            RunDayStatistique.RunPart("Day 3", "Part 1", () => day3.SolveOne());
        }
        public static void RunD3P2()
        {
            var day3 = new Day3();
            RunDayStatistique.RunPart("Day 3", "Part 2", () => day3.SolveTwo());
        }

        public static void RunD4P1()
        {
            var day4 = new Day4();
            RunDayStatistique.RunPart("Day 4", "Part 1", () => day4.SolveOne());
        }
        public static void RunD4P2()
        {
            var day4 = new Day4();
            RunDayStatistique.RunPart("Day 4", "Part 2", () => day4.SolveTwo());
        }

        public static void RunD5P1()
        {
            var day5 = new Day5();
            RunDayStatistique.RunPart("Day 5", "Part 1", () => day5.SolveOne());
        }
        public static void RunD5P2()
        {
            var day5 = new Day5();
            RunDayStatistique.RunPart("Day 5", "Part 2", () => day5.SolveTwo());
        }

        public static void RunD6P1()
        {
            var day6 = new Day6();
            RunDayStatistique.RunPart("Day 6", "Part 1", () => day6.SolveOne());
        }
        public static void RunD6P2()
        {
            var day6 = new Day6();
            RunDayStatistique.RunPart("Day 6", "Part 2", () => day6.SolveTwo());
        }

        public static void RunD7P1()
        {
            var day7 = new Day7();
            RunDayStatistique.RunPart("Day 7", "Part 1", () => day7.SolveOne());
        }
        public static void RunD7P2()
        {
            var day7 = new Day7();
            RunDayStatistique.RunPart("Day 7", "Part 2", () => day7.SolveTwo());
        }

        public static void RunD8P1()
        {
            var day8 = new Day8();
            RunDayStatistique.RunPart("Day 8", "Part 1", () => day8.SolveOne());
        }
        public static void RunD8P2()
        {
            var day8 = new Day8();
            RunDayStatistique.RunPart("Day 8", "Part 2", () => day8.SolveTwo());
        }

        public static void RunD9P1()
        {
            var day9 = new Day9();
            RunDayStatistique.RunPart("Day 9", "Part 1", () => day9.SolveOne());
        }
        public static void RunD9P2()
        {
            var day9 = new Day9();
            RunDayStatistique.RunPart("Day 9", "Part 2", () => day9.SolveTwo());
        }

        public static void RunD10P1()
        {
            var day10 = new Day10();
            RunDayStatistique.RunPart("Day 10", "Part 1", () => day10.SolveOne());
        }
        public static void RunD10P2()
        {
            var day10 = new Day10Part2NoNuget();
            RunDayStatistique.RunPart("Day 10", "Part 2", () => day10.Solve());
        }

        public static void RunD11P1()
        {
            var day11 = new Day11();
            RunDayStatistique.RunPart("Day 11", "Part 1", () => day11.SolveOne());
        }
        public static void RunD11P2()
        {
            var day11 = new Day11();
            RunDayStatistique.RunPart("Day 11", "Part 2", () => day11.SolveTwo());
        }

        public static void RunD12P1()
        {
            var day12 = new Day12();
            RunDayStatistique.RunPart("Day 12", "Part 1", () => day12.SolveOne());
        }
    }
}
