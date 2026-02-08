using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{

    class AdventOfCodeFileGenerator
    {
        public static void Create(string year)
        {

            // Base path from environment variable or fallback
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE")
                              ?? @"C:\AdventOfCode";

            string yearPath = Path.Combine(basePath, "AdventOfCode", $"Year{year}");
            Directory.CreateDirectory(yearPath);
            Directory.CreateDirectory(Path.Combine(yearPath, "inputs"));

            // Number of days in Advent of Code
            int totalDays = 25;


            // ===== Generate DayX.cs files =====
            for (int day = 1; day <= totalDays; day++)
            {
                string dayClass = $"Day{day}";
                string fileName = Path.Combine(yearPath, $"{dayClass.ToLower()}.cs");
                var intputFile = Path.Combine(yearPath, "inputs", $"{dayClass.ToLower()}.txt");
                if (!File.Exists(intputFile))
                {
                    File.WriteAllText(intputFile, "");
                }
                string fileContent = $@"
using System;
using System.IO;
using System.Numerics;

namespace AdventOfCode.Year{year}
{{
    public class {dayClass}
    {{
        public BigInteger SolveOne()
        {{
            string basePath = Environment.GetEnvironmentVariable(""ADVENT_OF_CODE"");
            string configFile = Path.Combine(basePath, ""AdventOfCode\\Year{year}\\inputs\\{dayClass.ToLower()}.txt"");
            string input = File.ReadAllText(configFile);
            string[] inputs = input.Split(new[] {{ ""\r\n"", ""\n\r"", ""\r"", ""\n"" }}, StringSplitOptions.None);

            return 0;
        }}

        public BigInteger SolveTwo()
        {{
            string basePath = Environment.GetEnvironmentVariable(""ADVENT_OF_CODE"");
            string configFile = Path.Combine(basePath, ""AdventOfCode\\Year{year}\\inputs\\{dayClass.ToLower()}.txt"");
            string input = File.ReadAllText(configFile);
            string[] inputs = input.Split(new[] {{ ""\r\n"", ""\n\r"", ""\r"", ""\n"" }}, StringSplitOptions.None);

            return 0;
        }}
    }}
}}
";
                if (!File.Exists(fileName))
                {
                    File.WriteAllText(fileName, fileContent);
                    Console.WriteLine($"Created file: {fileName}");
                }
            }

            // ===== Generate RunYYYY.cs file =====
            string runFileName = Path.Combine(yearPath, $"Run{year}.cs");

            // Build the switch cases dynamically
            string switchCases = "";
            string runMethods = "";
            for (int day = 1; day <= totalDays; day++)
            {
                switchCases += $"case {day}: if (part == 1) RunD{day}P1(); else RunD{day}P2(); break;\n                ";
                runMethods += $@"
        public static void RunD{day}P1()
        {{
            var day{day} = new Day{day}();
            RunDayStatistique.RunPart(""Day {day}"", ""Part 1"", () => day{day}.SolveOne());
        }}
        public static void RunD{day}P2()
        {{
            var day{day} = new Day{day}();
            RunDayStatistique.RunPart(""Day {day}"", ""Part 2"", () => day{day}.SolveTwo());
        }}
";
            }

            string runFileContent = $@"
namespace AdventOfCode.Year{year}
{{
    public static class Run{year}
    {{
        // ===== ENTRY POINT =====
        public static void Run(string dayInput, string partInput)
        {{
            if (dayInput == ""A"")
            {{
                for (int day = 1; day <= {totalDays}; day++)
                    RunDay(day, partInput);
            }}
            else
            {{
                RunDay(int.Parse(dayInput), partInput);
            }}
        }}

        // ===== DAY DISPATCHER =====
        private static void RunDay(int day, string partInput)
        {{
            if (partInput == ""A"")
            {{
                RunPart(day, 1);
                RunPart(day, 2);
            }}
            else
            {{
                RunPart(day, int.Parse(partInput));
            }}
        }}

        // ===== PART DISPATCHER =====
        private static void RunPart(int day, int part)
        {{
            switch (day)
            {{
                {switchCases}
            }}
        }}

        // ===== EXISTING METHODS (AUTO-GENERATED) =====
        {runMethods}
    }}
}}
";

            if (!File.Exists(runFileName))
            {
                File.WriteAllText(runFileName, runFileContent);
                Console.WriteLine($"Created file: {runFileName}");
            }
        }
    }

}
