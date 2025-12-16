using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2025
{
    public class Day10Object
    {
        public bool[] LightDiagram = new bool[0];
        public int[][] Buttons = new int[0][];
        public int[] Jolitage = new int[0];
    }


    public class Day10
    {
        public int[][] AllShape = new int[0][];
        public Int128 SolveOne()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day10.txt");
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);

            List<Day10Object> list = new List<Day10Object>();

            foreach (var item in inputs)
            {
                var newObject = new Day10Object();
                var currentLine = item;

                var lights = Regex.Match(currentLine, @"\[[.#]*\]").ToString();
                newObject.LightDiagram = new bool[lights[1..^1].Length];
                int i = 0;
                foreach (var light in lights[1..^1])
                {
                    newObject.LightDiagram[i++] = light == '#';
                }

                var voltage = Regex.Match(currentLine, @"\{\S*\}").ToString();

                currentLine = currentLine.Replace(lights, "");
                currentLine = currentLine.Replace(voltage, "");

                currentLine = currentLine.Trim();
                var allButon = currentLine.Split(" ");

                newObject.Buttons = new int[allButon.Length][];

                i = 0;
                foreach (var light in allButon)
                {
                    var allWire = light[1..^1].Split(",");
                    newObject.Buttons[i] = new int[allWire.Length];
                    int j = 0;
                    foreach (var wire in allWire)
                    {
                        newObject.Buttons[i][j++] = int.Parse(wire);
                    }
                    i++;
                }
                list.Add(newObject);
            }
            int sumOfPress = 0;
            foreach (var machine in list)
            {
                var allValidInput = new List<(string, int)>();
                for (int i = 0; i < int.MaxValue; i++)
                {
                    var buttonToPress = ConverteNumberToBinary(i, machine.Buttons.Length, out bool tryEveryPosibility);
                    if (tryEveryPosibility)
                    {
                        break;
                    }
                    if (IsAGoodInput(machine, buttonToPress.ToCharArray().Select(t => int.Parse(t.ToString())).ToArray()))
                    {
                        allValidInput.Add((buttonToPress, buttonToPress.Count('1')));
                    }

                }
                sumOfPress += allValidInput.OrderBy(t => t.Item2).First().Item2;

            }
            return sumOfPress;
        }
        public string ConverteNumberToBinary(int input, int length, out bool tryEveryPosibility)
        {
            var currentInput = Convert.ToString(input, 2);
            while (currentInput.Length < length)
            {
                currentInput = "0" + currentInput;
            }
            tryEveryPosibility = currentInput.Length > length;
            currentInput = currentInput.Remove(length);
            return currentInput;

        }
        public bool IsAGoodInput(Day10Object machine, int[] inputs)
        {
            bool[] currentStatus = new bool[machine.LightDiagram.Length];
            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[i]; j++)
                {
                    for (int k = 0; k < machine.Buttons[i].Length; k++)
                    {
                        currentStatus[machine.Buttons[i][k]] = !currentStatus[machine.Buttons[i][k]];
                    }
                }
            }
            for (int i = 0; i < currentStatus.Length; i++)
            {
                if (currentStatus[i] != machine.LightDiagram[i])
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsAGoodInputForVoltage(Day10Object machine, int[] inputs, out bool isImposible)
        {
            isImposible = false;
            int[] currentStatus = new int[machine.Jolitage.Length];
            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[i]; j++)
                {
                    for (int k = 0; k < machine.Buttons[i].Length; k++)
                    {
                        currentStatus[machine.Buttons[i][k]]++;
                    }
                }
            }
            for (int i = 0; i < currentStatus.Length; i++)
            {
                if (currentStatus[i] != machine.Jolitage[i])
                {
                    if (currentStatus[i] > machine.Jolitage[i])
                    {
                        isImposible = true;
                    }
                    return false;
                }
            }
            return true;
        }

        public List<int[]> GetAllCombination(int numberOfPresse, List<int[]> current)
        {
            if (current.Count == 0)
            {
                current.Add(new int[AllShape[0].Length]);
            }
            if (numberOfPresse == 0)
            {
                return current;
            }

            var newCurrent = new List<int[]>();

            foreach (var shape in AllShape)
            {
                foreach (var item in current)
                {
                    var newInput = new int[item.Length];
                    for(int i = 0;i < newInput.Length; i++)
                    {
                        newInput[i] = item[i] + shape[i];
                    }
                    newCurrent.Add(newInput);
                }
            }

            return GetAllCombination(--numberOfPresse, newCurrent.DistinctBy(e => string.Join(",", e)).ToList());
        }
        public List<Int128> ConverteNumberToBasse(Int128 input, Int128 newBase, int length, int sumOfVoltage, out bool tryEveryPosibility, out bool isImposible)
        {
            tryEveryPosibility = false;
            List<Int128> newNumber = new List<Int128>();
            Int128 reste = input;
            Int128 sum = 0;
            while (reste > 0)
            {
                var value = reste % newBase;
                sum += value;
                if (sum > sumOfVoltage)
                {
                    isImposible = true;
                    return newNumber;
                }
                newNumber.Add(value);
                reste = reste / newBase;
            }
            if (sum < newBase - 1)
            {
                isImposible = true;
                return newNumber;
            }
            if (newNumber.Count == length)
            {
                foreach (Int128 i in newNumber)
                {
                    tryEveryPosibility = true;
                    if (i != newBase - 1)
                    {

                        tryEveryPosibility = false;
                    }
                }
            }
            isImposible = false;
            return newNumber;
        }

        public Int128 SolveTwo()
        {

            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day10.txt");
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);

            List<Day10Object> list = new List<Day10Object>();

            foreach (var item in inputs)
            {
                var newObject = new Day10Object();
                var currentLine = item;

                var voltages = Regex.Match(currentLine, @"\{\S*\}").ToString();
                newObject.Jolitage = new int[voltages[1..^1].Split(",").Length];
                int i = 0;
                foreach (var voltage in voltages[1..^1].Split(","))
                {
                    newObject.Jolitage[i++] = int.Parse(voltage);
                }


                currentLine = currentLine.Replace(Regex.Match(currentLine, @"\[[.#]*\]").ToString(), "");
                currentLine = currentLine.Replace(voltages, "");

                currentLine = currentLine.Trim();
                var allButon = currentLine.Split(" ");

                newObject.Buttons = new int[allButon.Length][];

                i = 0;
                foreach (var light in allButon)
                {
                    var allWire = light[1..^1].Split(",");
                    newObject.Buttons[i] = new int[allWire.Length];
                    int j = 0;
                    foreach (var wire in allWire)
                    {
                        newObject.Buttons[i][j++] = int.Parse(wire);
                    }
                    i++;
                }
                list.Add(newObject);
            }
            Int128 sumOfPress = 0;
            int ccc =0;
            foreach (var machine in list)
            {
                Console.WriteLine(ccc++);
                bool hasFound = false;
                AllShape = new int[machine.Buttons.Length][];
                for (int i = 0; i < machine.Buttons.Length; i++)
                {
                    AllShape[i] = ConverteNumberToBinary(int.Parse(Math.Pow(2, i).ToString()), machine.Buttons.Length, out bool RAF).ToCharArray().Select(t => int.Parse(t.ToString())).ToArray(); ;
                }
                bool isFirstTime = true;
                List<int[]> all = new List<int[]>();
                for (int numberOfPresse = machine.Jolitage.Max(); numberOfPresse < int.MaxValue; numberOfPresse++)
                {
                    all = GetAllCombination((isFirstTime) ? numberOfPresse : 1, all);
                    isFirstTime = false;
                    List<int[]> needToBeRM = new List<int[]>();
                    foreach (var combinesonOfPress in all)
                    {
                        bool isImposible = false;
                        if (IsAGoodInputForVoltage(machine, combinesonOfPress, out isImposible))
                        {
                            hasFound = true;
                            sumOfPress += numberOfPresse;
                            break;
                        }
                        if (isImposible)
                        {
                            needToBeRM.Add(combinesonOfPress);
                        }
                    }
                    foreach (var item in needToBeRM)
                    {
                        all.Remove(item);
                    }
                    if (hasFound)
                    {
                        break;
                    }

                }

            }
           return sumOfPress;

        }
    }
}