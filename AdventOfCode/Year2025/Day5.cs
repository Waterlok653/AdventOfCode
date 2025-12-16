using System.Text.RegularExpressions;

namespace AdventOfCode.Year2025
{
    public partial class Day5
    {
        public Int128 SolveOne()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode\\AdventOfCode\\Year2025\\inputs\\day5.txt");
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);
            List<(long, long)> fresh = new List<(long, long)>();
            List<long> ingresdient = new List<long>();

            foreach (var item in inputs)
            {
                if (item.Equals(""))
                {
                    continue;
                }
                if (RegexIsRangeInput().IsMatch(item))
                {
                    var numbers = item.Split('-');
                    fresh.Add((long.Parse(numbers[0]), long.Parse(numbers[1])));
                    continue;
                }
                ingresdient.Add(long.Parse(item));
            }

            List<long> listFresh = new List<long>();
            foreach (var item in ingresdient)
            {
                foreach (var item2 in fresh)
                {
                    if (item2.Item1 > item)
                    {
                        continue;
                    }
                    if (item2.Item1 <= item && item2.Item2 >= item)
                    {
                        listFresh.Add(item);
                        break;
                    }
                }
            }

            return listFresh.Count;

        }


        public Int128 SolveTwo()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode\\AdventOfCode\\Year2025\\inputs\\day5.txt");
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);
            List<(long, long)> fresh = new List<(long, long)>();

            foreach (var item in inputs)
            {
                if (item.Equals(""))
                {
                    continue;
                }
                if (RegexIsRangeInput().IsMatch(item))
                {
                    var numbers = item.Split('-');
                    fresh.Add((long.Parse(numbers[0]), long.Parse(numbers[1])));
                    continue;
                }
                break;
            }
            bool didchange;
            List<(long, long)> newFresh;
            List<(long, long)> toRm;
            do
            {
                newFresh = new List<(long, long)>();
                toRm = new List<(long, long)>();
                didchange = false;
                foreach (var item in fresh)
                {
                    var didchange2 = false;
                    foreach (var item2 in fresh)
                    {
                        if (item2 == item)
                            continue;
                        if (item.Item1 >= item2.Item1 && item.Item2 <= item2.Item2)
                        {
                            didchange2 = true;
                            toRm.Add(item);
                            break;
                        }

                        if (item.Item1 >= item2.Item1 && item.Item1 <= item2.Item2)
                        {
                            newFresh.Add((item2.Item1, item.Item2));
                            toRm.Add(item);
                            toRm.Add(item2);
                            didchange = true;
                            didchange2 = true;
                            break;
                        }


                        if (item.Item2 <= item2.Item2 && item.Item2 >= item2.Item1)
                        {
                            newFresh.Add((item.Item1, item2.Item2));
                            toRm.Add(item);
                            toRm.Add(item2);
                            didchange = true;
                            didchange2 = true;
                            break;
                        }
                    }
                    if (didchange2)
                    {
                        break;
                    }

                }
                foreach (var item in newFresh)
                {
                    fresh.Add(item);
                    didchange = true;
                }
                foreach (var item in toRm)
                {
                    fresh.Remove(item);
                    didchange = true;
                }
            } while (didchange);

            var uniqueFresh = fresh.Distinct().ToArray();
            long total = 0;
            foreach (var item in uniqueFresh)
            {
                total += item.Item2 - item.Item1 + 1;
            }

            return total;

        }

        [GeneratedRegex(@"^[0-9]*-[0-9]*$")]
        private static partial Regex RegexIsRangeInput();
    }
}