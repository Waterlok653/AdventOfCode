
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Numerics;

namespace AdventOfCode.Year2015
{
    public class Day7
    {
        private class Day7Object
        {
            public Operation Operation;
            public ushort? Value = null;
            public int parameter;
            public string Source1;
            public string Source2;

            public ushort GetSource(Dictionary<string, Day7Object> dict, string name)
            {
                var val = dict.GetValueOrDefault(name);
                if (val == default)
                {
                    return (ushort)int.Parse(name);
                }
                else
                {
                    return val.GetValue(dict);
                }
            }

            public ushort GetValue(Dictionary<string, Day7Object> dict)
            {
                if (Value != null)
                {
                    return Value.Value;
                }

                switch (Operation)
                {
                    case Operation.AND:
                        Value = (ushort)(GetSource(dict, Source1) & GetSource(dict, Source2));
                        break;
                    case Operation.OR:
                        Value = (ushort)(GetSource(dict, Source1) | GetSource(dict, Source2));
                        break;
                    case Operation.RSH:
                        Value = (ushort)(GetSource(dict, Source1) >> parameter);
                        break;
                    case Operation.LSH:
                        Value = (ushort)(GetSource(dict, Source1) << parameter);
                        break;
                    case Operation.NOT:
                        Value = (ushort)(~GetSource(dict, Source1));
                        break;
                    case Operation.IsLinkedTo:
                        Value = (ushort)GetSource(dict, Source1);
                        break;
                }
                return Value.Value;
            }
        }
        private enum Operation
        {
            Value,
            AND,
            OR,
            NOT,
            LSH,
            RSH,
            IsLinkedTo,
        }
        public BigInteger SolveOne()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2015\\inputs\\day7.txt");
            string input = File.ReadAllText(configFile);
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);
            
            Dictionary<string, Day7Object> values = new Dictionary<string, Day7Object>();

            foreach(string data in inputs)
            {
                var splitedData = data.Split(" ");

                var newObject = new Day7Object();
                if (int.TryParse(splitedData[0], out int result) && splitedData[1].Equals("->"))
                {
                    newObject.Value = (ushort?)result;
                    newObject.Operation = Operation.Value;
                }
                else if (splitedData[0].Equals("NOT"))
                {
                    newObject.Operation = Operation.NOT;
                    newObject.Source1 = splitedData[1];
                }
                else
                {
                    newObject.Source1 = splitedData[0];
                    if (splitedData[1].Contains("SHIFT"))
                    {
                        newObject.parameter = int.Parse(splitedData[2]);
                        if (splitedData[1].StartsWith('R'))
                        {
                            newObject.Operation = Operation.RSH;
                        }
                        else
                        {
                            newObject.Operation = Operation.LSH;
                        }
                    }
                    else if (splitedData[1].Equals("->"))
                    {
                        newObject.Operation = Operation.IsLinkedTo;
                    }
                    else
                    {
                        newObject.Source2 = splitedData[2];

                        if (splitedData[1].Equals("AND"))
                            newObject.Operation = Operation.AND;
                        else
                            newObject.Operation = Operation.OR;
                    }
                }
                values.Add(splitedData.Last(), newObject);
            }


            return values.GetValueOrDefault("a").GetValue(values);
        }


        public BigInteger SolveTwo()
        {
            string basePath = Environment.GetEnvironmentVariable("ADVENT_OF_CODE");
            string configFile = Path.Combine(basePath, "AdventOfCode\\Year2015\\inputs\\day7.txt");
            string input = File.ReadAllText(configFile);
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);

            Dictionary<string, Day7Object> values = new Dictionary<string, Day7Object>();

            foreach (string data in inputs)
            {
                var splitedData = data.Split(" ");

                var newObject = new Day7Object();
                if (int.TryParse(splitedData[0], out int result) && splitedData[1].Equals("->"))
                {
                    newObject.Value = (ushort?)result;
                    newObject.Operation = Operation.Value;
                }
                else if (splitedData[0].Equals("NOT"))
                {
                    newObject.Operation = Operation.NOT;
                    newObject.Source1 = splitedData[1];
                }
                else
                {
                    newObject.Source1 = splitedData[0];
                    if (splitedData[1].Contains("SHIFT"))
                    {
                        newObject.parameter = int.Parse(splitedData[2]);
                        if (splitedData[1].StartsWith('R'))
                        {
                            newObject.Operation = Operation.RSH;
                        }
                        else
                        {
                            newObject.Operation = Operation.LSH;
                        }
                    }
                    else if (splitedData[1].Equals("->"))
                    {
                        newObject.Operation = Operation.IsLinkedTo;
                    }
                    else
                    {
                        newObject.Source2 = splitedData[2];

                        if (splitedData[1].Equals("AND"))
                            newObject.Operation = Operation.AND;
                        else
                            newObject.Operation = Operation.OR;
                    }
                }
                values.Add(splitedData.Last(), newObject);
            }
            values.GetValueOrDefault("b").Value = values.GetValueOrDefault("a").GetValue(values);
            foreach (var item in values)
            {
                if (item.Value.Operation != Operation.Value)
                {
                    item.Value.Value = null;
                }
            }

            return values.GetValueOrDefault("a").GetValue(values);
        }
    }
}
