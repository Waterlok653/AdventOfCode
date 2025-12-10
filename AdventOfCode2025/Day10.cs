using System;
using System.Collections.Generic;
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
    public static class Day10
    {
        public static void SolveOne()
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
                var allValidInput = new List<(string,int)>();
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
            Console.WriteLine("You need to press that many button : "+ sumOfPress);
        }
        public static string ConverteNumberToBinary(int input, int length, out bool tryEveryPosibility)
        {
            var currentInput = Convert.ToString(input, 2);
            while (currentInput.Length < length) {
                currentInput = "0" + currentInput;
            }
            tryEveryPosibility = currentInput.Length > length;
            currentInput = currentInput.Remove(length);
            return currentInput;

        }
        public static bool IsAGoodInput(Day10Object machine, int[] inputs)
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

        public static bool IsAGoodInputForVoltage(Day10Object machine, List<Int128> inputs)
        {
            int[] currentStatus = new int[machine.Jolitage.Length];
            for (int i = 0; i < inputs.Count; i++)
            {
                for (Int128 j = 0; j < inputs[i]; j++)
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
                    return false;
                }
            }
            return true;
        }
        public static List<Int128> ConverteNumberToBasse(Int128 input, Int128 newBase, int length, out bool tryEveryPosibility)
        {
            tryEveryPosibility = false;
            List<Int128> newNumber = new List<Int128>();
            Int128 reste = input;

            while (reste > 0)
            {
                newNumber.Add(reste / newBase);
                reste = reste % newBase;
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
            return newNumber;
        }

        public static void SolveTwo()
        {

            var currentInput = Convert.ToString(129, 128);

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
            foreach (var machine in list)
            {
                var allValidInput = new List<(List<Int128>, Int128)>();
                var bigestVoltage = machine.Jolitage.Max();
                for (Int128 i = 0; i < Int128.MaxValue; i++)
                {
                    var buttonToPress = ConverteNumberToBasse(i, bigestVoltage + 1, machine.Buttons.Length, out bool tryEveryPosibility);
                    if (tryEveryPosibility)
                    {
                        break;
                    }
                    if (IsAGoodInputForVoltage(machine, buttonToPress))
                    {
                        Int128 sum = 0;
                        foreach (var button in buttonToPress)
                        {
                            sum++;
                        }
                        allValidInput.Add((buttonToPress, sum));
                    }

                }
                sumOfPress += allValidInput.OrderBy(t => t.Item2).First().Item2;
            }
            Console.WriteLine("You need to press that many button : " + sumOfPress);
        }
    }
}
    }
}
