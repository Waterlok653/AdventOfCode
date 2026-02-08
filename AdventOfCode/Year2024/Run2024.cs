
namespace AdventOfCode.Year2024
{
    public static class Run2024
    {
        // ===== ENTRY POINT =====
        public static void Run(string dayInput, string partInput)
        {
            if (dayInput == "A")
            {
                for (int day = 1; day <= 25; day++)
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
                case 12: if (part == 1) RunD12P1(); else RunD12P2(); break;
                case 13: if (part == 1) RunD13P1(); else RunD13P2(); break;
                case 14: if (part == 1) RunD14P1(); else RunD14P2(); break;
                case 15: if (part == 1) RunD15P1(); else RunD15P2(); break;
                case 16: if (part == 1) RunD16P1(); else RunD16P2(); break;
                case 17: if (part == 1) RunD17P1(); else RunD17P2(); break;
                case 18: if (part == 1) RunD18P1(); else RunD18P2(); break;
                case 19: if (part == 1) RunD19P1(); else RunD19P2(); break;
                case 20: if (part == 1) RunD20P1(); else RunD20P2(); break;
                case 21: if (part == 1) RunD21P1(); else RunD21P2(); break;
                case 22: if (part == 1) RunD22P1(); else RunD22P2(); break;
                case 23: if (part == 1) RunD23P1(); else RunD23P2(); break;
                case 24: if (part == 1) RunD24P1(); else RunD24P2(); break;
                case 25: if (part == 1) RunD25P1(); else RunD25P2(); break;
                
            }
        }

        // ===== EXISTING METHODS (AUTO-GENERATED) =====
        
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
            var day10 = new Day10();
            RunDayStatistique.RunPart("Day 10", "Part 2", () => day10.SolveTwo());
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
        public static void RunD12P2()
        {
            var day12 = new Day12();
            RunDayStatistique.RunPart("Day 12", "Part 2", () => day12.SolveTwo());
        }

        public static void RunD13P1()
        {
            var day13 = new Day13();
            RunDayStatistique.RunPart("Day 13", "Part 1", () => day13.SolveOne());
        }
        public static void RunD13P2()
        {
            var day13 = new Day13();
            RunDayStatistique.RunPart("Day 13", "Part 2", () => day13.SolveTwo());
        }

        public static void RunD14P1()
        {
            var day14 = new Day14();
            RunDayStatistique.RunPart("Day 14", "Part 1", () => day14.SolveOne());
        }
        public static void RunD14P2()
        {
            var day14 = new Day14();
            RunDayStatistique.RunPart("Day 14", "Part 2", () => day14.SolveTwo());
        }

        public static void RunD15P1()
        {
            var day15 = new Day15();
            RunDayStatistique.RunPart("Day 15", "Part 1", () => day15.SolveOne());
        }
        public static void RunD15P2()
        {
            var day15 = new Day15();
            RunDayStatistique.RunPart("Day 15", "Part 2", () => day15.SolveTwo());
        }

        public static void RunD16P1()
        {
            var day16 = new Day16();
            RunDayStatistique.RunPart("Day 16", "Part 1", () => day16.SolveOne());
        }
        public static void RunD16P2()
        {
            var day16 = new Day16();
            RunDayStatistique.RunPart("Day 16", "Part 2", () => day16.SolveTwo());
        }

        public static void RunD17P1()
        {
            var day17 = new Day17();
            RunDayStatistique.RunPart("Day 17", "Part 1", () => day17.SolveOne());
        }
        public static void RunD17P2()
        {
            var day17 = new Day17();
            RunDayStatistique.RunPart("Day 17", "Part 2", () => day17.SolveTwo());
        }

        public static void RunD18P1()
        {
            var day18 = new Day18();
            RunDayStatistique.RunPart("Day 18", "Part 1", () => day18.SolveOne());
        }
        public static void RunD18P2()
        {
            var day18 = new Day18();
            RunDayStatistique.RunPart("Day 18", "Part 2", () => day18.SolveTwo());
        }

        public static void RunD19P1()
        {
            var day19 = new Day19();
            RunDayStatistique.RunPart("Day 19", "Part 1", () => day19.SolveOne());
        }
        public static void RunD19P2()
        {
            var day19 = new Day19();
            RunDayStatistique.RunPart("Day 19", "Part 2", () => day19.SolveTwo());
        }

        public static void RunD20P1()
        {
            var day20 = new Day20();
            RunDayStatistique.RunPart("Day 20", "Part 1", () => day20.SolveOne());
        }
        public static void RunD20P2()
        {
            var day20 = new Day20();
            RunDayStatistique.RunPart("Day 20", "Part 2", () => day20.SolveTwo());
        }

        public static void RunD21P1()
        {
            var day21 = new Day21();
            RunDayStatistique.RunPart("Day 21", "Part 1", () => day21.SolveOne());
        }
        public static void RunD21P2()
        {
            var day21 = new Day21();
            RunDayStatistique.RunPart("Day 21", "Part 2", () => day21.SolveTwo());
        }

        public static void RunD22P1()
        {
            var day22 = new Day22();
            RunDayStatistique.RunPart("Day 22", "Part 1", () => day22.SolveOne());
        }
        public static void RunD22P2()
        {
            var day22 = new Day22();
            RunDayStatistique.RunPart("Day 22", "Part 2", () => day22.SolveTwo());
        }

        public static void RunD23P1()
        {
            var day23 = new Day23();
            RunDayStatistique.RunPart("Day 23", "Part 1", () => day23.SolveOne());
        }
        public static void RunD23P2()
        {
            var day23 = new Day23();
            RunDayStatistique.RunPart("Day 23", "Part 2", () => day23.SolveTwo());
        }

        public static void RunD24P1()
        {
            var day24 = new Day24();
            RunDayStatistique.RunPart("Day 24", "Part 1", () => day24.SolveOne());
        }
        public static void RunD24P2()
        {
            var day24 = new Day24();
            RunDayStatistique.RunPart("Day 24", "Part 2", () => day24.SolveTwo());
        }

        public static void RunD25P1()
        {
            var day25 = new Day25();
            RunDayStatistique.RunPart("Day 25", "Part 1", () => day25.SolveOne());
        }
        public static void RunD25P2()
        {
            var day25 = new Day25();
            RunDayStatistique.RunPart("Day 25", "Part 2", () => day25.SolveTwo());
        }

    }
}
