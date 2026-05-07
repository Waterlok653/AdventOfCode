
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Numerics;

namespace AdventOfCode.Year2015
{
    public class Day13
    {
        private class Day13Guy
        {
            public string Name = "";
            public List<(string, int)> Values = new List<(string, int)>();
        }
        public BigInteger SolveOne()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2015\\inputs\\day13.txt");
            string input = File.ReadAllText(configFile);
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);
            var allName = new List<string>();
            var data = new Dictionary<string, Day13Guy>();
            var i = 0;
            foreach (var item in inputs)
            {
                var splited = item.Split(" ");
                var guy = data.GetValueOrDefault(splited[0]);
                if (guy == null)
                {
                    guy = new Day13Guy();
                }
                guy.Values.Add((splited[10][..^1], (splited[2].Equals("lose")) ? -int.Parse(splited[3]) : int.Parse(splited[3])));

                if (guy.Name.Equals(""))
                {
                    allName.Add(splited[0]);
                    guy.Name = splited[0];
                    data.Add(splited[0], guy);
                }
            }



            return GetAllPermutation(allName.ToArray(), 1, allName.Count-1, data);
        }

        private int GetAllPermutation(string[] array, int start, int end, Dictionary<string, Day13Guy> data)
        {
            if (start == end)
            {
                Console.WriteLine();
                foreach (var item in array)
                {
                    Console.Write(item);
                }
                return GetSum(array, data);
            }
            var max = 0;
            for (int i = start; i <= end; i++)
            {
                var tmp = array[i];
                array[i] = array[start];
                array[start] = tmp;
                var value = GetAllPermutation(array, start+1, end, data);
                if (value > max)
                {
                    max = value;
                }
                tmp = array[i];
                array[i] = array[start];
                array[start] = tmp;
            }
            return max;
        }

        private int GetSum(string[] array, Dictionary<string, Day13Guy> data)
        {
            var sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (i+1 >= array.Length)
                {
                    sum += data.GetValueOrDefault(array[i]).Values.Where(t => t.Item1.Equals(array[0])).First().Item2;
                }
                else
                {
                    sum += data.GetValueOrDefault(array[i]).Values.Where(t => t.Item1.Equals(array[i + 1])).First().Item2;
                }
                if (i == 0)
                {
                    sum += data.GetValueOrDefault(array[i]).Values.Where(t => t.Item1.Equals(array.Last())).First().Item2;
                }
                else
                {
                    sum += data.GetValueOrDefault(array[i]).Values.Where(t => t.Item1.Equals(array[i - 1])).First().Item2;
                }
            }
            return sum;
        }

        public BigInteger SolveTwo() //TODO faster
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2015\\inputs\\day13.2.txt");
            string input = File.ReadAllText(configFile);
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);
            var allName = new List<string>();
            var data = new Dictionary<string, Day13Guy>();
            var i = 0;
            foreach (var item in inputs)
            {
                var splited = item.Split(" ");
                var guy = data.GetValueOrDefault(splited[0]);
                if (guy == null)
                {
                    guy = new Day13Guy();
                }
                guy.Values.Add((splited[10][..^1], (splited[2].Equals("lose")) ? -int.Parse(splited[3]) : int.Parse(splited[3])));

                if (guy.Name.Equals(""))
                {
                    allName.Add(splited[0]);
                    guy.Name = splited[0];
                    data.Add(splited[0], guy);
                }
            }



            return GetAllPermutation(allName.ToArray(), 1, allName.Count - 1, data);
        }
    }
}
