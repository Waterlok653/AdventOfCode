using Microsoft.Z3;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2025
{
    public static class Day10Part2NoNuget
    {

        public static Int128 Solve()
        {
            string input = System.IO.File.ReadAllText("C:\\Users\\Moi\\source\\repos\\AdventOfCode2025\\AdventOfCode2025\\inputs\\day10.txt");
            string[] inputs = input.Split(new[] { "\r\n", "\n\r", "\r", "\n" }, StringSplitOptions.None);

            List<Day10Part2Object> list = new List<Day10Part2Object>();

            foreach (var item in inputs)
            {
                var newObject = new Day10Part2Object();
                var currentLine = item;

                var voltages = Regex.Match(currentLine, @"\{\S*\}").ToString();
                newObject.Jolitage = new VectorV2(voltages[1..^1].Split(",").Length);
                int i = 0;
                foreach (var voltage in voltages[1..^1].Split(","))
                {
                    newObject.Jolitage.Vector[i++] = int.Parse(voltage);
                }


                currentLine = currentLine.Replace(Regex.Match(currentLine, @"\[[.#]*\]").ToString(), "");
                currentLine = currentLine.Replace(voltages, "");

                currentLine = currentLine.Trim();
                var allButon = currentLine.Split(" ");

                newObject.Vector = new VectorV2[allButon.Length];
                for (int j = 0; j < newObject.Vector.Length; j++)
                {
                    newObject.Vector[j] = new VectorV2(newObject.Jolitage.Vector.Length);
                }

                i = 0;
                foreach (var light in allButon)
                {
                    var allWire = light[1..^1].Split(",");
                    int j = 0;
                    foreach (var wire in allWire)
                    {
                        newObject.Vector[i].Vector[int.Parse(wire)] = 1;
                    }
                    i++;
                }
                list.Add(newObject);
            }
            Int128 sumOfPress = 0;
            foreach (var machine in list)
            {
                var matrix = new double[machine.Jolitage.Vector.Length][];
                for (int j = 0; j < matrix.Length; j++)
                {
                    matrix[j] = new double[machine.Vector.Length + 1];
                    for (int i = 0; i < machine.Vector.Length; i++)
                    {
                        matrix[j][i] = machine.Vector[i].Vector[j];
                    }
                    matrix[j][matrix[j].Length - 1] = machine.Jolitage.Vector[j];
                }
                matrix = GaussianElimination(matrix);
                List<double> allNotPivotColumn = new List<double>();


                List<((double value, int index) pivot, int row)> allPivot = new List<((double value, int index) pivot, int row)>();
                for (int i = 0; i < matrix.Length; i++)
                {
                    allPivot.Add((GetPivotOfRow(matrix[i]), i));
                }
                for (int i = 0; i < matrix[0].Length - 1; i++)
                {
                    if (allPivot.Where(p => p.pivot.index == i).Count() == 0)
                    {
                        allNotPivotColumn.Add(i);
                    }
                }
                if (allNotPivotColumn.Count == 0)
                {
                    File.AppendAllText("output1.txt", (int)matrix.Sum(t => t[t.Length - 1]) + "\n");
                    sumOfPress += (int)matrix.Sum(t => t[t.Length - 1]);
                    continue;
                }
                Day10.AllShape = new int[allNotPivotColumn.Count][];
                List<int[]> allTestInput = new List<int[]>();
                if (allNotPivotColumn.Count != 0)
                {
                    for (int i = 0; i < Day10.AllShape.Length; i++)
                    {
                        Day10.AllShape[i] = Day10.ConverteNumberToBinary(int.Parse(Math.Pow(2, i).ToString()), Day10.AllShape.Length, out bool RAF).ToCharArray().Select(t => int.Parse(t.ToString())).ToArray(); ;
                    }
                    allTestInput.AddRange(Day10.GetAllCombination(0, new List<int[]>()));

                }
                else
                {
                    allTestInput.Add(new int[] { 0 });
                }

                List<int[]> toAdd = new List<int[]>();
                var minPress = int.MaxValue;
                do
                {
                    toAdd.Clear();
                    foreach (var testInput in allTestInput)
                    {
                        var isNotPossible = false;
                        var newMatrix = new double[matrix.Length];
                        for (int i = 0; i < matrix.Length; i++)
                        {
                            newMatrix[i] = matrix[i][matrix[i].Length - 1];
                            for (int j = 0; j < matrix[i].Length - 1; j++)
                            {
                                if (allNotPivotColumn.Contains(j))
                                {
                                    newMatrix[i] -= testInput[allNotPivotColumn.IndexOf(j)] * matrix[i][j];
                                }
                            }
                            if ((Math.Round(newMatrix[i],3) < 0 && Math.Round(matrix[i][matrix[i].Length - 1], 3) > 0))
                            {
                                isNotPossible = true;
                                break;
                            }
                        }
                        if (isNotPossible)
                        {
                            allTestInput.Remove(testInput);
                            break;
                        }
                        toAdd.Add(testInput);
                        var nbPress = newMatrix.Sum(t => Math.Abs(t)) + testInput.Sum(t => Math.Abs(t));

                        if (minPress > nbPress)
                        {
                            bool areAllNumberInt = true;
                            for (int j = 0; j < newMatrix.Length - 1; j++)
                            {
                                var value = Math.Round(newMatrix[j], 3);
                                if (Math.Round(value - Math.Truncate(value), 3) != 0 || value < 0)
                                {
                                    areAllNumberInt = false;
                                    break;
                                }

                            }
                            if (areAllNumberInt)
                                minPress = (int)Math.Round(nbPress);
                        }
                        break;
                    }
                    if (toAdd.Count > 0)
                    {
                        allTestInput.AddRange(Day10.GetAllCombination(1, toAdd));
                        allTestInput = allTestInput.DistinctBy(e => string.Join(",", e)).ToList();
                        allTestInput.Remove(toAdd.First());
                    }
                } while (allTestInput.Count != 0);
                //problemme quand le resulta final est negatif
                File.AppendAllText("output1.txt", minPress + "\n");
                sumOfPress += minPress;
            }
            return sumOfPress;
        }


        public static double[][] GaussianElimination(double[][] matrix)
        {            
            matrix = PutAMatrixInRowEchelonForm(matrix);
            matrix = PutAMatrixInEchelonForm(matrix);
            matrix = PutAllPivotToOne(matrix);
            return matrix;
        }
        public static double[][] PutAllPivotToOne(double[][] matrix)
        {
            for (int i = matrix.Length - 1; i >= 0; i--)
            {
                (double value, int index) pivot = GetPivotOfRow(matrix[i]);
                if (pivot.value == 0)
                {
                    continue;
                }
                matrix = MultiplyARowByAScalar(matrix, i, 1 / pivot.value);
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    matrix[i][j] = Math.Round(matrix[i][j], 5);
                }
            }
            return matrix;
        }
        public static double[][] PutAMatrixInEchelonForm(double[][] matrix)
        {
            for (int i = matrix.Length - 1; i >= 0; i--)
            {
                (double value, int index) pivot = GetPivotOfRow(matrix[i]);
                if (pivot.value == 0)
                {
                    continue;
                }
                for (int j = 0; j < i; j++)
                {
                    matrix = AddAScalarMultipleOfARowToAnother(matrix, i, matrix[j][pivot.index] / (-pivot.value), j);
                }
            }
            return matrix;
        }


        public static double[][] PutAMatrixInRowEchelonForm(double[][] matrix)
        {
            bool didChange = false;
            double[][] newMatrix = new double[matrix.Length][];
            for (int i = 0; i < newMatrix.Length; i++)
            {
                newMatrix[i] = new double[matrix[0].Length];
            }

            List<((double value, int index) pivot, int row)> allPivot = new List<((double value, int index) pivot, int row)>();
            for (int i = 0; i < matrix.Length; i++)
            {
                allPivot.Add((GetPivotOfRow(matrix[i]), i));
            }
            allPivot = allPivot.OrderBy(x => x.pivot.index).ThenByDescending(x => x.pivot.value).ToList();
            int j = 0;
            foreach (var pivot in allPivot)
            {
                if (pivot.row == j)
                {
                    newMatrix[j] = matrix[pivot.row]; ;
                    j++;
                    continue;
                }
                for (int i = 0; i < matrix[pivot.row].Length; i++)
                {
                    newMatrix[j][i] = matrix[pivot.row][i];
                    didChange = true;
                }
                j++;
            }

            allPivot.Clear();
            for (int i = 0; i < newMatrix.Length; i++)
            {
                allPivot.Add((GetPivotOfRow(newMatrix[i]), i));
            }
            allPivot = allPivot.Where(p => p.pivot.value != 0).OrderBy(x => x.pivot.index).ThenByDescending(x => x.pivot.value).ToList();

            bool haveDoneIt = false;
            for (int i = 0; i < allPivot.Count; i++)
            {
                for (int k = 0; k < allPivot.Count; k++)
                {
                    if (allPivot[i] != allPivot[k] && allPivot[i].pivot.index == allPivot[k].pivot.index)
                    {
                        newMatrix = AddAScalarMultipleOfARowToAnother(newMatrix, allPivot[i].row, (-allPivot[k].pivot.value) / allPivot[i].pivot.value, allPivot[k].row);
                        haveDoneIt = true;
                        didChange = true;
                        break;
                    }
                }
                if (haveDoneIt)
                {
                    break;
                }
            }
            if (didChange)
                return PutAMatrixInRowEchelonForm(newMatrix);
            return newMatrix;
        }
        public static (double value, int index) GetPivotOfRow(double[] row)
        {
            int i = 0;
            if (row.Sum(h => Math.Abs(h)) == 0)
            {
                return (0, row.Length - 1);
            }
            while (true)
            {
                if (row[i] != 0)
                {
                    return (row[i], i);
                }
                i++;
            }
        }
        public static double[][] InterchangeTwoRow(double[][] matrix, int rowOne, int rowTwo)
        {
            for (int i = 0; i < matrix[rowOne].Length; i++)
            {
                var tmp = matrix[rowOne][i];
                matrix[rowOne][i] = matrix[rowTwo][i];
                matrix[rowTwo][i] = tmp;
            }
            return matrix;
        }
        public static double[][] MultiplyARowByAScalar(double[][] matrix, int row, double scalar)
        {
            for (int i = 0; i < matrix[row].Length; i++)
            {
                matrix[row][i] = scalar * matrix[row][i];
            }
            return matrix;
        }
        public static double[][] AddAScalarMultipleOfARowToAnother(double[][] matrix, int row, double scalar, int rowDest)
        {
            for (int i = 0; i < matrix[row].Length; i++)
            {
                matrix[rowDest][i] += matrix[row][i] * scalar;
                if (Math.Abs(matrix[rowDest][i]) < 1e-8)
                {
                    matrix[rowDest][i] = 0;
                }
            }
            return matrix;
        }
    }
}