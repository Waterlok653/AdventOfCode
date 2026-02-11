
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace AdventOfCode.Year2015
{
    public class Day9
    {
        public BigInteger SolveOne()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2015\\inputs\\day9.txt");
            string input = File.ReadAllText(configFile);
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);

            Dictionary<string, Dictionary<string, int>> path = new Dictionary<string, Dictionary<string, int>>();
            List<string> visited = new List<string>();
            foreach (string line in inputs)
            {
                var data = line.Split(" ");
                if (path.TryGetValue(data[0], out Dictionary<string, int> result))
                {
                    result.Add(data[2], int.Parse(data[4]));
                }
                else
                {
                    path.Add(data[0], new Dictionary<string, int>() { { data[2], int.Parse(data[4]) } });
                }
                if (path.TryGetValue(data[2], out Dictionary<string, int> result2))
                {
                    result2.Add(data[0], int.Parse(data[4]));
                }
                else
                {
                    path.Add(data[2], new Dictionary<string, int>() { { data[0], int.Parse(data[4]) } });
                }
            }

            int shortest = int.MaxValue;
            for (int i = 0; i < path.Count - 1; i++)
            {
                for (int j = i + 1; j < path.Count; j++)
                {
                    shortest = int.Min(shortest, GetLenght("", path.Keys.ToList()[i], path.Keys.ToList()[j], path));

                }
            }

            return shortest;
        }
        private int GetLenght(string path, string current, string end, Dictionary<string, Dictionary<string, int>> allPath)
        {
            path += "," + current;
            var shortest = int.MaxValue;
            foreach (var item in allPath.GetValueOrDefault(current))
            {
                if (!path.Contains(item.Key))
                {
                    if (path.Count(',') == allPath.Count-1)
                    {
                        if (item.Key.Equals(end))
                        {
                            return item.Value;
                        }
                    }
                    else
                    {
                        if (!item.Key.Equals(end))
                        {
                            var distance = item.Value;
                            distance += GetLenght($"{path}", item.Key, end, allPath);
                            shortest = int.Min(distance,shortest);
                        }
                    }
                }
            }
            return shortest;
        }

        public BigInteger SolveTwo()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2015\\inputs\\day9.txt");
            string input = File.ReadAllText(configFile);
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);

            Dictionary<string, Dictionary<string, int>> path = new Dictionary<string, Dictionary<string, int>>();
            List<string> visited = new List<string>();
            foreach (string line in inputs)
            {
                var data = line.Split(" ");
                if (path.TryGetValue(data[0], out Dictionary<string, int> result))
                {
                    result.Add(data[2], int.Parse(data[4]));
                }
                else
                {
                    path.Add(data[0], new Dictionary<string, int>() { { data[2], int.Parse(data[4]) } });
                }
                if (path.TryGetValue(data[2], out Dictionary<string, int> result2))
                {
                    result2.Add(data[0], int.Parse(data[4]));
                }
                else
                {
                    path.Add(data[2], new Dictionary<string, int>() { { data[0], int.Parse(data[4]) } });
                }
            }

            int shortest = int.MinValue;
            for (int i = 0; i < path.Count - 1; i++)
            {
                for (int j = i + 1; j < path.Count; j++)
                {
                    shortest = int.Max(shortest, GetLenghtLongeest("", path.Keys.ToList()[i], path.Keys.ToList()[j], path));

                }
            }

            return shortest;
        }
        private int GetLenghtLongeest(string path, string current, string end, Dictionary<string, Dictionary<string, int>> allPath)
        {
            path += "," + current;
            var shortest = int.MinValue;
            foreach (var item in allPath.GetValueOrDefault(current))
            {
                if (!path.Contains(item.Key))
                {
                    if (path.Count(',') == allPath.Count - 1)
                    {
                        if (item.Key.Equals(end))
                        {
                            return item.Value;
                        }
                    }
                    else
                    {
                        if (!item.Key.Equals(end))
                        {
                            var distance = item.Value;
                            distance += GetLenghtLongeest($"{path}", item.Key, end, allPath);
                            shortest = int.Max(distance, shortest);
                        }
                    }
                }
            }
            return shortest;
        }
    }
}
