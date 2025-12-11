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
            double[][] test  = new double[3][];
            test[0] = new double[4];
            test[1] = new double[4];
            test[2] = new double[4];
            test[0][0] = 2;
            test[0][1] = 1;
            test[0][2] = -1;
            test[0][3] = 8;

            test[1][0] = -3;
            test[1][1] = -1;
            test[1][2] = 2;
            test[1][3] = -11;

            test[2][0] = -2;
            test[2][1] = 1;
            test[2][2] = 2;
            test[2][3] = -3;
            test = GaussianElimination(test);

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

            }
            return sumOfPress;
        }
        public static double[][] GaussianElimination(double[][] matrix)
        {
            matrix = PutAMatrixInRowEchelonForm(matrix);
            matrix = PutAMatrixInEchelonForm(matrix);
            return matrix;
        }
        public static double[][] PutAMatrixInEchelonForm(double[][] matrix)
        {
            for (int i = matrix.Length -1; i >= 0; i--)
            {
                (double value, int index) pivot = GetPivotOfRow(matrix[i]);
                for (int j = 0; j < i; j++)
                {
                    matrix = AddAScalarMultipleOfARowToAnother(matrix, i, (-pivot.value) * matrix[j][pivot.index], j);
                }
                matrix = MultiplyARowByAScalar(matrix, i, 1 / pivot.value);
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
            allPivot = allPivot.OrderBy(x => x.pivot.index).ThenByDescending(x => x.pivot.value).ToList();

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
            }
            return matrix;
        }
    }
}