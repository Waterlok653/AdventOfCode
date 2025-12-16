using AdventOfCode.Year2025;

namespace AdventOfCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string yearInput = "";
            string dayInput = "";
            string partInput = "";
            Console.WriteLine("Year 2015 / ... / 2025 / [A]ll):");
            yearInput = Console.ReadLine()?.Trim();
            if (!yearInput.Equals("A"))
            {
                if (int.Parse(yearInput) < 2025)
                {

                    Console.WriteLine("Day 1 / ... / 24 / [A]ll):");
                }
                else
                {
                    Console.WriteLine("Day 1 / ... / 12 / [A]ll):");
                }
                    dayInput = Console.ReadLine()?.Trim();
                if (!dayInput.Equals("A"))
                {
                    Console.WriteLine("Part 1 / 2 / [A]ll):");
                    partInput = Console.ReadLine()?.Trim();
                }
                else
                {
                    partInput = "A";
                }
            }
            else
            {
                partInput = "A";
                dayInput = "A";
            }


            if (yearInput == "A")
            {
                for (int year = 2015; year <= 2025; year++)
                {
                    RunYear(year, dayInput, partInput);
                }
            }
            else
            {
                int year = int.Parse(yearInput);
                RunYear(year, dayInput, partInput);
            }


            Console.ReadKey();
        }
        static void RunYear(int year, string dayInput, string partInput)
        {
            switch (year)
            {
                /*case 2015: Run2015.Run(dayInput, partInput); break;
                case 2016: Run2016.Run(dayInput, partInput); break;
                case 2017: Run2017.Run(dayInput, partInput); break;
                case 2018: Run2018.Run(dayInput, partInput); break;
                case 2019: Run2019.Run(dayInput, partInput); break;
                case 2020: Run2020.Run(dayInput, partInput); break;
                case 2021: Run2021.Run(dayInput, partInput); break;
                case 2022: Run2022.Run(dayInput, partInput); break;
                case 2023: Run2023.Run(dayInput, partInput); break;
                case 2024: Run2024.Run(dayInput, partInput); break;*/
                case 2025: Run2025.Run(dayInput, partInput); break;
            }
        }

    }
}
