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
    public class Day10Part2Object
    {
        public VectorV2[] Vector = new VectorV2[0];
        public VectorV2 Jolitage = new VectorV2();
    }
    public class VectorV2
    {
        public int[] Vector = new int[0];
        public VectorV2()
        {

        }
        public VectorV2(int size)
        {
            Vector = new int[size];
        }
        public VectorV2(int[] vector)
        {
            Vector = vector;
        }
        public int GetLegth()
        {
            int result = 0;
            foreach (var item in Vector)
            {
                result += item * item;
            }
            return result;
        }
        public VectorV2 MultiplyBy(int scalair)
        {
            var newVector = new VectorV2();
            newVector.Vector = new int[Vector.Length];
            for (int i = 0; i < Vector.Length; i++)
            {
                newVector.Vector[i] = scalair * Vector[i];
            }
            return newVector;
        }
        public VectorV2 Add(VectorV2 vector)
        {
            if (Vector.Length != vector.Vector.Length)
            {
                throw new Exception();
            }

            var newVector = new VectorV2();
            newVector.Vector = new int[Vector.Length];
            for (int i = 0; i < Vector.Length; i++)
            {
                newVector.Vector[i] = vector.Vector[i] + Vector[i];
            }
            return newVector;
        }
    }
    public static class Day10Part2
    {

        public static void Solve()
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
                Dictionary<string, string> cfg = new Dictionary<string, string>() { { "MODEL", "true" } };
                using (Context ctx = new Context())
                {
                    Optimize opt = ctx.MkOptimize();
                    IntExpr[] scalar = new IntExpr[machine.Vector.Length];

                    for (int i = 0; i < scalar.Length; i++)
                    {
                        scalar[i] = (IntExpr)ctx.MkIntConst("p_" + i);
                        opt.Assert(ctx.MkGe(scalar[i], ctx.MkInt(0)));
                    }

                    for (int i = 0; i < machine.Jolitage.Vector.Length; i++)
                    {
                        IntExpr rowSum = ctx.MkInt(0);
                        for (int j = 0; j < machine.Vector.Length; j++)
                        {
                            rowSum = (IntExpr)ctx.MkAdd(rowSum, ctx.MkMul(ctx.MkInt(machine.Vector[j].Vector[i]), scalar[j]));
                        }
                        opt.Assert(ctx.MkEq(rowSum, ctx.MkInt(machine.Jolitage.Vector[i])));
                    }

                    IntExpr sumOfScalar = (IntExpr)ctx.MkInt(0);
                    foreach (var item in scalar)
                    {
                        sumOfScalar = (IntExpr)ctx.MkAdd(sumOfScalar, item);
                    }
                    opt.MkMinimize(sumOfScalar);

                    if (opt.Check() == Status.SATISFIABLE)
                    {
                        Model model = opt.Model;
                        IntNum minTotal = (IntNum)model.Evaluate(sumOfScalar);
                        sumOfPress += minTotal.Int;
                    }
                    else
                    {
                        Console.WriteLine("No solution found.");
                    }
                }
            }

            Console.WriteLine("You need to press that many button : " + sumOfPress);
        }
    }
}