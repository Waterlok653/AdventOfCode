
using System;
using System.IO;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015
{
    public class Day12
    {
        public BigInteger SolveOne()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2015\\inputs\\day12.txt");
            string input = File.ReadAllText(configFile);

            return Regex.Matches(input, "(-?[0-9]+)").Sum(t => int.Parse(t.Value));
        }

        public BigInteger SolveTwo()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2015\\inputs\\day12.txt");
            string input = File.ReadAllText(configFile);

            var json = System.Text.Json.JsonDocument.Parse(input);
            return SumElement(json.RootElement);
        }
        private long SumElement(JsonElement element)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.String:
                    return 0;
                case JsonValueKind.Number:
                    return element.GetInt32();
                case JsonValueKind.Array:
                    long sum = 0;
                    foreach (var item in element.EnumerateArray())
                    {
                        sum += SumElement(item);
                    }
                    return sum;
                case JsonValueKind.Object:
                    if (element.EnumerateObject().Any(t => t.Value.ValueKind == JsonValueKind.String && t.Value.ToString().Equals("red")))
                    {
                        return 0;
                    }
                    var sumOb = 0l;
                    foreach (var item in element.EnumerateObject())
                    {
                        sumOb += SumElement(item.Value);
                    }
                    return sumOb;
                default:
                    return 0;
            }
        }

        private long Sum(string input) //TODO
        {
            var x = new String(input);
            if (Regex.IsMatch(input, "^\"[a-z]+\":({|\\[).*(}|\\])$"))
            {
                input = input[5..^1];
            }
            else if ((input.StartsWith('{') && input.EndsWith('}')) || input.StartsWith('[') && input.EndsWith(']'))
            {
                input = input[1..^1];
            }
            if (input.Length == 0)
            {
                return 0;
            }
            var splited = SplitFirstLevel(input);
            if (splited.Any(t => Regex.IsMatch(t, "^\"[a-z]+\":\"red\"$")))
            {
                return 0;
            }
            var sum = 0l;
            if (input.Contains("\"y\""))
            {
                Console.WriteLine(input);
                Console.WriteLine("------------------------------");
            }
            foreach (var t in splited)
            {
                if (Regex.IsMatch(t, "^\"[a-z]+\"$") || Regex.IsMatch(t, "^\"[a-z]+\":\"[a-z]+\"$"))
                {
                    continue;
                }
                if (int.TryParse(t, out var value))
                {
                    sum += value;
                }
                else if (Regex.IsMatch(t, "^\"[a-z]+\":(-?[0-9]+)$"))
                {
                    sum += int.Parse(t.Split(':')[1]);
                }
                else
                {                    
                    sum += Sum(t);
                }
            }
            return sum;
        }
        private List<string> SplitFirstLevel(string input)
        {
            var returnValue = new List<string>();
            var currentLevel = 0;
            var currentString = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '{' || input[i] == '[')
                {
                    currentLevel++;
                }
                else if (input[i] == '}' || input[i] == ']')
                {
                    currentLevel--;
                }

                if (currentLevel == 0 && input[i] == ',')
                {
                    returnValue.Add(currentString);
                    currentString = "";
                }
                else
                {
                    currentString += input[i];
                }

            }

            returnValue.Add(currentString);
            return returnValue;
        }
    }
}
